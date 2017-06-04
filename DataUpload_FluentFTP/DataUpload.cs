using System;
using System.Xml.Serialization;

namespace klandolt.ch.DataTransferFluentFTP.DataUpload
{
    /*XML Sample
     * <DataUpload>
     *  <Protocol>FTP</Protocol>
     *  <HostName></HostName>
     *  <UserName></UserName>
     *  <Password></Password>
     *  <RemoteDirectory></RemoteDirectory>
     * </DataUpload>
     */

    [Serializable]
    [XmlRoot("DataUpload")]
    public class DataUpload
    {

        #region XML Parameter
        [XmlElement("Protocol")]
        public string Protocol { get; set; }
        [XmlElement("HostName")]
        public string HostName { get; set; }
        [XmlElement("UserName")]
        public string UserName { get; set; }
        [XmlElement("Password")]
        public string Password { get; set; }
        [XmlElement("RemoteDirectory")]
        public string RemoteDirectory { get; set; }
        #endregion

        #region Constructor

        public DataUpload(string paramProtocol, string paramHostName, string paramUserName, string paramPassword, string paramRemoteDirectory)
        {
            Protocol = paramProtocol;
            HostName = paramHostName;
            UserName = paramUserName;
            Password = paramPassword;
            RemoteDirectory = paramRemoteDirectory;
        }
        public DataUpload()
        {

        }
        #endregion
    }
}
