﻿using System;
using System.Net;
using PiggySync.Common;
using PiggySync.Domain.Concrete;
using SQLite.Net.Attributes;

namespace PiggySync.Model
{
    public class PiggyRemoteHost //or struct
    {
        private static readonly PiggyRemoteHost me = new PiggyRemoteHost(IPUtils.LocalIPAddress(),
            XmlSettingsRepository.Instance.Settings.ComputerName);

        private IPAddress ip;

        public PiggyRemoteHost(IPAddress ip, string name)
        {
            this.ip = ip;
            Name = name;
            HashCode = CalculateHash(name + ip);
        }

        public static PiggyRemoteHost Me
        {
            get { return me; }
        }

        public IPAddress Ip
        {
            get { return ip; }
        }

        public byte[] IpBytes
        {
            get { return Ip.GetAddressBytes(); }
            set { ip = new IPAddress(value); }
        }

        public string Name { get; private set; }

        public Int64 HashCode { get; private set; }

        [Ignore]
        public UInt32 SEQNumber { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var host = (PiggyRemoteHost) obj;
            return host.HashCode == HashCode;
        }

        public static bool operator ==(PiggyRemoteHost o1, PiggyRemoteHost o2)
        {
            return o1.Equals(o2);
        }

        public static bool operator !=(PiggyRemoteHost o1, PiggyRemoteHost o2)
        {
            return !o1.Equals(o2);
        }

        private static Int64 CalculateHash(string read)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= 3074457345618258799ul;
            }
            return (Int64) hashedValue;
        }


        public string GetShortName()
        {
            return Name.Substring(0, Name.Length - XmlSettingsRepository.RandomNamePartLenght);
        }
    }
}