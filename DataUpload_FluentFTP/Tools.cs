using System;
using System.IO;
using System.Xml.Serialization;

namespace klandolt.ch.DataTransferFluentFTP.DataUpload
{
    public static class Tools
    {
        /// <summary>
        /// Save DataUpload object to config.xml
        /// </summary>
        /// <param name="paramConfigs"></param>
        public static void SerializeToXml(DataUpload paramConfigs)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataUpload), new XmlRootAttribute("DataUpload"));
            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
            serializer.Serialize(textWriter, paramConfigs);
            textWriter.Close();
        }
        /// <summary>
        /// Read Config XML and create DataUpload Object
        /// </summary>
        /// <returns>The DataUpload Object from File</returns>
        public static DataUpload DeserializeFromXml()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DataUpload), new XmlRootAttribute("DataUpload"));
            TextReader textReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
            var configs = (DataUpload)deserializer.Deserialize(textReader);
            textReader.Close();

            return configs;
        }

        /// <summary>
        /// Translate the localpath to remotepath.
        /// </summary>
        /// <param name="paramFilePath">Full filepath of this file</param>
        /// <param name="paramInputPath">Start und input path</param>
        /// <param name="paramRemotePath">Current Remotepath</param>
        /// <returns>Translatet remotepath</returns>
        public static string TranslateLocalPathToRemote(string paramFilePath, string paramInputPath, string paramRemotePath)
        {
            if (!paramInputPath.EndsWith("\\"))
            {
                paramInputPath = paramInputPath + "\\";
            }
            var returnPath = paramFilePath.Replace(paramInputPath, paramRemotePath);
            returnPath = returnPath.Replace("\\", "/");

            return returnPath;
        }

    }
}
