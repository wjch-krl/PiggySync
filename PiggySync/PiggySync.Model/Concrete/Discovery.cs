﻿using System;
using System.Linq;
using System.Net;
using PiggySync.Domain.Concrete;
using PiggySync.Model.Abstract;
using PiggySyncWin.WinUI.Models;
using PiggySync.Model.Abstract;

namespace PiggySync.Model.Concrete
{
    public class Discovery : UdpPacket
    {
        public Discovery()
            : base(240)
        {
        }

        static byte[] name = System.Text.Encoding.UTF8.GetBytes(XmlSettingsRepository.Instance.Settings.ComputerName);
        public static byte[] Name
        {
            get { return name; }
        }

        public override byte[] GetPacket()
        {
            byte[] ip = PiggyRemoteHost.Me.Ip.GetAddressBytes();
            byte[] msg = new byte[ip.Length + name.Length + 1];
            msg[0] = Code;
            ip.CopyTo(msg, 1); //TODO concat ??
            name.CopyTo(msg, ip.Length+1);
            return msg;
        }

        public static PiggyRemoteHost GetHOstData(byte[] data)
        {
            byte[] ip = new byte[4];
            Array.Copy(data, 1, ip, 0, 4);

            string name = System.Text.Encoding.UTF8.GetString(data, 5, data.Count() - 5);
            return new PiggyRemoteHost(new IPAddress(ip), name);       
        }

       

    }

}
