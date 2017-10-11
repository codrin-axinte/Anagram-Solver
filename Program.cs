﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnagramSolver {
    internal class Program {
        
        public static void Main(string[] args) {

            var filePath = GetFilePath();

            // Validate and get the anagram
            var anagram = GetAnagram();
            // After we have parsed the anagram, we must find the words matching the solver rules.
            var solutions = CollectSolutions(anagram, filePath);
            // We store the total solutions count for later usage
            var count = solutions.Count;
            // First we must check if there are any solutions at all
            if ( count == 0) {
                // If there are none we output a message for the user and we return/exit
                UI.Warning("No solution was found.");
                // Pause the console.
                UI.Pause();
                return;
            }

            // Otherwise we output the first solution
            UI.Success("Solution is '" + solutions.First() + "' from a total of " + count);
      
            //In addition we check if there are any another solutions so we can suggest them to the user
            if(count < 10) {
                UI.Blank();
                // Here we start from one to exclude the repetition of the first solution, we already have above
                UI.List(solutions, "Other suggestions:", 1);
            }

            // Pause the console.
            UI.Pause();
        }

        private static string GetFilePath(string defaultFilePath = "db.csv") {          
            string path;
            bool fileExists;

            do {
                path = UI.Input("Please input your dictionary path(Leave it blank to use the default):");
                if (string.IsNullOrEmpty(path))            
                    path = defaultFilePath;

                if (!(fileExists = File.Exists(path)))
                    UI.Error("File not found at the given path");
                
            } while (!fileExists);

            return path;
        }

        private static List<string> CollectSolutions(string anagram, string filePath) {
            // We prepare a list of solutions to return later
            var data = new List<string>();
            try {
                // We try to parse the file from the given path using the StreamReader
                using (var reader = new StreamReader(filePath)) {
                    // This stores the current line while reading and parsing
                    string line;
                    // While there are lines to read, we assing line var the current reading line
                    while ((line = reader.ReadLine()) != null) {
                        // We split the current line by a comma. We assume that the format is: value, length
                        var row = line.Split(',');
                        // We store the first row as lowercase for avoiding any case issues
                        var value = row[0].ToLower();
                        // We convert the second row to be able to compare it
                        var len = Convert.ToInt32(row[1]);
                        //
                        if(len > anagram.Length || !anagram.All(value.Contains)) continue;
                        data.Add(value);
                    }
                }
            }
            catch (Exception e) {
                // If any error occurs we catch, output it, and throw it again
                UI.Error(e.Message);
                throw;
            }

            // We sort all the solutions
            // TODO: Improve sorting to order them by the length of characters first and after alphabetically
            data.Sort();
           
            // At the end we return all the sorted solutions
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
                anagram = UI.Input("Anagram:");
                // We check and assing the pattern match of the given anagram, if it's true we continue, yey we have a geniuns
                if (isValid = Regex.IsMatch(anagram, pattern)) continue;
                // Otherwise, for the monkeys, we will output some messages to provide the necessary information about our pattern requierements.
                UI.Warning("Only A-Za-z characters are accepted.");
                UI.Warning("The maximum length of characters is " + maxCharacters + " and the minimum is " + minCharacters);
            } while(!isValid);
            UI.Ok("Anagram is matching the pattern");
            // In the end we return the valid anagram
            return anagram;
        }

      
    }
}