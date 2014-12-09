using System;
using System.Xml;
using System.Xml.Schema;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace PiggySync.FileMerger
{
	public static class Validator
	{
		public static bool ValidateXML (ref string xmlString)
		{
		//	try
		//	{
				//var tagsText = Regex.Matches (xmlString, "<[^>]+>");
//				var tags = tagsText.Cast<Match> ().Select (x => new XmlElement (x.ToString ()));
//				var root = tags.First ();
//				root.SubElements = BuildTree (tags.ToList ());
//				return ValidateTree (root);
				try
				{
					XmlDocument x = new XmlDocument ();
					x.LoadXml (xmlString);
					return true;
				}
				catch (Exception e)
				{
					return false;
				}
//			}
//			catch (Exception e)
//			{
//				return false;
//			}
		}

		public static List<XmlElement> BuildTree (IList<XmlElement> elements)
		{
			List<XmlElement> subElements = new List<XmlElement> ();
			if (elements == null || elements.Count == 0)
			{
				return subElements;
			}
			var currentRoot = elements.First ();
			int count = 0;
			if (elements == null || elements.Count == 0)
			{
				return new List<XmlElement> ();
			}
			var closedNodes = elements.Where (x => x.IsClosed).ToList ();
			for (int i = 1; i < elements.Count; i++)
			{
				if (i < elements.Count - 1 && elements [i].Name == elements [i + 1].Name)
				{
					closedNodes.Add (elements [i]);
					continue;
				}
				if (elements [i].Name == currentRoot.Name && count == 0 && elements [i].IsClosing)
				{
					closedNodes.Add (elements[i]);
					break;
				}
			}
			currentRoot.SubElements = BuildTree (subElements);
			return closedNodes;
		}

		static bool ValidateTree (XmlElement root)
		{
			if (root.SubElements
				.Where (x => !x.IsClosing)
				.GroupBy (x => x.Name)
				.Where (g => g.Count () > 1)
				.Select (y => y.Key)
				.Count () > 0)
			{
				return false;
			}
			foreach (var element in root.SubElements)
			{
				if (!ValidateTree (element))
				{
					return false;
				}
			}
			return true;
		}

		public static bool ValidateJson (ref string jsonString)
		{
			//	var validator = new JsonValidatingReader ( );
			return false;
		}

	}


}

