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
        public MainWindow()
        {
            InitializeComponent();
        }

        //Method for button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
			if(textBlock.SelectedIndex == 1){
				textBlock.Text += ipCheck();
			}
			if(textBlock.SelectedIndex == 2){
				
			}		
        }
		
        //Method that checks IP address on local machine
		private string ipCheck(){
			string response="";
			textBlock.Text += "Checking IP addresses! /n";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
					response = ip.ToString();
                }
            }
			return response;
		}

        //Methods for any event on click items in list.
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
