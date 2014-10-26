using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using PiggySyncWin.Domain.Concrete;
using PiggySyncWin.Domain;

namespace PiggySyncWin.WinUI.Models
{
	public class PiggyRemoteHost //or struct
	{
		static PiggyRemoteHost me = new PiggyRemoteHost (IPUtils.LocalIPAddress (), XmlSettingsRepository.Instance.Settings.ComputerName);

		static public PiggyRemoteHost Me {
			get { return me; }
		}
		private IPAddress ip;
		public IPAddress Ip
		{
			get { return ip; }
		}
			
		public byte[] IpBytes
		{
			get {
				return Ip.GetAddressBytes();
			}
			set {
				ip = new IPAddress(value);
			}
		}

		public string Name
		{
			get;
			private set;
		}

		public Int64 HashCode
		{
			get;
			private set;
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}

		public UInt32 SEQNumber {
			get;
			set;
		}

		public override bool Equals (object obj)
		{
			PiggyRemoteHost host = (PiggyRemoteHost)obj;
			return host.HashCode == this.HashCode;
		}

		public static bool operator == (PiggyRemoteHost o1, PiggyRemoteHost o2)
		{
			return o1.Equals (o2);
		}

		public static bool operator != (PiggyRemoteHost o1, PiggyRemoteHost o2)
		{
			return !o1.Equals (o2);
		}

		public PiggyRemoteHost (IPAddress ip, string name)
		{
			this.ip = ip;
			this.Name = name;
			this.HashCode = CalculateHash (name + ip.ToString ());
		}

		static Int64 CalculateHash (string read)
		{
			UInt64 hashedValue = 3074457345618258791ul;
			for (int i = 0; i < read.Length; i++)
			{
				hashedValue += read [i];
				hashedValue *= 3074457345618258799ul;
			}
			return (Int64)hashedValue;
		}


		public string GetShortName ()
		{
			return Name.Substring (0, Name.Length - PiggySyncWin.Domain.Concrete.XmlSettingsRepository.RandomNamePartLenght);
		}
	}
    

}
