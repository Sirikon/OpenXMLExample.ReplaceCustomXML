using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Sirikon.OpenXMLExample.ReplaceCustomXML
{
    class CustomXMLWriter<T>
    {
        public void Write(MemoryStream memoryStream, T data)
        {
            using (var wordprocessingDocument = WordprocessingDocument.Open(memoryStream, true))
            {
                var mainPart = wordprocessingDocument.MainDocumentPart;
                var document = mainPart.Document;

                mainPart.DeleteParts(mainPart.CustomXmlParts);

                var customXml = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);

                using (var sw = new StreamWriter(customXml.GetStream()))
                {
                    sw.Write(GenerateXML(data));
                }
            }
        }

        public string GenerateXML(T data)
        {
            var xmlResult = "";
            var xmlSerializer = new XmlSerializer(typeof(T));

            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };

            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
                {
                    xmlSerializer.Serialize(xmlWriter, data);
                    memoryStream.Position = 0;
                }
                using (var sr = new StreamReader(memoryStream))
                {
                    xmlResult = sr.ReadToEnd();
                }
            }

            return xmlResult;
        }
    }
}
