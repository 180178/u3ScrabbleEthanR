/*
/Ethan Ross
/April 17 2018
/Program to take only words with length of 7 or less and write them to a new file, also removes the weird header words
*/
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
using System.IO;

namespace u3WordCutterEthanR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            InitializeComponent();
            StreamReader read = new StreamReader("words.txt");
            StreamWriter write = new StreamWriter("words7.txt");
            while(!read.EndOfStream)
            {
                string line = read.ReadLine();
                //Checks if the words is shorter than eight letters and isn't the single capital letter headers
                if (line.Length<8&&!alphabet.Contains(line))
                {
                    //MessageBox.Show(line);
                    write.Write(line+'\r'+'\n');
                }
            }
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start("words7.txt");
            write.Flush();
            write.Close();
            read.Close();
        }
    }
}
