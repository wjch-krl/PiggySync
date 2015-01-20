using System;
using PiggySync.Common;
using System.Net.Sockets;
using System.IO;

namespace PiggySync.StandardTypeResolver
{
	public class DirectoryHelper : IDirectoryHelper
	{
		public void CreateHiddenDirectory (string path)
		{
			var dir = Directory.CreateDirectory (path);;
			dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
		}

		public string[] GetFilesFromAllDirectories (string rootPath, string fileName)
		{
			return Directory.GetFiles (rootPath, fileName, SearchOption.AllDirectories);
		}

		public void CreateDirectory (string path)
		{
			Directory.CreateDirectory (path);
		}

		public bool Exists (string name)
		{
			return Directory.Exists (name);
		}

		public string[] GetDirectories (string path)
		{
			return Directory.GetDirectories (path);
		}

		public string[] GetFiles (string path)
		{
			return Directory.GetFiles (path);
		}

		public string DocumentsPath
		{
			get
			{
				return Environment.GetFolderPath (Environment.SpecialFolder.UserProfile);//TODO
			}
		}

		public string MachineName
		{
			get
			{
				return Environment.MachineName;
			}
		}

		public string MyDocuments
		{
			get
			{
				//return Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);
				return Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			}
		}

		public Stream OpenFileRead (string path)
		{
			return File.OpenRead (path);
		}

		public Stream OperFileWrite (string path)
		{
			return File.OpenWrite (path);
		}

		public bool FileExists (string name)
		{
			return File.Exists (name);
		}
	}

}

