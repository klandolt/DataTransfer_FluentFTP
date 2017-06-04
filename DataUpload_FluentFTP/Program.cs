using System;
using System.IO;
using FluentFTP;

namespace klandolt.ch.DataTransferFluentFTP.DataUpload
{
    class Program
    {
        static int Main(string[] args)
        {
            //Create variables & objects
            string inputPath = "";
            FtpClient client = new FtpClient();

            Console.WriteLine($"Aplikations-Verzeichnis: {System.Reflection.Assembly.GetExecutingAssembly().Location}");

            try
            {
                //Control input argument
                if (args.Length == 1)
                {
                    inputPath = args[0];
                    Console.WriteLine($"Input Argument: {inputPath}");
                }
                else if (args.Length > 1 )
                {
                    Console.WriteLine("Mehr als 1 Input Argument!");
                    throw new ArgumentException();
                }
                else if (args.Length < 1)
                {
                    Console.WriteLine("Kein Argument!");
                    throw new ArgumentNullException();
                }
                //XML config import
                var config = Tools.DeserializeFromXml();

                //Create FTP Client and Connect
                client = new FtpClient(config.HostName, config.UserName, config.Password);
                client.Connect();

                //Check if is a file or directory
                if (File.Exists(inputPath))
                {
                    //Is file and exist
                    Console.WriteLine("Input ist eine Datei und existiert:");
                    var inputFile = Path.GetFileName(inputPath);
                    Console.WriteLine($"Datei hochladen: {inputPath}");
                    client.UploadFile(inputPath, config.RemoteDirectory + inputFile);
                }
                else if (Directory.Exists(inputPath))
                {
                    //Is directory and exist
                    Console.WriteLine("Input ist ein Verzeichnis und existiert:");
                    var inputDirectory = new DirectoryInfo(inputPath).Name;
                    //Update remoteBase:
                    var remotebasePath = config.RemoteDirectory + inputDirectory + "/";
                    // Enumerate files and directories to upload
                    var fileInfos = new DirectoryInfo(inputPath).EnumerateFiles("*", SearchOption.AllDirectories);
                    
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string remoteFilePath = Tools.TranslateLocalPathToRemote(fileInfo.FullName, inputPath,
                            remotebasePath);
                        Console.WriteLine($"Datei hochladen: {remoteFilePath}");
                        client.UploadFile(fileInfo.FullName, remoteFilePath, FtpExists.Overwrite, true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return e.HResult;
            }
            finally
            {
                client.Disconnect();
            }
#if DEBUG
            Console.WriteLine("Press Enter....");
            Console.ReadLine();
#endif
            return 0;
        }
    }
}
