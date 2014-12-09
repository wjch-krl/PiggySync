using System;
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
            throw new NotImplementedException();
        }

        internal string XmlToFile(string mergedXml, MergePattern pattern)
        {
            throw new NotImplementedException();
        }
    }
}

