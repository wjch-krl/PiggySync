using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PiggySync.Common;
using PiggySync.Domain;
using PiggySync.Domain.Concrete;
using PiggySync.Model;
using PiggySync.Model.Abstract;
using PiggySync.Model.Concrete;

namespace PiggySync.Core
{
    internal class Syncronizer
    {
        private static byte[] overflowBuffer;
        private static readonly string rootPath = XmlSettingsRepository.Instance.Settings.SyncRootPath;

        public static void GetRemoteFilesInfo(NetworkStream stream, SyncInfoPacket root)
        {
            var msg = new byte[2048];
            Int32 bytes;
            var packets = new List<TCPPacket>();
            do
            {
                bytes = stream.Read(msg, 0, msg.Length);
                packets.AddRange(TCPPacketReCreator.RecrateFromRecivedData(msg, bytes));
            } while (packets.Last() is NoRequestPacket);

            buildFilesTree(root, packets);
        }

        private static void buildFilesTree(SyncInfoPacket root, List<TCPPacket> packets)
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
                    buildFilesTree(x as FolderInfoPacket, packets.GetRange(i, packets.Count - i));
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

        public static void HandleSyncAsClientNoSSL(TcpClient host, DateTime lastSyncDate)
        {
            try
            {
                NetworkStream stream = host.GetStream();
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
                foreach (var x in remoteChanges.DeletedFiles)
                {
                    DeleteFile(x);
                }
                foreach (var x in remoteChanges.FileRequests)
                {
                    SyncFile(x, stream);
                }
                msg = new NoRequestPacket().GetPacket();
                stream.Write(msg, 0, msg.Length);
                // Close everything.
                stream.Close();
                host.Close();
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException:", e);
            }
        }

        private static void SyncFile(FileRequestPacket x, NetworkStream stream)
        {
            byte[] msg = x.GetPacket();
            Debug.WriteLine("Reciving Files of size " + x.File.FileSize);

			stream.Write (msg, 0, msg.Length);
			UInt32 size = x.File.FileSize;
			Int32 bytes;
			msg = new byte[2048];
			var filePath = Path.Combine (x.FilePath, x.File.FileName);
			var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            using (var writer = new BinaryWriter(fileStream))
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
                catch (Exception)
                {
                }
                finally
                {
                    writer.Close();
                }
            }

            Debug.WriteLine("Recived File: {0}", x.File.FileName);
        }

        private static FileChangesCollection ChooseFilesToSync(SyncInfoPacket rootFolder, DateTime lastSyncDate)
        {
            return ChooseFilesToSync(rootFolder, FileManager.RootFolder, rootPath, lastSyncDate);
        }

