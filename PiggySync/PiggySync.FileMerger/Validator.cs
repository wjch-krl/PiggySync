using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace PiggySync.FileMerger
{
    public static class Validator
    {
        public static bool ValidateXml(string xmlString)
        {
            try
            {
                var x = new XmlDocument();
                x.LoadXml(xmlString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool ValidateJson(ref string jsonString)
        {
            var serializer = JsonSerializer.Create();
            try
            {
                serializer.Deserialize(new JsonTextReader(new StringReader(jsonString)));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }


}

