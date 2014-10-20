using PiggySyncWin.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PiggySyncWin.Domain.Concrete
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

        static XmlSettingsRepository instance = new XmlSettingsRepository();

        public static XmlSettingsRepository Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private XmlSettingsRepository()
        {
            settings = LoadSettings();
        }

        private Settings LoadSettings()
        {
            try
            {
                serializer = new XmlSerializer(typeof(Settings));
                Stream stream = new FileStream(Environment.CurrentDirectory + "\\settings.xml", FileMode.Open, FileAccess.Read);
                Settings s = (Settings)serializer.Deserialize(stream);
                stream.Close();
                return s;
            }
            catch (Exception)
            {
                //var rand = new Random();
                //byte[] buffer = new byte[RandomNamePartLenght];
                //rand.NextBytes(buffer);
                return new Settings() { //Defaults Settrings
                    ComputerName = System.Environment.MachineName + Random8Numbers(),/* rand.Next(),*/ //System.Text.Encoding.ASCII.GetString(buffer),
                    SyncPath = System.Environment.CurrentDirectory,
                };

            }
        }

        public bool ReLoadSettings()
        {
            try
            {
                serializer = new XmlSerializer(typeof(Settings));
                Stream stream = new FileStream(Environment.CurrentDirectory + "\\settings.xml", FileMode.Open, FileAccess.Read);
                Settings s = (Settings)serializer.Deserialize(stream);
                stream.Close();
                settings = s;
                return true;
            }
            catch (Exception)
            {
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
                serializer = new XmlSerializer(typeof(Settings));
                Stream stream = new FileStream(Environment.CurrentDirectory + "\\settings.xml", FileMode.Create, FileAccess.Write);
                serializer.Serialize(stream, settings);
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ClearSettings()
        {
            //var rand = new Random();
            //byte[] buffer = new byte[RandomNamePartLenght];
            //rand.NextBytes(buffer);
            settings = new Settings() { //Defaults Settrings
                    ComputerName = System.Environment.MachineName + Random8Numbers(), /*rand.Next(),*/ //System.Text.Encoding.UTF8.GetString(buffer),
                    SyncPath = System.Environment.CurrentDirectory,
                };
        }


        private string Random8Numbers()
        {
            Random random = new Random();
            string result = "";
            for (int i = 0; i < 8; i++)
            {
                result += random.Next(0,9).ToString();
            }
            return result;
        }
    }
}
