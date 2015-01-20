using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using PiggySync.Domain.Abstract;
using PiggySync.Common;

namespace PiggySync.Domain.Concrete
{
    public class XmlSettingsRepository : ISettingsRepository
    {
        public const int RandomNamePartLenght = 8;
        private XmlSerializer serializer;
        private Settings settings;

        static XmlSettingsRepository() //TODO DAFUCK!!!!!!!!!!!!!!!!!!!!!!!!!! Static ctor is not being caled
        {
            Instance = new XmlSettingsRepository();
//			SettingsPath = Path.Combine(Common.TypeResolver.EnviromentHelper.DocumentsPath, ".PiggySync");
//			SettingsFile = Path.Combine(SettingsPath, "Piggy.xml");
//			if (TypeResolver.DirectoryHelper.Exists (SettingsPath))
//			{
//				TypeResolver.DirectoryHelper.CreateHiddenDirectory (SettingsPath);
//			}
        }

        private XmlSettingsRepository()
        {
            SettingsPath = Path.Combine(Common.TypeResolver.EnviromentHelper.DocumentsPath, ".PiggySync");
            SettingsFile = Path.Combine(SettingsPath, "Piggy.xml");
			if (TypeResolver.DirectoryHelper.Exists (SettingsPath))
            {
				TypeResolver.DirectoryHelper.CreateHiddenDirectory (SettingsPath);
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
                settings = LoadSettingsFile();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return false;
        }

        public bool SaveSettings()
        {
            if (settings == null)
            {
                return false;
            }
            try
            {
				using (var stream = TypeResolver.DirectoryHelper.OperFileWrite (SettingsFile))
                {
                    serializer.Serialize(stream, settings);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return false;
        }

        public void RestoreDefaults()
        {
            settings = new Settings
            {
                ComputerName = Common.TypeResolver.EnviromentHelper.MachineName + Random8Numbers(),
                SyncRootPath = Common.TypeResolver.EnviromentHelper.MyDocuments,
				TextFiles = new List<TextFile> {new TextFile {Extension = ".txt", Pattern =  null },},
                BannedFiles = new HashSet<string> {".DS_Store", "thumbs.db",},
                UseEncryption = false,
                UseTcp = true,
            };
        }

        private Settings LoadSettings()
        {
            try
            {
                return LoadSettingsFile();
            }
            catch (Exception e)
            {
				Debug.WriteLine("LoadSettings: {0}",e);
                RestoreDefaults();
                return settings;
            }
        }

        private Settings LoadSettingsFile()
        {
            serializer = new XmlSerializer(typeof (Settings));
			using (var stream = TypeResolver.DirectoryHelper.OpenFileRead(SettingsFile))
            {
                var s = (Settings) serializer.Deserialize(stream);
                settings = s;

                s.BannedFiles = new HashSet<string> {".DS_Store", "thumbs.db",};

                var pattern = new MergePattern()
                {
                    AggregateStartTag = "{",
                    AggregateStopTag = "}",
                    TagOpenString = new[] {"(", ";", ")", " ", "\t"}
                };
                s.TextFiles.Add(new TextFile() {Extension = ".cs", Pattern = pattern});

                return s;
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