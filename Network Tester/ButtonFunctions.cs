using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Controls;

namespace Network_Tester
{
    class ButtonFunctions
    {


        public ButtonFunctions()
        {
            
        }

        //Method that generates a hash using SHA256
        public string GenHash(string cde, string Password)
        {

            //Place body of serializing code
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.GenerateKey();
            SymmetricKey.GenerateIV();

            // Encrypt the string to an array of bytes.
            byte[] encrypted = EncryptStringToBytes(cde, SymmetricKey.Key, SymmetricKey.IV);

            //Return the encrypted data
            MainWindow.AppWindow.Addtext(cde);
            return Encoding.Default.GetString(encrypted);
        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        //Method that checks IP address on local machine
        public void IpCheck()
        {
            string response = "";
            MainWindow.AppWindow.Addtext("Checking IP addresses...");
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    response = ip.ToString();
                }
            }
            MainWindow.AppWindow.Addtext("Your local Host Name is " + host.HostName);
            MainWindow.AppWindow.Addtext("Your local IP is : " + response);
        }

        public List<int> ServerConnect()
        {

            List<int> listOfPorts = new List<int>();
            int port = 10;
            TcpListener server = null;

            //Assign localhost ip address to ipa
            IPAddress ipa = Dns.GetHostAddresses("localhost")[1];
            try
            {

                server = new TcpListener(ipa, 10);
                server.Start();
                MainWindow.AppWindow.Addtext("Connection to server started...");

                TcpClient client = new TcpClient("localhost", port);
                listOfPorts.Add(port);
                client.Close();
                server.Stop();

            }
            catch (SocketException e)
            {
                MainWindow.AppWindow.Addtext(e.Message);
            }
            return listOfPorts;
        }

    }
}
