using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver {
    public class Cache {

        private string _filePath;
                
        public Cache(string filePath) {
            _filePath = filePath;
        }

   
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
                    
                    while ((line = reader.ReadLine()) != null) {
                        var row = line.Split(':');
                        var key = row[0];
                        if (key.Length != anagram.Length || !key.All(anagram.Contains)) continue;                     
                                                
                        solutions = new List<string>(row[1].Split(','));
                        break;
                    }
                }
            }
            catch (Exception e) {
                UI.Error(e.Message);
            }
            return solutions;
        }
        
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