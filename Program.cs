using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnagramSolver {
    internal class Program {
        /**
         * TODO: Make a dedicated class for algo solver
         * TODO: Write solution to cache and have a hash map for each database
         * TODO: Improve the searching algorithm
         */
        public static void Main(string[] args) {
            var anagram = GetAnagram(9);
            // After we have parsed the anagram, we must find the words matching the solver rules.
            var solutions = CollectSolutions(anagram, "db.csv");
            if (solutions.Count == 0) {
                Console.WriteLine("[!] No solution was found.");
                Console.ReadLine();
                return;
            }
 
            Console.WriteLine("[OK] Solution is '" + solutions.First() + "' from a total of " + solutions.Count);
            if (solutions.Count <= 10) {
                Console.WriteLine();
                Console.WriteLine("[-] Other Suggestions:");
                for (var i = 1; i < solutions.Count; i++) {
                    Console.WriteLine(" [+] Suggestion '" + solutions[i] + "'");
                }
            }
          Console.ReadLine();
        }



        private static List<string> CollectSolutions(string anagram, string filePath) {
            var data = new List<string>();
            try {
                using (var reader = new StreamReader(filePath)) {
                    string line;
                    
                    while ((line = reader.ReadLine()) != null) {
                        //Console.WriteLine("[-] Line Parsed: " + line);
                        var row = line.Split(',');
                        var value = row[0].ToLower();
                        var len = Convert.ToInt32(row[1]);
                        // atc = cat
                        // atcs = cat's
                        if(len > anagram.Length) continue;
                        if(value.All(anagram.Contains))
                            data.Add(value);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            data.Sort();
            
            return data;
        }
        
        private static string GetAnagram(int maxCharacters = 3, int minCharacters = 1){
            // Here we define the pattern for the anagram we want to solve. 
            // The pattern consists in only alphabetics characters a-z (Uppers or Lowercase)
            // And from the characters length range defined in the parameters.
            var pattern = "^[a-zA-Z]{"+ minCharacters+","+ maxCharacters +"}$";
            //We prepare the variable where to store the anagram
            string anagram;
            // This will store if the anagram is valid or not
            bool isValid;
            // We repeat the user input until he give us a valid anagram matching the pattern defined above
            do {
                Console.Write("Word: ");
                anagram = Console.ReadLine();
                // We check and assing the pattern match of the given anagram, if it's true we continue, yey we have a geniuns
                if (isValid = Regex.IsMatch(anagram, pattern)) continue;
                // Otherwise, for the monkeys, we will output some messages to provide the necessary information about our pattern requierements.
                Console.WriteLine("[!] Only A-Za-z characters are accepted.");
                Console.WriteLine("[!] The maximum length of characters is " + maxCharacters + " and the minimum is " + minCharacters);
            } while(!isValid);
            Console.WriteLine("[OK] Anagram is matching the pattern!");
            // In the end we return the valid anagram
            return anagram;
        }
    }
}