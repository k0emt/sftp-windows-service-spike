using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

using Renci.SshNet;

namespace ConsoleSftp
{
    class Program
    {
        static void Main(string[] args)
        {
            uploadFile();
        }

        public static void uploadFile()
        {
            string host = "HOSTNAME HERE";
            string username = "USERNAME HERE";
            string password = "PASSWORD HERE";
            string localFileName = @"c:\temp\sftp-testing\sample.txt";
            string localFilesDir = @"c:\temp\sftp-testing\";
            string localZipFileName = @"c:\temp\sftp-testing-spike.zip";

            zipSourceDirectory(localFilesDir, localZipFileName);

            //doSftp(host, username, password, localZipFileName);
        }

        private static void zipSourceDirectory(string localFilesDir, string localZipFileName)
        {
            ZipFile.CreateFromDirectory(localFilesDir, localZipFileName);
        }

        private static void doSftp(string host, string username, string password, string localZipFileName)
        {
            string remoteFileName = System.IO.Path.GetFileName(localZipFileName);
            using (var sftp = new SftpClient(host, username, password))
            {
                sftp.Connect();

                using (var file = File.OpenRead(localZipFileName))
                {
                    sftp.UploadFile(file, remoteFileName);
                }

                sftp.Disconnect();
            }
        }
    }
}