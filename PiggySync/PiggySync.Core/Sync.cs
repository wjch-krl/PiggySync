using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Org.BouncyCastle.X509;
using PiggySync.Common;
using PiggySync.Domain.Concrete;
using PiggySync.Model;
using PiggySync.Model.Abstract;
using PiggySync.Model.Concrete;

namespace PiggySync.Core
{
    internal class Syncronizer
    {
        private static byte[] overflowBuffer;
        private static readonly string RootPath = XmlSettingsRepository.Instance.Settings.SyncRootPath;

        public static void GetRemoteFilesInfo(INetworkStream stream, SyncInfoPacket root)
        {
            var msg = new byte[2048];
            Int32 bytes;
            var packets = new List<TCPPacket>();
            do
            {
                bytes = stream.Read(msg, 0, msg.Length);
                packets.AddRange(TCPPacketReCreator.RecrateFromRecivedData(msg, bytes));
            } while (packets.Last() is NoRequestPacket);

            BuildFilesTree(root, packets);
        }

        private static void BuildFilesTree(SyncInfoPacket root, List<TCPPacket> packets)
        {
            int i = 0;
            foreach (var x in packets)
            {
                i++;
                if (x is FileInfoPacket)
                {
                    root.Files.Add(x as FileInfoPacket);
                }
                else if (x is FolderInfoPacket)
                {
                    root.Folders.Add(x as FolderInfoPacket); //TODO 2 subfolders in one folder
                    BuildFilesTree(x as FolderInfoPacket, packets.GetRange(i, packets.Count - i));
                }
                else if (x is FileDeletePacket)
                {
                    root.DeletedFiles.Add(x as FileDeletePacket);
                }
                else if (x is NoRequestPacket)
                {
                    Debug.WriteLine("End recieving remote files infs.");
                    return;
                }
                else
                {
                    Debug.WriteLine("err: " + x.GetType());
                    return;
                }
                if (i >= root.ElelmentsCount)
                {
                    return;
                }
            }
        }

