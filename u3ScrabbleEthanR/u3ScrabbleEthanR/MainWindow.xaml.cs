/*
/Ethan Ross
/April 18 2018
/Program to Cheat at Scrabble
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

namespace u3ScrabbleEthanR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ultimateWord;
        int maxtotal = 0;
        bool check;//If word contains a letter it shouldn't
        bool check2;//Makes sure a given letter apears the same number of times in the word and your letter 'hand'
        bool check3;//Relates to blank tiles
        bool debug_test = false;//Change to true if want to step through the program for blank tiles, set seed to 2 to test with a blank space
        string totalletters;
        string letters;
        StreamReader reader = new StreamReader("words7.txt");
        StreamWriter writer = new StreamWriter("CorrectWords.txt");
        
        public MainWindow()
        {
            InitializeComponent();
            ScrabbleGame sg = new ScrabbleGame();
            //Draws upper case letters
            letters = sg.drawInitialTiles().ToUpper();
            //Creates Alphabet without drawn tile letters
            for (int i = 65;i<65+26;i++)
            {
                char c = Convert.ToChar(i);
                Console.WriteLine("Letter: "+ c);
                if (!letters.Contains(c))
                {
                     totalletters += c;
                }
            }
            //Outputs tiles you have and the alphabet without them
            if(debug_test == true)
            {
                MessageBox.Show(totalletters, letters);
            }
            MessageBox.Show("Your Tiles are : " + letters);
            try
            {
                while (!reader.EndOfStream)
                {
                    check = true;
                    check2 = true;
                    check3 = false;
                    string line = reader.ReadLine().ToUpper();
                    //string lineUpper = line.ToUpper();
                    //Checks for situations involving a blank tile.
                    if (letters.Contains(' '))
                    {
                        if(line=="DAFT"&&debug_test==true)
                        {
                            MessageBox.Show(line);
                        }
                        int blank_count = 0;
                        int length = letters.Length;
                        int linelength = line.Length;
                        foreach (char c in letters)
                        {
                            if (c == ' ')
                            {
                                blank_count++;
                            }
                        }
                        string newline = line;
                        
                        for (int i = 0; i < 7; i++)
                        {
                            //Removes letters until your tiles run out
                            if (newline.Contains(letters[i]))
                            {
                                if(line=="DAFT" && debug_test == true)
                                {
                                   MessageBox.Show(newline + " Contains " + letters[i]);
                                }
                                int index = newline.IndexOf(letters[i]);
                                newline = newline.Remove(index, 1);
                                if(line=="DAFT" && debug_test == true)
                                {
                                   MessageBox.Show(newline);
                                }
                            }
                        }
                        //Checks if cut down word is the same length as the number of blank tiles
                        if (newline.Length <= blank_count)
                        {
                            check3 = true;
                        }
                        else
                        {
                            check3 = false;
                        }
                    }
                    else
                    {
                        check3 = false;
                    }
                    //Checks if letters left are in word
                    for (int i = 0; i < totalletters.Length; i++)
                    {
                        if (line.Contains(totalletters[i]))
                        {
                            check = false;
                        }
                    }
                    //Following code check if the number of times a given letter appears is the same in the word and your 'hand'
                    for (int i = 0; i < 7; i++)
                    {
                        int count1 = 0;
                        int count2 = 0;
                        foreach (char c in line)
                        {
                            if (c == letters[i])
                            {
                                count1++;
                            }
                        }
                        //Console.WriteLine("There is " + count1 + " " + letters[i] + " in " + line);
                        //The commented console lines were for testing
                        foreach (char c in letters)
                        {
                            if (c == letters[i])
                            {
                                count2++;
                            }
                        }
                        //Console.WriteLine("There is " + count2 + " " + letters[i] + " in " + letters);
                        if (count1 <= count2)
                        {
                            //Console.WriteLine("The Same" + '\n');
                        }
                        else
                        {
                            check2 = false;
                            //Console.WriteLine("Different" + '\n');
                        }
                    }

                    //Writes to file if all peramiters are correct
                    if ((check == true && check2 == true) || check3 == true)
                    {
                        //Gets largest score possible and word that corresponds to it
                        int total = 0;
                        foreach (char f in line)
                        {
                            ScrabbleLetter letter = new ScrabbleLetter(f);
                            int points = letter.Points;
                            total += points;
                        }
                        if (total > maxtotal)
                        {
                            ultimateWord = line;
                            maxtotal = total;
                        }
                        //Writes correct words to the file
                        writer.Write(line + '\r' + '\n');
                    }
                }
                writer.Flush();
                writer.Close();
                reader.Close();
                MessageBox.Show("Max Points Was: " + maxtotal + '\n'+"With the Word "+ultimateWord);
                MessageBox.Show("Job Done, Press Enter to see Word List");
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start("CorrectWords.txt");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}