        private static FileChangesCollection ChooseFilesToSync(SyncInfoPacket rootFolder, SyncInfoPacket localFiles,
            string path, DateTime lastSyncDate)
        {
            //TODO
            var remoteFiles = new FileChangesCollection();

            foreach (var remoteFile in rootFolder.Files)
            {
                if (!localFiles.Files.Exists((FileInfoPacket localFile) =>
                {
					//TODO check local files if deleted
                    if (localFile.File.FileName == remoteFile.File.FileName &&
                        localFile.File.FileSize == remoteFile.File.FileSize &&
                        localFile.File.CheckSum.SequenceEqual(remoteFile.File.CheckSum))
                    {
                        if (localFile.File.LastModyfiedDate > lastSyncDate &&
                            remoteFile.File.LastModyfiedDate > lastSyncDate)
                        {
                            //TODO CONFLICT
                            if (localFile.File.LastModyfiedDate > remoteFile.File.LastModyfiedDate)
                            {
                                //Local is newer
                            }
                            return true;
                        }
                        return localFile.File.LastModyfied < remoteFile.File.LastModyfied;
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
                    Directory.CreateDirectory(path + x.FolderName);
                    lock (localFiles)
                    {
                        localFiles.Folders.Add(localSubfolder);
                    }
                }
                var moreFiles = ChooseFilesToSync(x, localSubfolder, path + @"/" + x.FolderName, lastSyncDate);
                remoteFiles.FileRequests.AddRange(moreFiles.FileRequests);
                remoteFiles.DeletedFiles.AddRange(moreFiles.DeletedFiles);
            }
            //TODO delete file packets
            return remoteFiles;
        }

        public static void SendFilePackets(SyncInfoPacket syncInfoPacket, NetworkStream stream)
        {
            byte[] msg;
            byte[][] msgs;
            msg = syncInfoPacket.GetPacket();
            stream.Write(msg, 0, msg.Length);
            msgs = syncInfoPacket.GetFilePackets();
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

        public static void HandleSyncAsServerNoSSL(object hst) //TODO
        {
            try
            {
                var host = (TcpClient) hst;
                NetworkStream stream = host.GetStream();
                var syncInfoPacket = FileManager.RootFolder;
                byte[] msg; //13 sizeof filerequest packet
				FileRequestPacket fileReqPacet;
                Int32 bytes;
                SendFilePackets(syncInfoPacket, stream);
                Debug.WriteLine("Sending no request");
                var noRequest = new NoRequestPacket();
                msg = noRequest.GetPacket();
                stream.Write(msg, 0, msg.Length);
                msg = new byte[2048];
                do
                {
                    bytes = stream.Read(msg, 0, msg.Length);
                    List<TCPPacket> packets = TCPPacketReCreator.RecrateFromRecivedData(msg, bytes);
                    foreach (var x in packets)
                    {
                        if (x is FileRequestPacket)
                        {
                            fileReqPacet = x as FileRequestPacket;
                            Debug.WriteLine("Sending file: " + fileReqPacet.File.FileName + "of size" +
                                            fileReqPacet.File.FileSize);
                            SendFile(fileReqPacet, stream);
                        }
                        else
                        {
                            stream.Close();
                            host.Close();
                            return;
                        }
                    }
                } while (true);
                // Close everything.
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException:", e);
            }
        }


        private static void SendFile(FileRequestPacket fileReqPacket, NetworkStream stream)
        {
            int packetSize = 1024;
            int packetCount = (int) fileReqPacket.File.FileSize/packetSize;
            int lastPacketSize = (int) fileReqPacket.File.FileSize%packetSize;
            int i;

            try
            {
                string[] files = Directory.GetFiles(rootPath, fileReqPacket.File.FileName, SearchOption.AllDirectories);
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
                Debug.WriteLine("The process failed: {0}", e.ToString());
                throw e;
            }
        }

        private static void DeleteFile(FileDeletePacket x)
        {
            throw new NotImplementedException();
        }

        internal static void HandleSyncAsServer(object hst) //TODO
        {
            TcpClient host = null;
            SslStream sslStream = null;
            try
            {
                host = (TcpClient) hst;

                string message = "hello word";
                byte[] msg = Encoding.UTF8.GetBytes(message);

                sslStream = new SslStream(host.GetStream());
                sslStream.AuthenticateAsServer(CertificateManager.ServerCert, false, SslProtocols.Default, true);


                // Send the message to the connected TcpServer. 
                sslStream.Write(msg, 0, msg.Length);

                Debug.WriteLine("Sent: " + message);

                // Receive the TcpServer.response. 

                // Buffer to store the response bytes.
                msg = new Byte[256];

                // String to store the response UTF8 representation.
                string responseData;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = sslStream.Read(msg, 0, msg.Length);
                responseData = Encoding.UTF8.GetString(msg, 0, bytes);
                Debug.WriteLine("Received: " + responseData);
                // Close everything.
                sslStream.Close();
                host.Close();
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException:", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException:", e);
            }
            catch (AuthenticationException e)
            {
                Debug.WriteLine("AuthenticationException:", e);
            }
            finally
            {
                // The UDPReader stream will be closed with the sslStream 
                // because we specified this behavior when creating 
                // the sslStream.
                if (sslStream != null)
                {
                    sslStream.Close();
                }
                if (host != null)
                {
                    host.Close();
                }
            }
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            Debug.WriteLine("Certificate error: {0}", sslPolicyErrors);

            return false;
        }


        internal static void HandleSyncAsClient(object hst)
        {
            TcpClient host = null;
            SslStream sslStream = null;
            try
            {
                host = (TcpClient) hst;

                string message = "hello word";
                byte[] msg = Encoding.UTF8.GetBytes(message);

                sslStream = new SslStream(host.GetStream(), false, ValidateServerCertificate, null);

                sslStream.AuthenticateAsClient(CertificateManager.ServerCert.FriendlyName);
                // Send the message to the connected TcpServer. 
                sslStream.Write(msg, 0, msg.Length);

                Debug.WriteLine("Sent: " + message);

                // Receive the TcpServer.response. 

                // Buffer to store the response bytes.
                msg = new Byte[256];

                // String to store the response UTF8 representation.
                string responseData;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = sslStream.Read(msg, 0, msg.Length);
                responseData = Encoding.UTF8.GetString(msg, 0, bytes);
                Debug.WriteLine("Received: " + responseData);

                // Close everything.
                sslStream.Close();
                host.Close();
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
            catch (AuthenticationException e)
            {
                Debug.WriteLine("AuthenticationException: {0}", e);
            }
            finally
            {
                // The UDPReader stream will be closed with the sslStream 
                // because we specified this behavior when creating 
                // the sslStream.
                if (sslStream != null)
                {
                    sslStream.Close();
                }
                if (host != null)
                {
                    host.Close();
                }
            }
        }
    }
}
