using NUnit.Framework;
using PiggySync.Domain;
using PiggySync.FileMerger;

namespace DuckSync.Tests
{
    [TestFixture]
    internal class ConverterTests
    {
        [Test]
        public void TestCSharpToXml()
        {
            var pattern = new MergePattern()
            {
                AggregateStartTag = "{",
                AggregateStopTag = "}",
                TagOpenString = new[] {"(",";",")"," ", "\t"}
            };
            var converter = new Converter(pattern);
            string csFile = @"namespace PiggySync.Domain
            {
                public class MergePattern
                {
                    public string AggregateStartTag { get; set; }
                    public string AggregateStopTag { get; set; }
                    public string[] TagOpenString { get; set; }

                }
            }";
            var result = converter.FileToXml(csFile);

            var converdedBack = converter.XmlToFile(result);
            StringAssert.AreEqualIgnoringCase(result,converdedBack);

        }

    }
}