        public static void HandleSyncAsClientNoSsl(ITcpClient host, DateTime lastSyncDate,
            SyncManager.Notifier notyfier)
        {
            try
            {
                using (INetworkStream stream = host.GetStream())
                {
                    var msg = new byte[5]; //5 - size of SyncInfoPacket
                    Int32 bytes;
                    SyncInfoPacket rootFolder;

                    bytes = stream.Read(msg, 0, msg.Length);
                    if (msg[0] == 255 && bytes == 5)
                    {
                        rootFolder = new SyncInfoPacket(msg);
                        GetRemoteFilesInfo(stream, rootFolder);
                    }
                    else
                    {
                        throw new Exception(); //TODO write custom Exception
                    }


                    var remoteChanges = ChooseFilesToSync(rootFolder, lastSyncDate);

                    double progress = 0.01;
                    double step = 1.0/(remoteChanges.DeletedFiles.Count + remoteChanges.FileRequests.Count + 1);

                    foreach (var x in remoteChanges.DeletedFiles)
                    {
                        notyfier.NotyfyObservers(SyncStatus.Synchronizing, progress);
                        progress += step;
                        DeleteFile(x);
                    }
                    foreach (var x in remoteChanges.FileRequests)
                    {
                        notyfier.NotyfyObservers(SyncStatus.Synchronizing, progress);
                        progress += step;
                        SyncFile(x, stream);
                    }
                    msg = new NoRequestPacket().GetPacket();
                    stream.Write(msg, 0, msg.Length);
                }
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (Exception e)
            {
                Debug.WriteLine("HandleSyncAsClientNoSsl: {0}", e);
            }
        }

        private static void SyncFile(FileRequestPacket x, INetworkStream stream)
        {
            byte[] msg = x.GetPacket();
            Debug.WriteLine("Reciving Files of size " + x.File.FileSize);

            stream.Write(msg, 0, msg.Length);
            UInt32 size = x.File.FileSize;
            Int32 bytes;
            msg = new byte[2048];

            using (var writer = new BinaryWriter(TypeResolver.DirectoryHelper.OperFileWrite
                (Path.Combine(x.FilePath, x.File.FileName))))
            {
                try
                {
                    while (size != 0)
                    {
                        bytes = stream.Read(msg, 0, msg.Length);
                        if (bytes > size)
                        {
                            writer.Write(msg, 0, (int) size);
                            if (overflowBuffer == null)
                            {
                                overflowBuffer = msg.SubArray((int) size, bytes - (int) size);
                            }
                            return;
                        }
                        writer.Write(msg, 0, bytes); //TODO secure overflows etc...
                        size -= (UInt32) bytes;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Sync File: {0}", e);
                }
            }

            Debug.WriteLine("Recived File: {0}", x.File.FileName);
        }

        private static FileChangesCollection ChooseFilesToSync(SyncInfoPacket rootFolder, DateTime lastSyncDate)
        {
            return ChooseFilesToSync(rootFolder, FileManager.RootFolder, RootPath, lastSyncDate);
        }

        private static FileChangesCollection ChooseFilesToSync(SyncInfoPacket rootFolder, SyncInfoPacket localFiles,
            string path, DateTime lastSyncDate)
        {
            //TODO
            var remoteFiles = new FileChangesCollection();

            foreach (var remoteFile in rootFolder.Files)
            {
                FileInfoPacket file = remoteFile;
                if (!localFiles.Files.Exists(localFile =>
                {
                    //TODO check local files if deleted
                    if (localFile.File.FileName == file.File.FileName &&
                        localFile.File.FileSize == file.File.FileSize &&
                        localFile.File.CheckSum.SequenceEqual(file.File.CheckSum))
                    {
                        if (localFile.File.LastModyfiedDate > lastSyncDate &&
                            file.File.LastModyfiedDate > lastSyncDate)
                        {
                            //TODO CONFLICT
                            if (localFile.File.LastModyfiedDate > file.File.LastModyfiedDate)
                            {
                                //Local is newer
                            }
                            return true;
                        }
                        return localFile.File.LastModyfied < file.File.LastModyfied;
                    }
                    return false;
                }))
                {
                    remoteFiles.FileRequests.Add(new FileRequestPacket(remoteFile.File, path));
                }
            }
            foreach (var x in rootFolder.DeletedFiles)
            {
                throw new NotImplementedException();
                //TODO  add removed files
            }
            foreach (var x in rootFolder.Folders)
            {
                var localSubfolder = localFiles.Folders.Find(folder => folder.FolderName == x.FolderName);
                if (localSubfolder == null)
                {
                    //TODO Create folder on disk
                    localSubfolder = new FolderInfoPacket(x.FolderName);
                    TypeResolver.DirectoryHelper.CreateDirectory(Path.Combine(path, x.FolderName));
                    lock (localFiles)
                    {
                        localFiles.Folders.Add(localSubfolder);
                    }
                }
                var moreFiles = ChooseFilesToSync(x, localSubfolder, Path.Combine(path, x.FolderName), lastSyncDate);
                remoteFiles.FileRequests.AddRange(moreFiles.FileRequests);
                remoteFiles.DeletedFiles.AddRange(moreFiles.DeletedFiles);
            }
            //TODO delete file packets
            return remoteFiles;
        }

        public static void SendFilePackets(SyncInfoPacket syncInfoPacket, INetworkStream stream)
        {
            byte[] msg = syncInfoPacket.GetPacket();
            stream.Write(msg, 0, msg.Length);
            byte[][] msgs = syncInfoPacket.GetFilePackets();
            foreach (var x in msgs)
            {
                stream.Write(x, 0, x.Length);
            }
            msgs = syncInfoPacket.GetDeletedFilePackets();
            foreach (var x in msgs)
            {
                stream.Write(x, 0, x.Length);
            }
            var folders = syncInfoPacket.Folders;
            foreach (var x in folders)
            {
                SendFilePackets(x, stream);
            }
        }

        public static void HandleSyncAsServerNoSsl(ITcpClient hst, SyncManager.Notifier notyfier) //TODO
        {
            try
            {
                using (INetworkStream stream = hst.GetStream())
                {
                    var syncInfoPacket = FileManager.RootFolder;
                    SendFilePackets(syncInfoPacket, stream);
                    Debug.WriteLine("Sending no request");
                    var noRequest = new NoRequestPacket();
                    byte[] msg = noRequest.GetPacket();
                    stream.Write(msg, 0, msg.Length);
                    msg = new byte[2048];
                    do
                    {
                        Int32 bytes = stream.Read(msg, 0, msg.Length);
                        List<TCPPacket> packets = TCPPacketReCreator.RecrateFromRecivedData(msg, bytes);
                        double progress = 0.01;
                        double step = 1.0/(packets.Count + 1);
                        foreach (var x in packets)
                        {
                            notyfier.NotyfyObservers(SyncStatus.Synchronizing, progress);
                            progress += step;
                            if (x is FileRequestPacket)
                            {
                                var fileReqPacet = x as FileRequestPacket;
                                Debug.WriteLine("Sending file: " + fileReqPacet.File.FileName + "of size" +
                                                fileReqPacet.File.FileSize);
                                SendFile(fileReqPacet, stream);
                            }
                            else
                            {
                                return;
                            }
                        }
                    } while (true);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("HandleSyncAsServerNoSSL:", e);
            }
        }


        private static void SendFile(FileRequestPacket fileReqPacket, INetworkStream stream)
        {
            int packetSize = 1024;
            int packetCount = (int) fileReqPacket.File.FileSize/packetSize;
            int lastPacketSize = (int) fileReqPacket.File.FileSize%packetSize;
            try
            {
                string[] files = TypeResolver.DirectoryHelper.GetFilesFromAllDirectories(RootPath,
                    fileReqPacket.File.FileName);
                string filePath;
                if (files.Length == 1)
                {
                    filePath = files[0];
                }
                else if (files.Length == 0)
                {
                    throw new FileNotFoundException("Cannot load file. ", fileReqPacket.File.FileName);
                }
                else
                {
                    //var chekSumGenerator = new CRC32();//TODO check ckecksum
                    //foreach (var x in files)
                    //{      
                    //}
                    filePath = files[0];
                }
                int i;
                for (i = 0; i < packetCount; i++)
                {
                    Debug.WriteLine("Sending file packets");
                    byte[] packet = (FilePacketCreator.CreatePacket(filePath, i, packetSize));
                    stream.Write(packet, 0, packetSize);
                }
                Debug.WriteLine("Sending last file packet");
                byte[] lastPacket = (FilePacketCreator.CreatePacket(filePath, i, packetSize));
                stream.Write(lastPacket, 0, lastPacketSize);
            }
            catch (Exception e)
            {
                Debug.WriteLine("The process failed: {0}", e);
                throw e;
            }
        }

        private static void DeleteFile(FileDeletePacket x)
        {
            throw new NotImplementedException();
        }

        internal static void HandleSyncAsServer(object hst) //TODO
        {
            ITcpClient host = null;
            try
            {
                host = (ITcpClient) hst;

                string message = "hello word";
                byte[] msg = Encoding.UTF8.GetBytes(message);

                using (var sslStream = TypeResolver.SslStream(host.GetStream()))
                {
                    //  sslStream.AuthenticateAsServer(CertificateManager.ServerCert, false, SslProtocols.Default, true);
                    // Send the message to the connected TcpServer. 
                    sslStream.Write(msg, 0, msg.Length);
                    Debug.WriteLine("Sent: " + message);
                    // Receive the TcpServer.response. 
                    // Buffer to store the response bytes.
                    msg = new Byte[256];
                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = sslStream.Read(msg, 0, msg.Length);
                    // String to store the response UTF8 representation.
                    string responseData = Encoding.UTF8.GetString(msg, 0, bytes);
                    Debug.WriteLine("Received: " + responseData);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("HandleSyncAsServer: {0}", e);
            }
        }

        public static bool ValidateServerCertificate(X509Certificate certificate)
        {
            return certificate.IsValid(DateTime.Now);
        }

        internal static void HandleSyncAsClient(object hst)
        {
            try
            {
                var host = (ITcpClient) hst;
                string message = "hello word";
                byte[] msg = Encoding.UTF8.GetBytes(message);

                using (var sslStream = TypeResolver.SslStream(host.GetStream()))
                {
                    //     sslStream.AuthenticateAsClient(CertificateManager.ServerCert);
                    // Send the message to the connected TcpServer. 
                    sslStream.Write(msg, 0, msg.Length);
                    Debug.WriteLine("Sent: " + message);
                    // Receive the TcpServer.response. 

                    // Buffer to store the response bytes.
                    msg = new Byte[256];
                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = sslStream.Read(msg, 0, msg.Length);
                    // String to store the response UTF8 representation.
                    string responseData = Encoding.UTF8.GetString(msg, 0, bytes);
                    Debug.WriteLine("Received: " + responseData);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("HandleSyncAsClient: {0}", e);
            }
        }
    }
}