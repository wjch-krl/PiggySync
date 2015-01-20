using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PiggySync.Common;
using PiggySync.Model;
using PiggySync.Model.Concrete;

namespace PiggySync.Core
{
	public class SyncManager : ISyncManager
	{
		private static UInt32 packetNum;
		private readonly ConcurrentDictionary<UInt32, bool> ackDickt;
		private readonly IUdpClient udpReader;
		private readonly IUdpClient udpWriter;
		private readonly object clientLock;
		private readonly ConcurrentQueue<PiggyRemoteHostSync> clientQueue = new ConcurrentQueue<PiggyRemoteHostSync> ();
		private readonly object serverLock;
		private readonly ConcurrentQueue<PiggyRemoteHostSync> serverQueue = new ConcurrentQueue<PiggyRemoteHostSync> ();
		private int didReciveSyncNotyfyRetries;
		private int didReciveSyncRequestReties;
		private ConcurrentBag<PiggyRemoteHost> hosts = new ConcurrentBag<PiggyRemoteHost> ();

		public static IHostWather HostObserver { get; set; }

		public SyncManager ()
		{ 
			serverLock = new object ();
			clientLock = new object ();
			udpReader = TypeResolver.UdpClient (1337);
			udpWriter = TypeResolver.UdpClient ();
			ackDickt = new ConcurrentDictionary<UInt32, bool> ();
			udpReader.EnableBroadcast = true;
			Observers = new List<IStatusWather> ();
			var fileWather = TypeResolver.Wather;
			if (fileWather != null)
			{
				fileWather.Initialize (this);
			}
		}

		public ICollection<IStatusWather> Observers { get; set; }

		public bool IsSynchronizing
		{
			get { return serverQueue.Count + clientQueue.Count > 0; }
		}

		private void Cleaner ()
		{
			do
			{
//				TypeResolver.ThreadHelper.Sleep (100000);
//				PiggyRemoteHost host;
//				var cleaned = new ConcurrentBag<PiggyRemoteHost> ();
//				while (hosts.TryTake (out host))
//				{
//					if (clientQueue.Contains (host) || serverQueue.Contains (host))
//					{
//						cleaned.Add (host);
//					}
//				}
//				hosts = cleaned;
//				NotyfyAboutHostUpade ();
			} while (true);
		}

		private void NotyfyAboutHostUpade ()
		{
			if (HostObserver != null)
			{
				HostObserver.RefreshHostsList (hosts);
			}
		}

		public void Listner (object observer)
		{
			IIPEndPoint source = TypeResolver.IPEndPoint (TypeResolver.IpHelper.Any, 1337);

			byte[] msg;
			PiggyRemoteHost remote;
			do
			{
				lock (udpReader)
				{
					msg = udpReader.Receive (ref source);
				}

				switch (msg [0])
				{
				case 240:
					Task.Factory.StartNew (() =>
					{
						try
						{
							remote = Discovery.GetHostData (msg);

							((SyncManager)observer).CreateNewConnection (remote);
						}
						catch (Exception e)
						{
							Debug.WriteLine (e);
						}
					});
					break;
				case 255:
					Task.Factory.StartNew (() =>
					{
						try
						{
							DidReciveSyncRequest (msg);
						}
						catch (InvalidHostException e)
						{
							Debug.WriteLine (e.ToString ());
						}
					});
					break;
				case 170:
					Task.Factory.StartNew (() =>
					{
						try
						{
							DidReciveSyncNotyfy (msg);
						}
						catch (InvalidHostException e)
						{
							Debug.WriteLine (e.ToString ());
						}
					});
					break;
				case 0:
					Task.Factory.StartNew (() =>
					{
						var tmp = BitConverter.ToUInt32 (msg, 1); //udpdates ack with seq numm TODO describe better
						if (!ackDickt.TryUpdate (tmp, false, true))
						{
							Debug.WriteLine ("recived ack err");
							//TODO handle error - was allready updated
						}
						else
						{
							Debug.WriteLine ("recived ack ok");
						}
					});
					break;
				default:
					Task.Factory.StartNew (() =>
					{
						Debug.WriteLine ("Unknown UDP packet recieved");
						Debug.WriteLine (Encoding.UTF8.GetString (msg, 0, msg.Length));
					});
					break;
				}
			} while (true);
		}

