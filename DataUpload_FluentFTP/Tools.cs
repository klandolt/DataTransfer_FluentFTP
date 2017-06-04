using System;
using System.IO;
using System.Xml.Serialization;

namespace klandolt.ch.DataTransferFluentFTP.DataUpload
{
    public static class Tools
    {
        public static void SerializeToXml(DataUpload paramConfigs)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataUpload),
             new XmlRootAttribute("DataUpload"));
            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
            serializer.Serialize(textWriter, paramConfigs);
            textWriter.Close();
        }
        public static DataUpload DeserializeFromXml()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DataUpload),
             new XmlRootAttribute("DataUpload"));
            TextReader textReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
            DataUpload configs;
            configs = (DataUpload)deserializer.Deserialize(textReader);
            textReader.Close();

            return configs;
        }

        public static string TranslateLocalPathToRemote(string paramFilePath, string paramInputPath, string paramRemotePath)
        {
            return paramFilePath.Replace(paramInputPath,paramRemotePath);
        }

    }
}
