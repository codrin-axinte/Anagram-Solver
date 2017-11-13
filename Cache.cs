using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver {
    public class Cache {

        private string _filePath;
        
        /// <summary>
        /// Init cache manager and set the file path
        /// </summary>
        /// <param name="filePath"></param>
        public Cache(string filePath) {
            _filePath = filePath;
        }

        /// <summary>
        /// Dump the cache file if exists
        /// </summary>
        public void Flush() {
            if(_filePath != null) File.Delete(_filePath);
        }

        /// <summary>
        /// Load data from cache file and unserialize it
        /// Read each line, if the key is found we return it.
        /// </summary>
        /// <param name="anagram"></param>
        /// <returns></returns>
        public List<string> Find(string anagram) {
            // Check if the file path is null, if it is null we return
            if (_filePath == null)  return null; 
            
            // Check if the file exists
            if (!File.Exists(_filePath)) {
                // Create the file if not exists
                File.WriteAllText(_filePath, string.Empty);
                return null;
            }
            List<string> solutions = null;
            try {              
                using (var reader = new StreamReader(_filePath)) {
                    string line;
                    // Loop while there are lines to read
                    while ((line = reader.ReadLine()) != null) {
                        var row = line.Split(':'); // split the row by the ':' character
                        var key = row[0]; // Get the key element after the split, 
                        // Check if the key length not equals the anagram length or any of the anagram characters is not present in the key, we continue to the next line
                        if (key.Length != anagram.Length || !key.All(anagram.Contains)) continue;                     
                        // Otherwise we return a new list o strings by splitting the second element after the first split
                        solutions = new List<string>(row[1].Split(','));
                        break;
                    }
                }
            }
            catch (Exception e) {
                UI.Error(e.Message); // Output any exception as a error message
            }
            return solutions;
        }
        /// <summary>
        /// Write content to the cache file
        /// </summary>
        /// <param name="anagram"></param>
        /// <param name="solutions"></param>
        public void Save(string anagram, List<string> solutions) {
            // Check if the file path is null, if it is null we return
            if (_filePath == null) return;
            // Append all content to the cache file
            using (StreamWriter writer = File.AppendText(_filePath))
            {
                var values = string.Join(",", solutions.ToArray());
                var line = string.Format("{0}:{1}", anagram, values);
                writer.WriteLine(line);
            }
        }
     
    }
}