using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace UnemployedWordle
{
    class Program
    {
        Words words = new Words();
        public int lives = 6;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.GetData();
        }

        private string GetWord()
        {
            Random rnd = new Random();

            return words.Word[rnd.Next(1, words.Word.Length)];
        }

        private void GetData()
        {
            string word = "";
            word = GetWord();
            
            Game(word);
        }

        private void StartGame()
        {
            string ans = "";
            Console.Write("Start New Game? ");
            ans = Console.ReadLine();

            if (ans == "Y" || ans == "y" || ans == "yes" || ans == "Yes")
            {
                Console.Clear();
                lives = 6;
                Game(GetWord());
            }
            else if (ans == "N" || ans == "n" || ans == "no" || ans == "No")
            {
                Environment.Exit(0);
            }
        }

        private void Game(string word)
        {
            string guess = "";
            //Console.ForegroundColor = ConsoleColor.DarkGray;
            //Console.WriteLine("| " + word);
            //Console.ForegroundColor = ConsoleColor.White;
            int result = 0;

            Console.WriteLine("| UNEMPLOYED WORDLE </3");
            Console.WriteLine("-----------------------");

            while (lives > -1 || result != 1)
            {
                guess = Console.ReadLine();

                if (guess.Length == 5)
                {
                    if (guess == word)
                    {
                        result = 1;
                        Result(word, 1);
                    }

                    FindSim(word, guess);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("| invalid string length, please reenter");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
            
        //Find letters that exist within the word - not that they are on the same level
        private void FindSim(string word, string guess)
        {
            if (lives < 0)
            {
                Result(word, 2);
            }

            // string corrString = "";
            //string cs = "";
            int type = 0;

            char[] wLetters = new char[5];
            char[] gLetters = new char[5];

            int i = 0;
            foreach (char l in word.ToUpper())
            {
                wLetters[i] = l;
                i++;
            }

            i = 0;
            foreach (char l in guess.ToUpper())
            {
                gLetters[i] = l;
                i++;
            }

            for(int x = 0; x < gLetters.Length; x++)
            {
                for(int y = 0; y < wLetters.Length; y++)
                {
                    if (gLetters[x].ToString() == wLetters[y].ToString())
                    {
                        if(FindExact(x, gLetters, wLetters) == "^")
                        {
                            type = 0;
                        }
                        else
                        {
                            type = 1;
                        }

                        break;
                    }
                    else
                    {
                        type = 2;
                    }
                }

                Console.Write(" | ");

                if (type == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (type == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(gLetters[x]);

                Console.ForegroundColor = ConsoleColor.White;
            }

            lives--;
            Console.WriteLine();
        }

        //Find letters on the exact same character as each other
        private string FindExact(int x, char[] g, char[] w)
        {
            string result = "";

            if (w[x] == g[x])
            {
                result = "^";
            }

            return result;
        }

        private void Result(string corrWord, int index)
        {
            if (index == 1)     //win
            {
                Console.WriteLine();
                Console.WriteLine("| YOU GUESSED THE WORD");
                Console.WriteLine("----------------------");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("| " + corrWord);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("| ts pmo icl </3");
            }
            else                //lose
            {
                Console.WriteLine();
                Console.WriteLine("| YOU COULDN'T GUESS THE WORD");
                Console.WriteLine("-----------------------------");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("| " + corrWord);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("| time to get a j*b, bro </3");
            }

            Load();
        }

        private void Load()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------");
            StartGame();
        }
    }
}