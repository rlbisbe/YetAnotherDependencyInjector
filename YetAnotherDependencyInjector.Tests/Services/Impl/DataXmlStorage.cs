using System.IO;
using System.Xml;

namespace YetAnotherDependencyInjector.Tests.Services.Impl
{
    public class DataXmlStorage : IDataStorage
    {
        public void Save(string note)
        {
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment,
                CloseOutput = false,
                Indent = true

            };

            using (XmlWriter writer = XmlWriter.Create("test.xml", settings))
            {
                writer.WriteStartElement("order");
                writer.WriteStartElement("elementData");
                writer.WriteString(note);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }
        }
    }
}