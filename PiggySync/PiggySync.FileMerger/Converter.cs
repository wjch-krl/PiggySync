using System;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;
using PiggySync.Domain;

namespace PiggySync.FileMerger
{
	public class Converter
	{
		public string XmlToJson (string xml)
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (xml);
			return JsonConvert.SerializeXmlNode (doc);
		}

		public string JsonToXml (string json)
		{
			XmlDocument doc = JsonConvert.DeserializeXmlNode (json);
			return doc.ToString ();
		}

        internal string FileToXml(string text1, MergePattern pattern)
        {
            string[] lines = text1.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            XmlDocument doc = new XmlDocument();
      //      doc.CreateElement()
            foreach (var element in lines)
            {
                string[] tokens = element.Split(new char[0],StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Contains(pattern.AggregateStartTag))
                {
                    
                }
            }
            return string.Empty;
        }

        internal string XmlToFile(string mergedXml, MergePattern pattern)
        {
            throw new NotImplementedException();
        }
    }
}

