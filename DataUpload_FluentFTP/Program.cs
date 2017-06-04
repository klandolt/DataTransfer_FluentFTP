using System;
using System.IO;
using FluentFTP;

namespace klandolt.ch.DataTransferFluentFTP.DataUpload
{
    class Program
    {
        static int Main(string[] args)
        {
            //Create Variables & Objects
            string inputPath = "";
            DataUpload config;
            FtpClient client = new FtpClient();
            
            try
            {
                
                //Kontrolle Input Argumente
                if (args.Length == 1)
                {
                    inputPath = args[0];
                }
                else if (args.Length > 1 || args.Length < 1)
                {
                    throw new ArgumentException();
                }
                //XML Import
                config = Tools.DeserializeFromXml();

                //Create FTP Client and Connect
                client = new FtpClient(config.HostName, config.UserName, config.Password);
                client.Connect();

                //Check is a file or directory
                if (File.Exists(inputPath))
                {
                    //File exist
                    Console.WriteLine("Datei existiert");
                    var inputFile = Path.GetFileName(inputPath);
                    Console.WriteLine($"Datei hochladen: {inputPath}");
                    client.UploadFile(inputPath, config.RemoteDirectory + inputFile, FtpExists.Overwrite, true);
                }
                else if (Directory.Exists(inputPath))
                {
                    //Directory exist
                    var inputDirectory = new DirectoryInfo(inputPath).Name;
                    if (!client.DirectoryExists(config.RemoteDirectory + inputDirectory))
                    {
                        Console.WriteLine($"Verzeichniss erstellen: {inputDirectory}");
                        client.CreateDirectory(inputDirectory);
                    }
                    else
                    {
                        Console.WriteLine($"Verzeichnis existiert bereits: {inputDirectory}");
                    }

                    //Update RemoteBase:
                    var remotebasePath = config.RemoteDirectory + inputDirectory + "/";
                    // Enumerate files and directories to upload
                    var fileInfos = new DirectoryInfo(inputPath).EnumerateFileSystemInfos("*",
                        SearchOption.AllDirectories);

                    foreach (FileSystemInfo fileInfo in fileInfos)
                    {
                        string remoteFilePath = Tools.TranslateLocalPathToRemote(fileInfo.FullName, inputPath,
                            remotebasePath);

                        if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
                        {
                            // Create remote subdirectory, if it does not exist yet;
                            if (!client.DirectoryExists(remoteFilePath))
                            {
                                Console.WriteLine($"Verzeichniss erstellen: {remoteFilePath}");
                                client.CreateDirectory(remoteFilePath);
                            }
                            else
                            {
                                Console.WriteLine($"Verzeichnis existiert bereits: {remoteFilePath}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Datei erstellen: {remoteFilePath}");
                            client.UploadFile(fileInfo.FullName, remoteFilePath, FtpExists.Overwrite, true);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return e.HResult;
            }
            finally
            {
                client.Disconnect();
            }
            return 0;
        }
    }
}
