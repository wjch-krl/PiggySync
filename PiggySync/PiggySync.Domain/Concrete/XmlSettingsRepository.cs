using System;
using System.IO;
using System.Xml.Serialization;
using PiggySyncWin.Domain;
using PiggySync.Domain.Abstract;

namespace PiggySync.Domain.Concrete
{
	public class XmlSettingsRepository : ISettingsRepository
	{
		public const int RandomNamePartLenght = 8;
		private XmlSerializer serializer;
		Settings settings;

		public Settings Settings
		{
			get { return settings; }
			set { settings = value; }
		}

		public static string SettingsPath
		{
			get;
			set;
		}

		static XmlSettingsRepository ()
		{
			Instance = new XmlSettingsRepository ();
			SettingsPath = String.Format ("{0}/{1}", Environment.GetFolderPath (Environment.SpecialFolder.Personal), ".PiggySync");
			if (!Directory.Exists (SettingsPath))
			{
				DirectoryInfo di = Directory.CreateDirectory (SettingsPath);
				di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 
			}
		}

		public static XmlSettingsRepository Instance
		{
			get;
			set;
		}

		private XmlSettingsRepository ()
		{
			settings = LoadSettings ();
		}

		private Settings LoadSettings ()
		{
			try
			{
				serializer = new XmlSerializer (typeof(Settings));
				Stream stream = new FileStream (Environment.CurrentDirectory + "\\settings.xml", FileMode.Open, FileAccess.Read);
				Settings s = (Settings)serializer.Deserialize (stream);
				stream.Close ();
				return s;
			}
			catch (Exception)
			{
				//var rand = new Random();
				//byte[] buffer = new byte[RandomNamePartLenght];
				//rand.NextBytes(buffer);
				return new Settings () { //Defaults Settrings
					ComputerName = System.Environment.MachineName + Random8Numbers (),
					SyncRootPath = System.Environment.CurrentDirectory,
				};

			}
		}

		public bool ReLoadSettings ()
		{
			try
			{
				serializer = new XmlSerializer (typeof(Settings));
				Stream stream = new FileStream (Environment.CurrentDirectory + "\\settings.xml", FileMode.Open, FileAccess.Read);
				Settings s = (Settings)serializer.Deserialize (stream);
				stream.Close ();
				settings = s;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool SaveSettings ()
		{
			if (settings == null)
			{
				return false;
			}
			try
			{
				serializer = new XmlSerializer (typeof(Settings));
				Stream stream = new FileStream (Environment.CurrentDirectory + "\\settings.xml", FileMode.Create, FileAccess.Write);
				serializer.Serialize (stream, settings);
				stream.Close ();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void ClearSettings ()
		{
			//var rand = new Random();
			//byte[] buffer = new byte[RandomNamePartLenght];
			//rand.NextBytes(buffer);
			settings = new Settings () { //Defaults Settrings
				ComputerName = System.Environment.MachineName + Random8Numbers (), /*rand.Next(),*/ //System.Text.Encoding.UTF8.GetString(buffer),
				SyncRootPath = System.Environment.CurrentDirectory,
			};
		}


		private string Random8Numbers ()
		{
			Random random = new Random ();
			string result = "";
			for (int i = 0; i < 8; i++)
			{
				result += random.Next (0, 9).ToString ();
			}
			return result;
		}
	}
}
