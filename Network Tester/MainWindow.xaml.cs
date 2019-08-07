using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Network_Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string code = "";
        ButtonFunctions buttonFunctions = new ButtonFunctions();
          
        public MainWindow()
        {
            DataContext = new DataBinder();
            InitializeComponent();
            ProgressBar ProgBar = new ProgressBar();
        }
        

        //Button click event, different methods triggered by selected item in the menu
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //IP Check function
            if (listBox1.SelectedIndex.Equals(0))
            {
                buttonFunctions.IpCheck();
            }

            //Generate encrypted hash function
            if (listBox1.SelectedIndex.Equals(1))
            {
                if (!inputBox.Text.Equals(""))
                {
                    viewModel.Textblock ="Initializing Code, serializing input to 256bit encrypted hash.";
                    string pass = password.Password;
                    code = inputBox.Text;
                    viewModel.Textblock = buttonFunctions.GenHash(code, pass);
                }
                else
                    Addtext("The text box is empty. Please enter the information you would like to be encrypted.");
            }

            //Server connection function
            if (listBox1.SelectedIndex.Equals(4))
            {
                List<int> openPorts = new List<int>();
                openPorts = buttonFunctions.ServerConnect();
                foreach (int port in openPorts)
                {
                    viewModel.Textblock ="Connection has been made on port " + port";
                }
            }

            //Daisy button(Place holder for now, future features will go here)
            if (listBox1.SelectedIndex.Equals(5))
            {
                Addtext("Hi Wifey! Just adding placeholders to the program.");
            }
        }

        //Button to clear out text box
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            inputBox.Text = "";
            password.Clear();
        }

        //Methods for any event on click items in list.
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
    
    public class DataBinder : INotifyPropertyChanged
    {
        string _textBlock = "Empty";
 
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public string Textblock
        {
            get { return _textBlock; }
            set { _textBlock = value; 
            OnPropertyChanged("Textblock");
            }
        }
    }
}
