using System;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PiggySync.FileMerger
{
	public class XmlElement
	{
		public XmlElement (string element)
		{
			if (Regex.IsMatch (element, "</\\S+>"))
			{
				IsClosing = true;
			}
			if (Regex.IsMatch (element, "<\\S+/>"))
			{
				if (IsClosing)
				{
					throw new Exception ("Not a valid node");
				} 
				IsClosed = true;
			}
			Name = element.Replace ("<", string.Empty).Replace (">", string.Empty).
				Replace ("/", string.Empty).Split(new char[0],StringSplitOptions.RemoveEmptyEntries).First();
		}

		public string Name { get; set; }

		public bool IsClosing { get; set; }

		public bool IsClosed { get; set; }

		public List<XmlElement> SubElements { get; set; }

		public override string ToString ()
		{
			return string.Format ("[XmlElement: Name={0}, IsClosing={1}, IsClosed={2}, SubElements={3}]", Name, IsClosing, IsClosed, SubElements);
		}
	}
}

