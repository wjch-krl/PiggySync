using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using PiggySyncWin.Domain.Concrete;

namespace PiggySyncWin.WinUI.Models
{
	public class PiggyRemoteHost //or struct
	{
		static PiggyRemoteHost me = new PiggyRemoteHost (IPUtils.LocalIPAddress (), XmlSettingsRepository.Instance.Settings.ComputerName);

		static public PiggyRemoteHost Me {
			get { return me; }
		}

		IPAddress ip;

		public IPAddress Ip {
			get { return ip; }
		}

		string name;

		public string Name {
			get { return name; }
		}

		UInt64 hasCode;

		public UInt64 HashCode {
			get { return hasCode; }
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
			this.name = name;
			this.hasCode = CalculateHash (name + ip.ToString ());
		}

		static UInt64 CalculateHash (string read)
		{
			UInt64 hashedValue = 3074457345618258791ul;
			for (int i = 0; i < read.Length; i++)
			{
				hashedValue += read [i];
				hashedValue *= 3074457345618258799ul;
			}
			return hashedValue;
		}


		public string GetShortName ()
		{
			return name.Substring (0, name.Length - PiggySyncWin.Domain.Concrete.XmlSettingsRepository.RandomNamePartLenght);
		}
	}
    

}
