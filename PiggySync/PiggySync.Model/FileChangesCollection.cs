using System.Collections.Generic;
using PiggySync.Model.Concrete;

namespace PiggySync.Model
{
    public class FileChangesCollection
    {
        public FileChangesCollection()
        {
            DeletedFiles = new List<FileDeletePacket>();
            FileRequests = new List<FileRequestPacket>();
        }

        public List<FileRequestPacket> FileRequests { get; set; }

        public List<FileDeletePacket> DeletedFiles { get; set; }
    }
}