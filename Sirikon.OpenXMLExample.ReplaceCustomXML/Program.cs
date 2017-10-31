using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sirikon.OpenXMLExample.ReplaceCustomXML
{
    [XmlRoot("data")]
    public class DocumentData
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }

    class Program
    {
        static bool CheckArgs (string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Wrong arguments, expected template file path and output path");
                return false;
            }

            return true;
        }

        static FileStream GetReadFileStream (string filePath)
        {
            return new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read);
        }

        static FileStream GetWriteFileStream(string filePath)
        {
            return new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write);
        }

        static void Main(string[] args)
        {
            if (!CheckArgs(args)) return;

            var templateFilePath = args[0];
            var outputFilePath = args[1];

            using (var memoryStream = new MemoryStream())
            using (var templateFileStream = GetReadFileStream(templateFilePath))
            using (var outputFileStream = GetWriteFileStream(outputFilePath))
            {
                templateFileStream.CopyTo(memoryStream);

                var customXMLWriter = new CustomXMLWriter<DocumentData>();
                customXMLWriter.Write(memoryStream, new DocumentData { Name = "Peter" });

                memoryStream.Position = 0;
                memoryStream.CopyTo(outputFileStream);
            }
        }
    }
}
