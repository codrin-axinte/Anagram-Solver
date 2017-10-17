﻿using System;
using System.IO;
using System.Linq;

namespace AnagramSolver {

    internal class Program {        

        public static void Main(string[] args) {
            // Get words database path 
            var filePath = GetFilePath();
            // Init Algo object with the database file path
            var algo = new Algo(filePath, 6);
            // Encode db file path, so we pair the cache with the file path database. Different databases may have different solutions
            var cacheFile = Base64Encode(filePath);
            // Init Cache Manager
            var cache = new Cache("resources/" + cacheFile);
            var keepAlive = true;
            while (keepAlive) {
                // Output a blank space on each turn
                UI.Blank();
                // Validate and get the anagram
                var anagram = GetAnagram(algo);            

                // Try to find the anagram in the repository
                var solutions = cache.Find(anagram);
             
                // Check if anagram was found in the cache repository
                if (solutions == null) {
                    // If there were no solutions found in the cache repository, then we collect a new set of solutions
                    solutions = algo.Solve(anagram);
                   
                    // We store the valid anagram with already solved solutions to the cache repository and save it
                    cache.Save(anagram, solutions);
                }

                // First we must check if there are any solutions at all
                // We store the total solutions count for later usage
                if (solutions.Count == 0) {
                    // If there are none we output a message for the user and we return/exit
                    UI.Warning("No solution was found.");
                    continue;
                }

                // Otherwise we output the first solution
                UI.Success("Best solution is '" + solutions.First() + "' from a total of " + solutions.Count);

                //In addition we check if there are any another solutions so we can suggest them to the user
                if (solutions.Count > 1)
                    // Here we start from one to exclude the repetition of the first solution, we already have it above
                    UI.List(solutions, "Other suggestions:", 1);
            }
        }
        
        private static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
       
        private static string GetFilePath(string defaultFilePath = "resources/db.csv") {          
            string path;
            bool fileExists;
            // We repeat the reading until the file exists
            do {
                path = UI.Input("Please input your dictionary path (Leave it blank to use the default):");
                // If the input is left blank we set the path to default one
                if (string.IsNullOrEmpty(path))            
                    path = defaultFilePath;
                // IF the the file does not exists we output a message
                if (!(fileExists = File.Exists(path)))
                    UI.Error("File not found at the given path");
                
            } while (!fileExists);

            return path;
        }

        private static string GetAnagram(Algo algo){
            //We prepare the variable where to store the anagram
            string anagram;
            // This will store if the anagram is valid or not
            bool isValid;
            // We repeat the user input until he give us a valid anagram matching the pattern defined above
            do {
                // Get the user input for the anagram
                anagram = UI.Input("Anagram:");
                // We check and assing the pattern match of the given anagram, if it's true we continue, yey we have a geniuns
                if (isValid = algo.IsValid(anagram)) continue;
                // Otherwise, for the monkeys, we will output some messages to provide the necessary information about our pattern requierements.
                UI.Warning("Only A-Za-z characters are accepted.");
                UI.Warning("The maximum length of characters is " + algo.MaxCharacters + " and the minimum is " + algo.MinCharacters);
            } while(!isValid);
            // In the end we output a 'ok' message and return the valid anagram
            UI.Ok("Anagram is matching the pattern");
            return anagram;
        }

      
    }
}