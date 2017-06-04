using System;
using System.Xml.Serialization;

namespace klandolt.ch.DataTransferFluentFTP.DataUpload
{
    /*XML Sample
     * <DataUpload>
     *  <HostName></HostName>
     *  <UserName></UserName>
     *  <Password></Password>
     *  <RemoteDirectory></RemoteDirectory>
     * </DataUpload>
     */

    /// <summary>
    /// DataUpload object
    /// </summary>
    [Serializable]
    [XmlRoot("DataUpload")]
    public class DataUpload
    {

        #region XML Parameter
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

        public DataUpload(string paramHostName, string paramUserName, string paramPassword, string paramRemoteDirectory)
        {
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
