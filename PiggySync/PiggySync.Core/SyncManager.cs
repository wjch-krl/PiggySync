using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PiggySync.Common;
using PiggySync.Model;
using PiggySync.Model.Concrete;

namespace PiggySync.Core
{
    public class SyncManager
    {
        private static UInt32 packetNum;
        private readonly ConcurrentDictionary<UInt32, bool> ACKDickt;
        private readonly UdpClient UDPReader;
        private readonly UdpClient UDPWriter;
        private readonly Thread broadcaster;
        private readonly object clientLock;
        private readonly ConcurrentQueue<PiggyRemoteHostSync> clientQueue = new ConcurrentQueue<PiggyRemoteHostSync>();
        private readonly Thread hostListCleaner;
        private readonly Thread listner;
        private readonly object serverLock;
        private readonly ConcurrentQueue<PiggyRemoteHostSync> serverQueue = new ConcurrentQueue<PiggyRemoteHostSync>();
        private readonly Thread syncClient;
        private readonly Thread syncServer;
        private int didReciveSyncNotyfyRetries;
        private int didReciveSyncRequestReties;
        private ConcurrentBag<PiggyRemoteHost> hosts = new ConcurrentBag<PiggyRemoteHost>();
        private IHostWather observer;

        public SyncManager()
        {
            broadcaster = new Thread(ThreadBroadcasterRun);
            listner = new Thread(ThreadListnerRun);
            syncClient = new Thread(HandleSyncAsServer);
            syncServer = new Thread(HandleSyncAsClient);
            hostListCleaner = new Thread(CleanHostsList);
            serverLock = new object();
            clientLock = new object();
            UDPReader = new UdpClient(1337);
            UDPWriter = new UdpClient();
            ACKDickt = new ConcurrentDictionary<UInt32, bool>();
            UDPReader.EnableBroadcast = true;
        }

        public bool IsSynchronizing
        {
            get { return serverQueue.Count + clientQueue.Count > 0; }
        }

        private void CleanHostsList()
        {
            do
            {
                Thread.Sleep(100000);
                PiggyRemoteHost host;
                var cleaned = new ConcurrentBag<PiggyRemoteHost>();
                while (hosts.TryTake(out host))
                {
                    if (clientQueue.Contains(host) || serverQueue.Contains(host))
                    {
                        cleaned.Add(host);
                    }
                }
                hosts = cleaned;
                NotyfyAboutHostUpade();
            } while (true);
        }

        private void NotyfyAboutHostUpade()
        {
            if (observer != null)
            {
                observer.RefreshHostsList(hosts);
            }
        }

        public void ThreadListnerRun(object observer)
        {
            var source = new IPEndPoint(IPAddress.Any, 1337);

            byte[] msg;
            PiggyRemoteHost remote;
            do
            {
                lock (UDPReader)
                {
                    msg = UDPReader.Receive(ref source);
                }

                switch (msg[0])
                {
                    case 240:
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                remote = Discovery.GetHostData(msg);

                                ((SyncManager) observer).CreateNewConnection(remote);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e);
                            }
                        });
                        break;
                    case 255:
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                DidReciveSyncRequest(msg);
                            }
                            catch (InvalidHostException e)
                            {
                                Debug.WriteLine(e.ToString());
                            }
                        });
                        break;
                    case 170:
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                DidReciveSyncNotyfy(msg);
                            }
                            catch (InvalidHostException e)
                            {
                                Debug.WriteLine(e.ToString());
                            }
                        });
                        break;
                    case 0:
                        Task.Factory.StartNew(() =>
                        {
                            var tmp = BitConverter.ToUInt32(msg, 1); //udpdates ack with seq numm TODO describe better
                            if (!ACKDickt.TryUpdate(tmp, false, true))
                            {
                                Debug.WriteLine("recived ack err");
                                //TODO handle error - was allready updated
                            }
                            else
                            {
                                Debug.WriteLine("recived ack ok");
                            }
                        });
                        break;
                    default:
                        Task.Factory.StartNew(() =>
                        {
                            Debug.WriteLine("Unknown UDP packet recieved");
                            Debug.WriteLine(Encoding.UTF8.GetString(msg));
                        });
                        break;
                }
            } while (true);
        }

