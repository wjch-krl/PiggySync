using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using PiggySync.Domain;
using PiggySync.Model;

namespace PiggySync.FileMerger
{
    public class FileMerger
    {
        private readonly string fileAPath;
        private readonly string fileBPath;
        private readonly string resultPath;
        private MergePattern pattern;
        private string extension;

        public FileMerger(string fileAPath, string fileBPath, string resultPath)
        {
            if ((extension = Path.GetExtension(fileAPath)) != Path.GetExtension(fileBPath))
            {
                throw new PiggyFileException("File extensions dosn't math");
            }
            
            this.fileAPath = fileAPath;
            this.fileBPath = fileBPath;
            this.resultPath = resultPath;
        }

        public bool MergeFiles()
        {
            var file = FileTools.TextFile(extension, fileAPath, fileBPath);
            if (file == null)
            {
                throw new PiggyFileException("Not a text file");
            }
			if (file.Pattern != null)
            {
				pattern = file.Pattern;
            }
            switch (extension)
            {
                case ".xml":
                    return MergeXmlFiles(); // && Validator.va;
                case ".json":
                    return MergeJsonFile();
                default:
                    return pattern != null ? ConvertAndMerge(pattern) : MergeTextFiles();
            }
        }

        private bool ConvertAndMerge(MergePattern pattern)
        {
            try
            {
                var converter = new Converter(pattern);
                var text1 = File.ReadAllText(fileAPath);
                var text2 = File.ReadAllText(fileAPath);

                var xmlReader1 = new XmlTextReader(new StringReader(converter.FileToXml(text1)));
                var xmlReader2 = new XmlTextReader(new StringReader(converter.FileToXml(text2)));

                var mergedXml = MergeXmlStreams(xmlReader1, xmlReader2);
                var mergedText = converter.XmlToFile(mergedXml);
                File.WriteAllText(resultPath, mergedText);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private bool MergeTextFiles()
        {
            string fileA = File.ReadAllText(fileAPath);
            string fileB = File.ReadAllText(fileBPath);
            string merged = String.Empty;

            var differ = new Differ();
            var inlineBuilder = new InlineDiffBuilder(differ);
            var diferenceA = inlineBuilder.BuildDiffModel(fileB, fileA);

            foreach (var diffPieceA in diferenceA.Lines)
            {
                if (diffPieceA.Type == ChangeType.Inserted)
                {
                    merged = String.Format("{0}\n{1}", merged, diffPieceA.Text);
                }
                else if (diffPieceA.Type != ChangeType.Deleted)
                {
                    merged += String.Format("{0}\n", diffPieceA.Text);
                }
            }
            File.WriteAllText(resultPath, merged);
            return true;
        }

        private bool MergeXmlFiles()
        {
            try
            {
                var xmlReader1 = new XmlTextReader(fileAPath);
                var xmlReader2 = new XmlTextReader(fileAPath);

                var merged = MergeXmlStreams(xmlReader1, xmlReader2);
                File.WriteAllText(resultPath, merged);
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

        private string MergeXmlStreams(XmlTextReader xmlreader1, XmlTextReader xmlreader2)
        {
//            var xmlDocument1 = XDocument.Load(xmlreader1);
//            var xmlDocument2 = XDocument.Load(xmlreader2);
//            var combinedUnique = xmlDocument1.Descendants()
//                          .Union(xmlDocument2.Descendants());
//            return combinedUnique.ToString();
            var ds = new DataSet();
            ds.ReadXml(xmlreader1);
            var ds2 = new DataSet();
            ds2.ReadXml(xmlreader2);
            ds.Merge(ds2,false);
            return ds.GetXml();
        }

        private bool MergeJsonFile()
        {
            try
            {
                var json1 = File.ReadAllText(fileAPath);
                var json2 = File.ReadAllText(fileAPath);

                var xmlReader1 = new XmlTextReader(new StringReader(Converter.JsonToXml(json1)));
                var xmlReader2 = new XmlTextReader(new StringReader(Converter.JsonToXml(json2)));

                var mergedXml = MergeXmlStreams(xmlReader1, xmlReader2);
                var mergedJson = Converter.XmlToJson(mergedXml);
                File.WriteAllText(resultPath, mergedJson);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}