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
            TcpClient tcpClient = new TcpClient();
            for(int i=0;i<30;i++)
            {
                try{
                tcpClient.Connect("127.0.0.1", i);
                listOfPorts.Add(i);
                }
                catch (Exception){
                }
            }
            return listOfPorts;
        }
		
        //Method that checks IP address on local machine
		private void IpCheck(){
			string response="";
			textBlock.Text = "Checking IP addresses... ";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
					response = ip.ToString();
                }
            }
			textBlock.Text+="Your local IP is : " +response;
		}

        //Methods for any event on click items in list.
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
