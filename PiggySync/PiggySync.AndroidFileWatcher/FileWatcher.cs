using System;
using Android.OS;

namespace PPiggySync.AndroidFileWatcher
{
	class CustomFileObserver : FileObserver
	{
		public CustomFileObserver(string path) : base(path) {}

		public override void OnEvent (FileObserverEvents e, string path)
		{
			switch (e) {
			case FileObserverEvents.Create:
				break;
			case FileObserverEvents.Modify:
				break;
			case FileObserverEvents.Delete:
				break;
			case FileObserverEvents.DeleteSelf:
				break;
			default:
				break;
			}
		}
	}

	public class AndrFileWatcher
	{
		FileObserver fileObsver;
		public AndrFileWatcher ()
		{
			fileObsver = new CustomFileObserver ("");
		//	fileObsver.
		}
			
	}
}

