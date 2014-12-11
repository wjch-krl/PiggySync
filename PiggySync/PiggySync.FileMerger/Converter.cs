using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml;
using PiggySync.Domain;
using PiggySync.Model;

namespace PiggySync.FileMerger
{
	public class Converter
	{
	    private readonly MergePattern pattern;

	    public Converter(MergePattern pattern)
	    {
	        this.pattern = pattern;
	    }

		public static string XmlToJson (string xml)
		{
			var doc = new XmlDocument ();
			doc.LoadXml (xml);
			return JsonConvert.SerializeXmlNode (doc);
		}

		public static string JsonToXml (string json)
		{
			XmlDocument doc = JsonConvert.DeserializeXmlNode (json);
			return doc.ToString ();
		}

	    public string FileToXml(string text1)
	    {
	        text1 = text1.Replace(pattern.AggregateStartTag, string.Format("\0{0}\0", pattern.AggregateStartTag));
            text1 = text1.Replace(pattern.AggregateStopTag, string.Format("\0{0}\0", pattern.AggregateStopTag));

	        var lines =
	            text1.Split(new string[] {Environment.NewLine, "\0"},
	                StringSplitOptions.RemoveEmptyEntries).Where(x => !String.IsNullOrWhiteSpace(x)).ToList();
	        if (lines.Count < 1)
	        {
	            throw new PiggyFileException("Can't convert empty file");
	        }

	        var root = new XElement("root",BuildXml(lines));
	        return root.ToString();
	    }

        private IEnumerable<XElement> BuildXml(IEnumerable<string> lines)
        {
            var nodes = new List<XElement>();
            var subnodes = new List<string>();
            bool foundAggregation = false;
            int counter = 0;
            foreach (var line in lines)
            {
                if (!foundAggregation)
                {
                    if (!line.Contains(pattern.AggregateStartTag))
                    {
                        string[] split = line.Split(pattern.TagOpenString, StringSplitOptions.RemoveEmptyEntries);
                        nodes.Add(new XElement(split[0], line.Replace(split[0], string.Empty)));
                    }
                    else
                    {
                        foundAggregation = true;
                        counter++;
                    }
                }
                else
                {
                    if (line == pattern.AggregateStartTag)
                        counter++;
                    else if (line == pattern.AggregateStopTag)
                        counter--;
                    
                    if (counter == 0)
                    {
                        nodes.Last().Add(BuildXml(subnodes));
                        subnodes.Clear();
                        foundAggregation = false;
                        continue;
                    }
                    subnodes.Add(line);
                }
            }
            return nodes;
            //return new XElement(split[0], name.Replace(split[0],string.Empty), nodes);
        }


	    public string XmlToFile(string mergedXml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(mergedXml);
            return SerializeXml(doc);
        }

	    private string SerializeXml(XmlNode doc)
	    {
	        string result = string.Empty;
	        bool hasChildNodes = doc.ChildNodes.Cast<XmlNode>().Any(node => node.NodeType != XmlNodeType.Text);
            result += doc.NodeType != XmlNodeType.Text ? doc.Name : string.Empty;
            result += doc.Value;
            result = doc.ChildNodes.Cast<XmlNode>().Where(x => doc.NodeType == XmlNodeType.Text).Aggregate(result, (current, node) => current + SerializeXml(node));
            if (hasChildNodes)
	        {
	            result += string.Format("{0}{1}",pattern.AggregateStartTag, "\n");
	        }
            result = doc.ChildNodes.Cast<XmlNode>().Where(x => doc.NodeType != XmlNodeType.Text).Aggregate(result, (current, node) => current + SerializeXml(node));
	        if (hasChildNodes)
	        {
                result += string.Format("{0}{1}",pattern.AggregateStopTag, "\n");
            }
	        return result;
	    }
	}
}