		public void Broadcaster ()
		{
			IUdpClient broadcastClient = TypeResolver.UdpClient ();
			broadcastClient.EnableBroadcast = true;
			var discovery = new Discovery ();
			byte[] msg = discovery.GetPacket ();
			var destination = TypeResolver.IPEndPoint (IPUtils.LocalBroadCastAdress, 1337);
			do
			{
				broadcastClient.Send (msg, msg.Count (), destination);
				TypeResolver.ThreadHelper.Sleep (1000);
			} while (true);
		}

		public void CreateNewConnection (PiggyRemoteHost host)
		{
			if (!hosts.Contains (host) && PiggyRemoteHost.Me.Name != host.Name)
			{
				var newHosts = hosts.Where (x => x.Name != host.Name || x.Ip == host.Ip);
				hosts = new ConcurrentBag<PiggyRemoteHost> (newHosts);
				Debug.WriteLine ("Addding new host: " + host.Name);
				hosts.Add (host);
				RequestSync (host);
			}
		}

		public void RequestSync (PiggyRemoteHost x)
		{
			serverQueue.Enqueue (new PiggyRemoteHostSync (x, false, null));
			lock (serverLock)
			{
				Monitor.PulseAll (serverLock);
			}
		}

		public void ForceSync ()
		{
			foreach (var elelement in hosts)
			{
				RequestSync (elelement);
			}
		}

		public void NotyfyAllHost ()
		{
			SyncNotyfy packet;
			byte[] msg;
			bool ack;
			UInt32 seqNumber;
			IIPEndPoint hostAddr;
			foreach (var x in hosts)
			{
				seqNumber = packetNum++;
				packet = new SyncNotyfy (seqNumber);
				msg = packet.GetPacket ();
				hostAddr = TypeResolver.IPEndPoint (x.Ip, 1337);
				lock (udpWriter)
				{
					udpWriter.Send (msg, msg.Length, hostAddr);
				}
				x.SEQNumber = seqNumber;
				if (!ackDickt.TryAdd (seqNumber, true))
				{
					//TODO handle error
				}
			}
			TypeResolver.ThreadHelper.Sleep (2000);
			foreach (var x in hosts)
			{
				string hostsNotResponding = "";

				if (ackDickt.TryGetValue (x.SEQNumber, out ack))
				{
					if (ack)
					{
						hostsNotResponding += x.Name + " ";
					}
				}
				if (hostsNotResponding != "")
				{
					throw new ConnectionTimeOutException ("Following host didn't respond:\n" + hostsNotResponding);
				}
			}
		}

		public bool DidReciveSyncNotyfy (byte[] msg)
		{
			UInt32 seqNumber = BitConverter.ToUInt32 (msg, 1);
			string name = Encoding.UTF8.GetString (msg, 5, msg.Length - 5);
			foreach (var x in hosts)
			{
				if (x.Name == name)
				{
					var packet = new PiggyACK (seqNumber).GetPacket ();
					serverQueue.Enqueue (new PiggyRemoteHostSync (x, false, packet)); //or false
					lock (serverLock)
					{
						Monitor.PulseAll (serverLock);
					}
					return true;
				}
			}
			if (didReciveSyncNotyfyRetries++ > 3)
			{
				TypeResolver.ThreadHelper.Sleep (1200);
				if (DidReciveSyncNotyfy (msg))
				{
					return true;
				}
			}
			didReciveSyncNotyfyRetries = 0;
			throw new InvalidHostException ("Host notyfied about file changes but is not in the hosts list.");
		}

		public bool DidReciveSyncRequest (byte[] msg)
		{
			UInt32 seqNumber = BitConverter.ToUInt32 (msg, 1);
			string name = Encoding.UTF8.GetString (msg, 5, msg.Length - 5);
			foreach (var x in hosts)
			{
				if (x.Name == name)
				{
					Debug.WriteLine ("DidReciveSyncRequest");
					var packet = new PiggyACK (seqNumber).GetPacket ();
					clientQueue.Enqueue (new PiggyRemoteHostSync (x, true, packet));
					lock (clientLock)
					{
						Monitor.PulseAll (clientLock);
					}
					return true;
				}
			}
			if (didReciveSyncRequestReties++ < 2) //HACK handle error - sometimes happens but shouldn't
			{
				TypeResolver.ThreadHelper.Sleep (1100);
				Debug.WriteLine ("2nd chanse");

				if (DidReciveSyncRequest (msg))
					return true;
			}
			didReciveSyncRequestReties = 0;
			throw new InvalidHostException ("Host requested sync but is not in the hosts list.");
		}

