using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PiggySyncWin.WinUI.Infrastructure.Concrete;
using PiggySyncWin.WinUI.Models;
using PiggySyncWin.WinUI.Models.Concrete;
using PiggySync.Core;
using PiggySync.Model.Concrete;
using PiggySync.Common;

namespace PiggySyncWin.Core
{
	public class SyncManager
	{
		ConcurrentBag<PiggyRemoteHost> hosts = new ConcurrentBag<PiggyRemoteHost> ();
		ConcurrentQueue<PiggyRemoteHostSync> serverQueue = new ConcurrentQueue<PiggyRemoteHostSync> ();
		ConcurrentQueue<PiggyRemoteHostSync> clientQueue = new ConcurrentQueue<PiggyRemoteHostSync> ();
		object clientLock;
		object serverLock;
		ConcurrentDictionary<UInt32, bool> ACKDickt;
		static UInt32 packetNum = 0;
		Thread broadcaster;
		Thread listner;
		Thread syncClient;
		Thread syncServer;
		Thread hostListCleaner;

		UdpClient UDPReader;
		UdpClient UDPWriter;
		IHostWather observer;

		public SyncManager ()
		{
			broadcaster = new Thread (new ThreadStart (ThreadBroadcasterRun));
			listner = new Thread (new ParameterizedThreadStart (ThreadListnerRun));
			syncClient = new Thread (new ThreadStart (HandleSyncAsServer));
			syncServer = new Thread (new ThreadStart (HandleSyncAsClient));
			hostListCleaner = new Thread (new ThreadStart (CleanHostsList));
			serverLock = new object ();
			clientLock = new object ();
			UDPReader = new UdpClient (1337);
			UDPWriter = new UdpClient ();
			ACKDickt = new ConcurrentDictionary<UInt32, bool> ();
			UDPReader.EnableBroadcast = true;
		}

		public bool IsSynchronizing
		{
			get { return serverQueue.Count + clientQueue.Count > 0; }
		}

		private void CleanHostsList ()
		{
			do
			{
				Thread.Sleep (100000);
				PiggyRemoteHost host;
				ConcurrentBag<PiggyRemoteHost> cleaned = new ConcurrentBag<PiggyRemoteHost>();
				while(hosts.TryTake(out host))
				{
					if (clientQueue.Contains (host) || serverQueue.Contains (host))
					{
						cleaned.Add (host);
					}
				} 
				hosts = cleaned;
				NotyfyAboutHostUpade ();
			} while (true);
		}

		void NotyfyAboutHostUpade ()
		{
			if (observer != null)
			{
				observer.RefreshHostsList (hosts);
			}
		}

		public void ThreadListnerRun (object observer)
		{         
			IPEndPoint source = new IPEndPoint (IPAddress.Any, 1337);
           
			byte[] msg;
			PiggyRemoteHost remote;
			do
			{
				lock (UDPReader)
				{
					msg = UDPReader.Receive (ref source);
				}
                
				switch (msg [0])
				{
                    
				case 240:
					Task.Factory.StartNew (() =>
					{
						try
						{
							remote = Discovery.GetHOstData (msg);

							((SyncManager)observer).CreateNewConnection (remote); 
                                
						}
						catch (Exception e)
						{
							System.Diagnostics.Debug.WriteLine (e);
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
							System.Diagnostics.Debug.WriteLine (e.ToString ());
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
							System.Diagnostics.Debug.WriteLine (e.ToString ());
						}
					});
					break;
				case 0:
					Task.Factory.StartNew (() =>
					{
						var tmp = BitConverter.ToUInt32 (msg, 1);//udpdates ack with seq numm TODO describe better
						if (!ACKDickt.TryUpdate (tmp, false, true))
						{
							System.Diagnostics.Debug.WriteLine ("recived ack err");
							//TODO handle error - was allready updated
						}
						else
						{
							System.Diagnostics.Debug.WriteLine ("recived ack ok");
						}
					});
					break;
				default:
					Task.Factory.StartNew (() =>
					{
						System.Diagnostics.Debug.WriteLine ("Unknown UDP packet recieved");
						System.Diagnostics.Debug.WriteLine (System.Text.Encoding.UTF8.GetString (msg));
					});
					break;
				}
			} while (true);
		}

