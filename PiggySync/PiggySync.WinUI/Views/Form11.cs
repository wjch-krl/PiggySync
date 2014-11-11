using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using PiggySync.Core;
using PiggySyncWin.Domain;
using PiggySyncWin.WinUI.Models;

namespace PiggySyncWin.WinUI.Views
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            //InitializeComponent();
            //CertificateManager.Initialize();
            //FileManager.Initialize();
            //var tmp1=new List<FileInfoPacket>();
            //tmp1.Add(new FileInfoPacket(new FileInf()
            //    {
            //        CheckSum = 32,
            //        FileName = "DUPA",
            //        FileSize = 12,
            //        LastModyfied = 12222222,                        
            //    }));
            //var tmp2 = new List<FolderInfoPacket>();
            //var tmp3 = new List<FileDeletePacket>();
            //tmp2.Add(new FolderInfoPacket(tmp1,new List<FolderInfoPacket>(),));
           // FileManager.SetRootFolder(new SyncInfoPacket(tmp1,tmp2,tmp3));

            SyncManager main = new SyncManager();
            main.Run();
        }
    }
}
