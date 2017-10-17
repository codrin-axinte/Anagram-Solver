using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnagramSolver {
    public class Algo {
        public int MaxCharacters { get; private set; }
        public int MinCharacters { get; private set; }

        private string _filePath;
        private string _pattern;

        public Algo(string dbPath) {
            _filePath = dbPath;
        }
        
        public Algo(string dbPath, int maxCharacters = 9, int minCharacters = 1) {
            _filePath = dbPath;
            SetRules(maxCharacters, minCharacters);
        }
        
        public void SetRules(int maxCharacters = 9, int minCharacters = 1) {
            // Here we define the pattern for the anagram we want to solve. 
            // The pattern consists in only alphabetics characters a-z (Uppers or Lowercase)
            // And from the characters length range defined in the parameters.
            MaxCharacters = maxCharacters;
            MinCharacters = minCharacters;
            _pattern = "^[a-zA-Z]{" + minCharacters + "," + maxCharacters + "}$";
        }
        
        public bool IsValid(string anagram) {
            // We perform a regex check on the anagram based our pattern and return the result
            if (anagram == null) return false;
            return Regex.IsMatch(anagram, _pattern);
        }
        
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