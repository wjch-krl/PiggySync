using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggySyncWin.Domain
{
	[Serializable]
	public class Settings
	{
		//TODO
		public string SyncPath
		{ 
			get;
			set;
		}

		public string ComputerName
		{ 
			get;
			set;
		}

		public string[] TextFiles
		{
			get;
			set;
		}
	}
}