using System;
using System.IO;
using System.Xml.Serialization;
using PiggySync.Domain.Abstract;
using System.Collections.Generic;
using System.Diagnostics;

namespace PiggySync.Domain.Concrete
{
    public class XmlSettingsRepository : ISettingsRepository
    {
        public const int RandomNamePartLenght = 8;
        private XmlSerializer serializer;
        private Settings settings;

        static XmlSettingsRepository()//TODO DAFUCK!!!!!!!!!!!!!!!!!!!!!!!!!! Static ctor is not being caled
        {
            Instance = new XmlSettingsRepository();
            SettingsPath = String.Format("{0}/{1}", Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                ".PiggySync");
			SettingsFile = Path.Combine (SettingsPath,"Piggy.xml");
            if (!Directory.Exists(SettingsPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(SettingsPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        private XmlSettingsRepository()
        {
		//	Instance = new XmlSettingsRepository();
			SettingsPath = String.Format("{0}/{1}", Environment.GetFolderPath(Environment.SpecialFolder.Personal),
				".PiggySync");
			SettingsFile = Path.Combine (SettingsPath,"Piggy.xml");
			if (!Directory.Exists(SettingsPath))
			{
				DirectoryInfo di = Directory.CreateDirectory(SettingsPath);
				di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
			}
            settings = LoadSettings();
        }

        public static string SettingsPath { get; set; }
		public static string SettingsFile { get; private set; }

        public static XmlSettingsRepository Instance { get; set; }

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public bool ReLoadSettings()
        {
            try
            {
                serializer = new XmlSerializer(typeof (Settings));
				Stream stream = new FileStream(SettingsFile, FileMode.Open,
                    FileAccess.Read);
                var s = (Settings) serializer.Deserialize(stream);
                stream.Close();
                settings = s;
                return true;
            }
            catch (Exception e)
            {
				Debug.WriteLine (e);
                return false;
            }
        }

        public bool SaveSettings()
        {
            if (settings == null)
            {
                return false;
            }
            try
            {
                serializer = new XmlSerializer(typeof (Settings));
				Stream stream = new FileStream(SettingsFile, FileMode.Create,
                    FileAccess.Write);
                serializer.Serialize(stream, settings);
                stream.Close();
                return true;
            }
            catch (Exception e)
            {
				Debug.WriteLine (e);
                return false;
            }
        }

        public void RestoreDefaults()
        {
            settings = new Settings
            {
                ComputerName = Environment.MachineName + Random8Numbers(),
                SyncRootPath = Environment.CurrentDirectory,
				AutoSync = true,
				TextFiles = new List<TextFile>() {new TextFile {Extension =  "txt", TemplatePath = String.Empty}, },
				UseEncryption = false,
				UseTcp = true,
            };
        }

        private Settings LoadSettings()
        {
            try
            {
                serializer = new XmlSerializer(typeof (Settings));
				Stream stream = new FileStream(SettingsFile, FileMode.Open,
                    FileAccess.Read);
                var s = (Settings) serializer.Deserialize(stream);
                stream.Close();
                return s;
            }
            catch (Exception e)
            {
				Debug.WriteLine (e);
				RestoreDefaults ();
				return settings;
            }
        }


        private string Random8Numbers()
        {
            var random = new Random();
            string result = "";
            for (int i = 0; i < 8; i++)
            {
                result += random.Next(0, 9).ToString();
            }
            return result;
        }
    }
}