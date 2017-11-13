using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnagramSolver {
    public class Algo {
        /// <summary>
        /// Define the maximum characters the anagram can have 
        /// </summary>
        public int MaxCharacters { get; private set; }
        /// <summary>
        /// Define the minimum characters the anagram can have
        /// </summary>
        public int MinCharacters { get; private set; }

        /// <summary>
        /// Stores the database file path
        /// </summary>
        private string _filePath;
        /// <summary>
        /// Stores the validation pattern
        /// </summary>
        private string _pattern;


        /// <summary>
        /// Init the algo with the database path without any rules defined
        /// </summary>
        /// <param name="dbPath"></param>
        public Algo(string dbPath) {
            _filePath = dbPath;
        }
        

        /// <summary>
        /// Init the algo with the database path and the characters rules defined
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="maxCharacters"></param>
        /// <param name="minCharacters"></param>
        public Algo(string dbPath, int maxCharacters = 9, int minCharacters = 1) {
            _filePath = dbPath;
            SetRules(maxCharacters, minCharacters);
        }

        /// <summary>
        /// Define the pattern for the anagram we want to solve. 
        /// The pattern consists in: 
        /// - Only alphabetics characters A-Z (Uppers or Lowercase)
        /// - And from the characters length range defined in the parameters.
        /// </summary>
        /// <param name="maxCharacters"></param>
        /// <param name="minCharacters"></param>
        public void SetRules(int maxCharacters = 9, int minCharacters = 1) {
           
            MaxCharacters = maxCharacters;
            MinCharacters = minCharacters;
            _pattern = "^[a-zA-Z]{" + minCharacters + "," + maxCharacters + "}$";
        }

        /// <summary>
        /// We perform a regex check on the anagram based our pattern and return the result
        /// </summary>
        /// <param name="anagram"></param>
        /// <returns></returns>
        public bool IsValid(string anagram) {           
            if (anagram == null) return false;
            return Regex.IsMatch(anagram, _pattern);
        }

        /// <summary>
        /// Solve the given anagram
        /// </summary>
        /// <param name="anagram"></param>
        /// <returns></returns>
        public List<string> Solve(string anagram) {
            // We prepare a list of solutions to return later
            var data = new List<string>();
            // Anagram was not found, so we continue
            try {
                // We try to parse the file from the given path using the StreamReader
                using (var reader = new StreamReader(_filePath)) {
                    // This stores the current line while reading and parsing
                    string line;
                    // While there are lines to read, we assing line var, the current reading line
                    while ((line = reader.ReadLine()) != null) {
                        var row = ParseCsv(line);
                        if(row.Length > anagram.Length || !row.All(anagram.Contains)) continue;
                        data.Add(row);
                    }
                }
            }
            catch (Exception e) {
                // If any error occurs we catch, output it
            }
            // Before we return, we must sort the list data to match our requierement. 
            // We first sort words by their length descending, the more letters, the more valuable
            // After we sort it in the alphabet order
            var solutions = data.OrderByDescending(str => str.Length).ThenBy(str => str).ToList();
       
            // At the end we return all the sorted solutions
            return solutions;
        }

        private string ParseCsv(string line) {
            // We split the current line by a comma. We assume that the format is: value, length
            // We store the first row as lowercase for avoiding any case issues
            var row = line.Split(',');
         
            // We convert the second row to be able to compare it
            // Never trust the client, we will get the length by code
            //var len = Convert.ToInt32(row[1]);
            return row[0].ToLower();
        }
    }
}