		public void ThreadBroadcasterRun ()
		{
			UdpClient broadcaster = new UdpClient ();
			broadcaster.EnableBroadcast = true;
			Discovery discovery = new Discovery ();
			byte[] msg = discovery.GetPacket ();
			IPEndPoint destination = new IPEndPoint (IPUtils.LocalBroadCastAdress, 1337);
			do
			{
				broadcaster.Send (msg, msg.Count (), destination);
				Thread.Sleep (1000);
			} while (true);
		}

		public void CreateNewConnection (PiggyRemoteHost host)
		{
			if (!hosts.Contains (host) && PiggyRemoteHost.Me.Name != host.Name)
			{
				foreach (var x in hosts)
				{
					if (x.Name == host.Name && x.Ip != x.Ip)
					{
						// hosts.Remove(x); TODO Remove host
						break;
					}
				}
				System.Diagnostics.Debug.WriteLine ("Addding new host: " + host.Name);
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
			IPEndPoint hostAddr;
			foreach (var x in hosts)
			{
				seqNumber = packetNum++;
				packet = new SyncNotyfy (seqNumber);
				msg = packet.GetPacket ();
				hostAddr = new IPEndPoint (x.Ip, 1337);
				lock (UDPWriter)
				{
					UDPWriter.Send (msg, msg.Length, hostAddr);
				}
				x.SEQNumber = seqNumber;
				if (!ACKDickt.TryAdd (seqNumber, true))
				{
					//TODO handle error
				}
			}
			Thread.Sleep (2000);
			foreach (var x in hosts)
			{
				string hostsNotResponding = "";

				if (ACKDickt.TryGetValue (x.SEQNumber, out ack))
				{
					if (ack)
					{
						hostsNotResponding += x.Name + " ";
					}
				}
				else
				{
					//TODO handle error
				}
				if (hostsNotResponding != "")
				{
					throw new ConnectionTimeOutException ("Following host didn't respond:\n" + hostsNotResponding);
				}
			}
		}

		int didReciveSyncNotyfyRetries = 0;

		public bool DidReciveSyncNotyfy (byte[] msg)
		{
			UInt32 seqNumber = BitConverter.ToUInt32 (msg, 1);
			string name = System.Text.Encoding.UTF8.GetString (msg, 5, msg.Length - 5);
			foreach (var x in hosts)
			{
				if (x.Name == name)
				{
					var packet = new PiggyACK (seqNumber).GetPacket ();
					serverQueue.Enqueue (new PiggyRemoteHostSync (x, false, packet));//or false
					lock (serverLock)
					{
						Monitor.PulseAll (serverLock);
					}
					return true;

				}
			}
			if (didReciveSyncNotyfyRetries++ > 3)
			{
				Thread.Sleep (1200);
				if (DidReciveSyncNotyfy (msg))
				{
					return true;
				}

			}
			didReciveSyncNotyfyRetries = 0;
			throw new InvalidHostException ("Host notyfied about file changes but is not in the hosts list.");

		}

		int didReciveSyncRequestReties = 0;

		public bool DidReciveSyncRequest (byte[] msg)
		{
            
			UInt32 seqNumber = BitConverter.ToUInt32 (msg, 1);
			string name = System.Text.Encoding.UTF8.GetString (msg, 5, msg.Length - 5);
			foreach (var x in hosts)
			{
				if (x.Name == name)
				{
					System.Diagnostics.Debug.WriteLine ("DidReciveSyncRequest");
					var packet = new PiggyACK (seqNumber).GetPacket ();
					clientQueue.Enqueue (new PiggyRemoteHostSync (x, true, packet));
					lock (clientLock)
					{
						Monitor.PulseAll (clientLock);
					}
					return true;

				}
			}
			if (didReciveSyncRequestReties++ < 2)//HACK handle error - sometimes happens but shouldn't
			{
				Thread.Sleep (1100);
				System.Diagnostics.Debug.WriteLine ("2nd chanse");

				if (DidReciveSyncRequest (msg))
					return true;
			}
			didReciveSyncRequestReties = 0;
			throw new InvalidHostException ("Host requested sync but is not in the hosts list."); 
            
		}

		public void HandleSyncAsServer ()
		{
			PiggyRemoteHostSync x;

			do
			{
				lock (clientLock)
				{
					while (clientQueue.TryDequeue (out x))
					{
						System.Diagnostics.Debug.WriteLine ("Connection from " + x.Ip.ToString ());
						var endPoint = new IPEndPoint (x.Ip, 1337);
						System.Diagnostics.Debug.WriteLine ("Sending ACK");
						lock (UDPWriter)
						{
							UDPWriter.Send (x.Msg, x.Msg.Length, endPoint);
						}

						TcpListener listner = new TcpListener (new IPEndPoint (PiggyRemoteHost.Me.Ip, 1339));
						listner.Start ();
						TcpClient newConnection;
						newConnection = listner.AcceptTcpClient ();
                        
						listner.Stop ();     


						Syncronizer.HandleSyncAsServerNoSSL (newConnection);
					}


					Monitor.Wait (clientLock);
				}
                
                
			} while (true);
		}

        
		public void HandleSyncAsClient ()
		{
			PiggyRemoteHostSync x;
			do
			{
				lock (serverLock) //TODO
				{
					while (serverQueue.TryDequeue (out x))
					{ 
						System.Diagnostics.Debug.WriteLine ("Connecting to " + x.Ip.ToString ());
						var endPoint = new IPEndPoint (x.Ip, 1337);
						if (x.Msg != null) //Send ACK packet only when is invoked by recieving Sync notyfy packet
						{
							System.Diagnostics.Debug.WriteLine ("Sending ACK s");
							lock (UDPWriter)
							{
								UDPWriter.Send (x.Msg, x.Msg.Length, endPoint);
							}
						}
						var packetN = packetNum++;
						var reqPacket = new SyncRequest (packetN);
						var packet = reqPacket.GetPacket ();

						lock (UDPWriter)
						{
							UDPWriter.Send (packet, packet.Length, endPoint);
						}
						int i = 0;
						bool ack;
						if (!ACKDickt.TryAdd (packetN, true))
						{
							//TODO handle error - duplicated value - seq number had overflowed
							System.Diagnostics.Debug.WriteLine ("Dictionary add err");
						}
						do
						{
							ACKDickt.TryGetValue (packetN, out ack);
							Thread.Sleep (10);
							if (i++ == 1000)
							{
								throw new ConnectionTimeOutException ("Doesn't have recieved ACK packet");
							}
							if (!ack)
							{
								break;
							}
						} while (true);


						TcpClient newConnection;
						IPEndPoint host;
						host = new IPEndPoint (x.Ip, 1339);
						newConnection = new TcpClient (new IPEndPoint (PiggyRemoteHost.Me.Ip, 1338));
						newConnection.Connect (host);

						Syncronizer.HandleSyncAsClientNoSSL (newConnection);
						try
						{
							newConnection.Close ();
						}
						catch (Exception e)
						{
							System.Diagnostics.Debug.WriteLine (e);
						}
						DeviaceHistoryManager.AddHost (x);
					}
					Monitor.Wait (serverLock);
				}
			} while (true);
		}


		public void Run ()
		{
			listner.Start (this);
			broadcaster.Start ();
			syncClient.Start ();
			syncServer.Start ();
			hostListCleaner.Start ();
		}
	}

}
