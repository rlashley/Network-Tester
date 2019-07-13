using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Network_Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected string code="";

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
                GenHash(code);
            }

            if (listBox1.SelectedIndex.Equals(4))
            {
                List<int> openPorts = new List<int>();
                openPorts = PortOpen();
                foreach(int port in openPorts)
                {
                textBlock.Text += port +" is open\n";
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
        }

        //Method that generates a hash using SHA256
        private string GenHash(string cde){
            textBlock.Text="Initializing Code Beep boop bop, serializing input to 256bit encrypted hash";
            //Place body of serializing code
            string hash = "Initializing";
            return hash;
        }

        private List<int> PortOpen(){

            List<int> listOfPorts = new List<int>();
            //Assign localhost ip address to ipa
            IPAddress ipa = Dns.GetHostAddresses("localhost")[1];

            //Loop through ports below. In future, change i to variable and query user for port range
            for(int i=1;i<10;i++)
            {
                try
                {
                 //Create new socket and connect to ip address and socket
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(ipa, i);
                if (sock.Connected)
                    listOfPorts.Add(i);
                sock.Close();     
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ProgBar.Value++;
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