		public void SyncServer ()
		{
			PiggyRemoteHostSync x;

			do
			{
				lock (clientLock)
				{
					while (clientQueue.TryDequeue (out x))
					{
						Debug.WriteLine ("Connection from " + x.Ip);
						var endPoint = TypeResolver.IPEndPoint (x.Ip, 1337);
						Debug.WriteLine ("Sending ACK");
						lock (udpWriter)
						{
							udpWriter.Send (x.Msg, x.Msg.Length, endPoint);
						}
						ITcpListener listner =
							TypeResolver.TcpListener (TypeResolver.IPEndPoint (PiggyRemoteHost.Me.Ip, 1339));
						listner.Start ();
						ITcpClient newConnection = listner.AcceptTcpClient ();

						listner.Stop ();

						Syncronizer.HandleSyncAsServerNoSsl (newConnection, new Notifier (Observers));
					}


					Monitor.Wait (clientLock);
				}
			} while (true);
		}


		public void SyncClient ()
		{
			do
			{
				lock (serverLock) //TODO
				{
					PiggyRemoteHostSync x;
					while (serverQueue.TryDequeue (out x))
					{
						Debug.WriteLine ("Connecting to " + x.Ip);
						var endPoint = TypeResolver.IPEndPoint (x.Ip, 1337);
						if (x.Msg != null) //Send ACK packet only when is invoked by recieving Sync notyfy packet
						{
							Debug.WriteLine ("Sending ACK s");
							lock (udpWriter)
							{
								udpWriter.Send (x.Msg, x.Msg.Length, endPoint);
							}
						}
						var packetN = packetNum++;
						var reqPacket = new SyncRequest (packetN);
						var packet = reqPacket.GetPacket ();

						lock (udpWriter)
						{
							udpWriter.Send (packet, packet.Length, endPoint);
						}
						int i = 0;
						bool ack;
						if (!ackDickt.TryAdd (packetN, true))
						{
							//TODO handle error - duplicated value - seq number had overflowed
							Debug.WriteLine ("Dictionary add err");
						}
						do
						{
							ackDickt.TryGetValue (packetN, out ack);
							TypeResolver.ThreadHelper.Sleep (10);
							if (i++ == 1000)
							{
								throw new ConnectionTimeOutException ("Doesn't have recieved ACK packet");
							}
							if (!ack)
							{
								break;
							}
						} while (true);

						try
						{
							IIPEndPoint host = TypeResolver.IPEndPoint (x.Ip, 1339);
							using (
								ITcpClient newConnection =
									TypeResolver.TcpClient (TypeResolver.IPEndPoint (PiggyRemoteHost.Me.Ip, 1338)))
							{
								newConnection.Connect (host);
								var lastSyncDate = DeviaceHistoryManager.LastSyncDate (x);
								Syncronizer.HandleSyncAsClientNoSsl (newConnection, lastSyncDate, new Notifier (Observers));
							}
						}
						catch (Exception e)
						{
							Debug.WriteLine (e);
						}
						DeviaceHistoryManager.AddHost (x);
					}
					Monitor.Wait (serverLock);
				}
			} while (true);
		}

		public void Run ()
		{
//            broadcaster =
			Task.Factory.StartNew (Broadcaster);
//            listner =
			Task.Factory.StartNew (() => Listner (this));
//            syncClient =
			Task.Factory.StartNew (SyncServer);
//            syncServer =
			Task.Factory.StartNew (SyncClient);
//            hostListCleaner =
			Task.Factory.StartNew (Cleaner);
		}

		internal class Notifier
		{
			private readonly IEnumerable<IStatusWather> observers;

			internal Notifier (IEnumerable<IStatusWather> observers)
			{
				this.observers = observers;
			}

			internal void NotyfyObservers (SyncStatus stat, double progress = 0.0)
			{
				foreach (var element in observers)
				{
					element.Status = stat;
					element.Progress = progress;
				}
			}
		}
	}
}