<<<<<<< Upstream, based on origin/master
        public void ThreadBroadcasterRun()
        {
            var broadcaster = new UdpClient();
            broadcaster.EnableBroadcast = true;
            var discovery = new Discovery();
            byte[] msg = discovery.GetPacket();
            var destination = new IPEndPoint(IPUtils.LocalBroadCastAdress, 1337);
            do
            {
                broadcaster.Send(msg, msg.Count(), destination);
                Thread.Sleep(1000);
            } while (true);
        }
=======
		public void CreateNewConnection (PiggyRemoteHost host)
		{
			if (!hosts.Contains (host) && PiggyRemoteHost.Me.Name != host.Name)
			{
				foreach (var x in hosts)
				{
					if (x.Name == host.Name && x.Ip != x.Ip)
					{
						//hosts.Remove(x); 
						break;
					}
				}
				System.Diagnostics.Debug.WriteLine ("Addding new host: " + host.Name);
				hosts.Add (host);
				RequestSync (host); 
			}
		}
>>>>>>> 0e72788 CheckSum fixes

        public void CreateNewConnection(PiggyRemoteHost host)
        {
            if (!hosts.Contains(host) && PiggyRemoteHost.Me.Name == host.Name)
            {
                foreach (var x in hosts)
                {
                    if (x.Name == host.Name && x.Ip != x.Ip)
                    {
                        //hosts.Remove(x); 
                        break;
                    }
                }
                Debug.WriteLine("Addding new host: " + host.Name);
                hosts.Add(host);
                RequestSync(host);
            }
        }

        public void RequestSync(PiggyRemoteHost x)
        {
            serverQueue.Enqueue(new PiggyRemoteHostSync(x, false, null));
            lock (serverLock)
            {
                Monitor.PulseAll(serverLock);
            }
        }

        public void ForceSync()
        {
            foreach (var elelement in hosts)
            {
                RequestSync(elelement);
            }
        }

        public void NotyfyAllHost()
        {
            SyncNotyfy packet;
            byte[] msg;
            bool ack;
            UInt32 seqNumber;
            IPEndPoint hostAddr;
            foreach (var x in hosts)
            {
                seqNumber = packetNum++;
                packet = new SyncNotyfy(seqNumber);
                msg = packet.GetPacket();
                hostAddr = new IPEndPoint(x.Ip, 1337);
                lock (UDPWriter)
                {
                    UDPWriter.Send(msg, msg.Length, hostAddr);
                }
                x.SEQNumber = seqNumber;
                if (!ACKDickt.TryAdd(seqNumber, true))
                {
                    //TODO handle error
                }
            }
            Thread.Sleep(2000);
            foreach (var x in hosts)
            {
                string hostsNotResponding = "";

                if (ACKDickt.TryGetValue(x.SEQNumber, out ack))
                {
                    if (ack)
                    {
                        hostsNotResponding += x.Name + " ";
                    }
                }
                if (hostsNotResponding != "")
                {
                    throw new ConnectionTimeOutException("Following host didn't respond:\n" + hostsNotResponding);
                }
            }
        }

        public bool DidReciveSyncNotyfy(byte[] msg)
        {
            UInt32 seqNumber = BitConverter.ToUInt32(msg, 1);
            string name = Encoding.UTF8.GetString(msg, 5, msg.Length - 5);
            foreach (var x in hosts)
            {
                if (x.Name == name)
                {
                    var packet = new PiggyACK(seqNumber).GetPacket();
                    serverQueue.Enqueue(new PiggyRemoteHostSync(x, false, packet)); //or false
                    lock (serverLock)
                    {
                        Monitor.PulseAll(serverLock);
                    }
                    return true;
                }
            }
            if (didReciveSyncNotyfyRetries++ > 3)
            {
                Thread.Sleep(1200);
                if (DidReciveSyncNotyfy(msg))
                {
                    return true;
                }
            }
            didReciveSyncNotyfyRetries = 0;
            throw new InvalidHostException("Host notyfied about file changes but is not in the hosts list.");
        }

        public bool DidReciveSyncRequest(byte[] msg)
        {
            UInt32 seqNumber = BitConverter.ToUInt32(msg, 1);
            string name = Encoding.UTF8.GetString(msg, 5, msg.Length - 5);
            foreach (var x in hosts)
            {
                if (x.Name == name)
                {
                    Debug.WriteLine("DidReciveSyncRequest");
                    var packet = new PiggyACK(seqNumber).GetPacket();
                    clientQueue.Enqueue(new PiggyRemoteHostSync(x, true, packet));
                    lock (clientLock)
                    {
                        Monitor.PulseAll(clientLock);
                    }
                    return true;
                }
            }
            if (didReciveSyncRequestReties++ < 2) //HACK handle error - sometimes happens but shouldn't
            {
                Thread.Sleep(1100);
                Debug.WriteLine("2nd chanse");

                if (DidReciveSyncRequest(msg))
                    return true;
            }
            didReciveSyncRequestReties = 0;
            throw new InvalidHostException("Host requested sync but is not in the hosts list.");
        }

        public void HandleSyncAsServer()
        {
            PiggyRemoteHostSync x;

            do
            {
                lock (clientLock)
                {
                    while (clientQueue.TryDequeue(out x))
                    {
                        Debug.WriteLine("Connection from " + x.Ip);
                        var endPoint = new IPEndPoint(x.Ip, 1337);
                        Debug.WriteLine("Sending ACK");
                        lock (UDPWriter)
                        {
                            UDPWriter.Send(x.Msg, x.Msg.Length, endPoint);
                        }

                        var listner = new TcpListener(new IPEndPoint(PiggyRemoteHost.Me.Ip, 1339));
                        listner.Start();
                        TcpClient newConnection;
                        newConnection = listner.AcceptTcpClient();

                        listner.Stop();


                        Syncronizer.HandleSyncAsServerNoSSL(newConnection);
                    }


                    Monitor.Wait(clientLock);
                }
            } while (true);
        }


        public void HandleSyncAsClient()
        {
            PiggyRemoteHostSync x;
            do
            {
                lock (serverLock) //TODO
                {
                    while (serverQueue.TryDequeue(out x))
                    {
                        Debug.WriteLine("Connecting to " + x.Ip);
                        var endPoint = new IPEndPoint(x.Ip, 1337);
                        if (x.Msg != null) //Send ACK packet only when is invoked by recieving Sync notyfy packet
                        {
                            Debug.WriteLine("Sending ACK s");
                            lock (UDPWriter)
                            {
                                UDPWriter.Send(x.Msg, x.Msg.Length, endPoint);
                            }
                        }
                        var packetN = packetNum++;
                        var reqPacket = new SyncRequest(packetN);
                        var packet = reqPacket.GetPacket();

                        lock (UDPWriter)
                        {
                            UDPWriter.Send(packet, packet.Length, endPoint);
                        }
                        int i = 0;
                        bool ack;
                        if (!ACKDickt.TryAdd(packetN, true))
                        {
                            //TODO handle error - duplicated value - seq number had overflowed
                            Debug.WriteLine("Dictionary add err");
                        }
                        do
                        {
                            ACKDickt.TryGetValue(packetN, out ack);
                            Thread.Sleep(10);
                            if (i++ == 1000)
                            {
                                throw new ConnectionTimeOutException("Doesn't have recieved ACK packet");
                            }
                            if (!ack)
                            {
                                break;
                            }
                        } while (true);


                        TcpClient newConnection;
                        IPEndPoint host;
                        host = new IPEndPoint(x.Ip, 1339);
                        newConnection = new TcpClient(new IPEndPoint(PiggyRemoteHost.Me.Ip, 1338));
                        newConnection.Connect(host);
                        var lastSyncDate = DeviaceHistoryManager.LastSyncDate(x);
                        Syncronizer.HandleSyncAsClientNoSSL(newConnection, lastSyncDate);
                        try
                        {
                            newConnection.Close();
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                        }
                        DeviaceHistoryManager.AddHost(x);
                    }
                    Monitor.Wait(serverLock);
                }
            } while (true);
        }


        public void Run()
        {
            listner.Start(this);
            broadcaster.Start();
            syncClient.Start();
            syncServer.Start();
            hostListCleaner.Start();
        }
    }
}
