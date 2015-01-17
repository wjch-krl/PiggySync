using System;
using PiggySync.Model;

namespace PiggySync.Model
{
	public interface IStatusWather
	{
		SyncStatus Status { set;}
		double Progress {set; }
	}
}

