using System;
using NUnit.Framework;
using PiggySync.FileMerger;

namespace DuckSync.Tests
{
	[TestFixture]
	public class ValidatorTest
	{
		[Test]
		public void ValidateXmlTest ()
		{
			string xml = "<PurchaseOrder xmlns:xsi=http://www.w3.org/2001/XMLSchema-instance xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"> \n <Items>   \n    <Item>\n        <ItemID>aaa111</ItemID>	\n      <ItemPrice>34.22</ItemPrice>	\n     <Item>	\n      <Item>\n          <ItemID>bbb222</ItemID>\n          <ItemPrice>2.89</ItemPrice>\n      <Item>\n   </Items>	\n  			</PurchaseOrder>";
			Assert.IsTrue (Validator.ValidateXML (ref xml));
		}
	}
}

