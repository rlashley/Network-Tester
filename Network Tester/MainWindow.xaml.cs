using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;

namespace Network_Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string code="";

        public MainWindow()
        {
            InitializeComponent();
            ProgressBar ProgBar = new ProgressBar();
        }

        //Button click event, different methods triggered by selected item in the menu
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex.Equals(0))
            {
                IpCheck();
            }

            if (listBox1.SelectedIndex.Equals(1))
            {
                if (!inputBox.Text.Equals(""))
                {
                    string pass = password.Password;
                    code = inputBox.Text;
                    textBlock.Text=GenHash(code,pass);
                }
                else
                    textBlock.Text="The text box is empty. Please enter the information you would like to be encrypted.";
            }

            if (listBox1.SelectedIndex.Equals(4))
            {
                List<int> openPorts = new List<int>();
                openPorts = ServerConnect();
                foreach(int port in openPorts)
                {
                    textBlock.Text += "\nConnection has been made on port "+port+"\n";
                }
            }

            if (listBox1.SelectedIndex.Equals(5))
            {
                textBlock.Text="Hi Wifey! Just adding placeholders to the program.";
            }
        }

        //Button to clear out text box
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text="";
            inputBox.Text="";
        }

        //Method that generates a hash using SHA256
        private string GenHash(string cde, string Password){
            textBlock.Text="Initializing Code, serializing input to 256bit encrypted hash.";
            //Place body of serializing code

            string Salt = "FairyUnicornPrincess";
            string HashAlgorithm = "SHA1";
            int PasswordIterations = 2;
            string InitialVector = "OFRna73m*aze01xY";
            int KeySize = 256;

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(cde);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes);
            
            MemoryStream MemStream = new MemoryStream();
                
            CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write);
                    
            CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
            CryptoStream.FlushFinalBlock();
            CipherTextBytes = MemStream.ToArray();
            MemStream.Close();
            CryptoStream.Close();            
            SymmetricKey.Clear();

        return Convert.ToBase64String(CipherTextBytes);
        }

        private List<int> ServerConnect(){

            List<int> listOfPorts = new List<int>();
            int port = 10;
            TcpListener server = null;

            //Assign localhost ip address to ipa
            IPAddress ipa = Dns.GetHostAddresses("localhost")[1];
            try
            {

                server = new TcpListener(ipa, 10);
                server.Start();
                textBlock.Text += "\nConnection to server started...\n";

                TcpClient client = new TcpClient("localhost", port);
                listOfPorts.Add(port);
                client.Close();
                server.Stop();
            
            }
            catch (SocketException e) 
            {
                MessageBox.Show(e.Message);
            }
            return listOfPorts;
        }
		
        //Method that checks IP address on local machine
		private void IpCheck(){
			string response="";
			textBlock.Text = "Checking IP addresses... \n";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
					response = ip.ToString();
                }
            }
            textBlock.Text+="Your local Host Name is "+host.HostName+"\n";
			textBlock.Text+="Your local IP is : " +response;
		}

        //Methods for any event on click items in list.
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
