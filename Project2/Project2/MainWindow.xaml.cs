/*
 * Babble Program - Program #2
 * CS 212 - Due October 16
 * Professor Plantinga provided some of the code
 * Ethan Clark wrote the functions dealing with the babbling of the text file
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BabbleSample
{
    /// Babble framework
    /// Starter code for CS212 Babble assignment
    public partial class MainWindow : Window
    {
        private string input;               // input file
        private string[] words;             // input file broken into array of words
        private int wordCount = 200;        // number of words to babble
        private Random random_index = new Random();     //Random type to determine random number
        private int Order_n = 0;            // int variable to keep track of order of statistics
        private string current_word;        // string variable to keep track of the current_word for the key lookup
        char[] delimiterChars = { '-' };    // array only containing the hypen character, used to split the key word
        string[] split_words;               // array that contains the split words from the key word
        int number_words = 0;                   // int that keeps track of the number of words from the text file
        int number_uniqueWords = 0;             // int that keeps track of the number of unique words from the text file

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.FileName = "Sample"; // Default file name
            ofd.DefaultExt = ".txt"; // Default file extension
            ofd.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            if ((bool)ofd.ShowDialog())
            {
                textBlock1.Text = "Loading file " + ofd.FileName + "\n";
                input = System.IO.File.ReadAllText(ofd.FileName);  // read file
                words = Regex.Split(input, @"\s+");       // split into array of words
            }
        }

        private void analyzeInput(int order)
        {
            if (order > 0)
            {
                MessageBox.Show("Analyzing at order: " + order);
            }
        }

        //Function that determines what order statistics is called and calls the corresponding order function
        private void babbleButton_Click(object sender, RoutedEventArgs e)
        {

            //Clear the current words that are in the big TextBlock
            textBlock1.Text = String.Empty;

            //Clear the number of words and number of unique words text blocks
            numberUniquewords.Text = " ";
            numberWords.Text = " ";
            number_uniqueWords = 0;
            number_words = 0;

            //If we want order 0 statistics
            if (Order_n == 0)
            {
                Order_Zero();
            }
            //If we want order 1 statistics
            if (Order_n == 1)
            {
                Order_One();
            }

            //If we want order 2 statistics
            if (Order_n == 2)
            {
                Order_Two();
            }

            //If we want order 3 statistics
            if (Order_n == 3)
            {
                Order_Three();
            }

            //If we want order 4 statistics
            if (Order_n == 4)
            {
                Order_Four();
            }

            //If we want order 5 statistics
            if (Order_n == 5)
            {
                Order_Five();
            }
        }

        //Function that performs if Order Zero is called
        //Order_Zero writes out the first 200 words from the text file
        private void Order_Zero()
        {
            //For loop that prints the first 200 words from the text file
            for (int i = 0; i < Math.Min(wordCount, words.Length); i++)
            {
                textBlock1.Text += " " + words[i];
            }
        }

        //Function that performs if Order One is called
        private void Order_One()
        {
            //Create a hashTable to put all the words as a key, and the following word as a value.
            Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

            //Enter all the words and the following word as key, value pairs
            for (int i = 0; i < words.Count() - 1; i++)
            {
                number_words++;
                string firstword = words[i];
                if (!hashTable.ContainsKey(firstword))
                {
                    hashTable.Add(firstword, new ArrayList());
                    number_uniqueWords++;
                }

                hashTable[firstword].Add(words[i + 1]);
            }

            //Update the number of words and the number of unique words to the screen
            numberUniquewords.Text += Convert.ToString(number_uniqueWords) + " unique words";
            numberWords.Text += Convert.ToString(number_words) + " words";

            //Choose a starting point to write out to the window screen
            current_word = words[0];
            textBlock1.Text += " " + current_word;

            //For loop that prints out 200 words of "random" text from the file, based on the keys and their values
            for (int i = 0; i < Math.Min(wordCount, words.Length); i++)
            {
                string random_word = Babble_Function(hashTable);
                textBlock1.Text += " " + random_word;
                current_word = random_word;
            }
        }

        //Function that performs if Order Two is called
        private void Order_Two()
        {
            //Create a hashTable to put all the words as a key, and the following word as a value.
            Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

            //Enter all the 2 word pairs (that appear in a row) as keys and the following word as its value
            for (int i = 0; i < words.Count() - 2; i++)
            {
                string firstword = words[i] + "-" + words[i+1];
                number_words++;
                if (!hashTable.ContainsKey(firstword))
                {
                    hashTable.Add(firstword, new ArrayList());
                    number_uniqueWords++;
                }

                hashTable[firstword].Add(words[i + 2]);
            }

            //Update the number of words and the number of unique words to the screen
            numberUniquewords.Text += Convert.ToString(number_uniqueWords) + " unique sequences";
            numberWords.Text += Convert.ToString(number_words) + " sequence of words";

            //Choose a starting point to write out to the window screen
            current_word = words[0] + " " + words[1];
            textBlock1.Text += " " + current_word;

            current_word = words[0] + "-" + words[1];

            //For loop to write out 200 "random" words from the text file based on keys and their values
            for (int i = 0; i < Math.Min(wordCount - 1, words.Length); i++)
            {
                string random_word = Babble_Function(hashTable);
                textBlock1.Text += " " + random_word;
                split_words = current_word.Split(delimiterChars);
                current_word = split_words[1] + "-" + random_word;
            }
        }

        //Function that performs if Order Three is called
        private void Order_Three()
        {
            //Create a hashTable to put all the words as a key, and the following word as a value.
            Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

            //Enter all the 3 word pairs (that appear in a row) as keys and the following word as its value
            for (int i = 0; i < words.Count() - 3; i++)
            {
                string firstword = words[i] + "-" + words[i + 1] + "-" + words[i + 2];
                number_words++;
                if (!hashTable.ContainsKey(firstword))
                {
                    hashTable.Add(firstword, new ArrayList());
                    number_uniqueWords++;
                }

                hashTable[firstword].Add(words[i + 3]);
            }

            //Update the number of words and the number of unique words to the screen
            numberUniquewords.Text += Convert.ToString(number_uniqueWords) + " unique sequences";
            numberWords.Text += Convert.ToString(number_words) + " sequence of words";

            //Choose a starting point to write out to the window screen
            current_word = words[0] + " " + words[1] + " " + words[2];
            textBlock1.Text += " " + current_word;

            current_word = words[0] + "-" + words[1] + "-" + words[2];

            //For loop that prints out 200 "random" words from the text file, based on their key and value pairs
            for (int i = 0; i < Math.Min(wordCount - 2, words.Length); i++)
            {
                string random_word = Babble_Function(hashTable);
                textBlock1.Text += " " + random_word;
                split_words = current_word.Split(delimiterChars);
                current_word = split_words[1] + "-" + split_words[2] + "-" + random_word;
            }
        }

        //Function that performs if Order Four is called
        private void Order_Four()
        {
            //Create a hashTable to put all the words as a key, and the following word as a value.
            Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

            //Enter all the 4 word pairs (that appear in a row) as keys and the following word as its value
            for (int i = 0; i < words.Count() - 4; i++)
            {
                string firstword = words[i] + "-" + words[i + 1] + "-" + words[i + 2] + "-" + words[i + 3];
                number_words++;
                if (!hashTable.ContainsKey(firstword))
                {
                    hashTable.Add(firstword, new ArrayList());
                    number_uniqueWords++;
                }

                hashTable[firstword].Add(words[i + 4]);
            }

            //Update the number of words and the number of unique words to the screen
            numberUniquewords.Text += Convert.ToString(number_uniqueWords) + " unique sequences";
            numberWords.Text += Convert.ToString(number_words) + " sequence of words";

            //Choose a starting point to write out to the window screen
            current_word = words[0] + " " + words[1] + " " + words[2] + " " + words[3];
            textBlock1.Text += " " + current_word;

            current_word = words[0] + "-" + words[1] + "-" + words[2] + "-" + words[3];

            //For loop that prints out 200 "random" words from the text value, based on keys and their values
            for (int i = 0; i < Math.Min(wordCount - 3, words.Length); i++)
            {
                string random_word = Babble_Function(hashTable);
                textBlock1.Text += " " + random_word;
                split_words = current_word.Split(delimiterChars);
                current_word = split_words[1] + "-" + split_words[2] + "-" + split_words[3] + "-" + random_word;
            }
        }

        //Function that performs if Order Five is called
        private void Order_Five()
        {
            //Create a hashTable to put all the words as a key, and the following word as a value.
            Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

            //Enter all the 5 word pairs (that appear in a row) as keys and the following word as its value
            for (int i = 0; i < words.Count() - 5; i++)
            {
                string firstword = words[i] + "-" + words[i + 1] + "-" + words[i + 2] + "-" + words[i + 3] + "-" + words[i + 4];
                number_words++;
                if (!hashTable.ContainsKey(firstword))
                {
                    hashTable.Add(firstword, new ArrayList());
                    number_uniqueWords++;
                }

                hashTable[firstword].Add(words[i + 5]);
            }
            //Update the number of words and the number of unique words to the screen
            numberUniquewords.Text += Convert.ToString(number_uniqueWords) + " unique sequences";
            numberWords.Text += Convert.ToString(number_words) + " sequence of words";


            //Choose a starting point to write out to the window screen
            current_word = words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4];
            textBlock1.Text += " " + current_word;

            current_word = words[0] + "-" + words[1] + "-" + words[2] + "-" + words[3] + "-" + words[4];

            //For loop that prints out 200 "random" words from the text file, based on keys and their values
            for (int i = 0; i < Math.Min(wordCount - 4, words.Length); i++)
            {
                string random_word = Babble_Function(hashTable);
                textBlock1.Text += " " + random_word;
                split_words = current_word.Split(delimiterChars);
                current_word = split_words[1] + "-" + split_words[2] + "-" + split_words[3] + "-" + split_words[4] + "-" + random_word;
            }
        }

        //Babble Function chooses a random word from the ArrayList corresponding to a particular key
        private string Babble_Function(Dictionary<string, ArrayList> hashTable)
        {
            //The following five if (order_n) statements deal with if the last word of the text file is ever used as a key

            //If the key is not in the hash table, start over at the first word
            if (Order_n == 1)
            {
                if (!hashTable.ContainsKey(current_word))
                {
                    current_word = words[0];
                }
            }

            //If the key is not in the hash table, start over at the first two words
            if (Order_n == 2)
            {
                if (!hashTable.ContainsKey(current_word))
                {
                    current_word = words[0] + "-" + words[1];
                }
            }
            //If the key is not in the hash table, start over at the first three words

            if (Order_n == 3)
            {
                if (!hashTable.ContainsKey(current_word))
                {
                    current_word = words[0] + "-" + words[1] + "-" + words[2];
                }
            }

            //If the key is not in the hash table, start over at the first four words
            if (Order_n == 4)
            {
                if (!hashTable.ContainsKey(current_word))
                {
                    current_word = words[0] + "-" + words[1] + "-" + words[2] + "-" + words[3];
                }
            }

            //If the key is not in the hash table, start over at the first five words
            if (Order_n == 5)
            {
                if (!hashTable.ContainsKey(current_word))
                {
                    current_word = words[0] + "-" + words[1] + "-" + words[2] + "-" + words[3] + "-" + words[4];
                }
            }

            //Generate a random number according to how many elements are in the ArrayList
            int number_choice = random_index.Next(hashTable[current_word].Count);

            //Create a new ArrayList for the specified key from the hashTable.
            ArrayList list = hashTable[current_word];

            //Create a string variable for the random number choice from the ArrayList
            string new_word = Convert.ToString(list[number_choice]);

            //Return the randomly choosen word from the ArrayList
            return new_word;
            
        }

        private void orderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order_n = orderComboBox.SelectedIndex;
            analyzeInput(orderComboBox.SelectedIndex);            
        }
    }
}
