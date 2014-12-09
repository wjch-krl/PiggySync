using System;
using Newtonsoft.Json;
using System.Xml;

namespace PiggySync.FileMerger
{
	public class Converter
	{
		public Converter ()
		{
		}

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
	}
}

