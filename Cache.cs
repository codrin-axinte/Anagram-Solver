using System;
using System.Collections.Generic;
using System.IO;

namespace AnagramSolver {
    public class Cache {
        public Dictionary<string, List<string>> Data { get; } 
        private string _filePath;
        
        public Cache() {
            Data = new Dictionary<string, List<string>>();
        }
        
        public Cache(string filePath) {
            Data = new Dictionary<string, List<string>>();
            _filePath = filePath;
        }

        public List<string> Find(string key) {
            List<string> data;
            Data.TryGetValue(key, out data);
            return data;
        }

        public void Flush() {
            if(_filePath == null) return;
            File.WriteAllText(_filePath, string.Empty);
        }
        
        public void Load() {
            // Check if the file path is null, if it is null we return
            if (_filePath == null)  return; 
            
            // Check if the file exists
            if (!File.Exists(_filePath)) {
                // Create the file if not exists
                File.WriteAllText(_filePath, string.Empty);
            }
            try {
                using (var reader = new StreamReader(_filePath)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        var row = line.Split(':');
                        var key = row[0];
                        var values = new List<string>(row[1].Split(','));
                        Data.Add(key, values);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public Cache Put(string key, List<string> value) {
            Data.Add(key, value);
            return this;
        }
        
        public void Save() {
            // Check if the file path is null, if it is null we return
            if (_filePath == null) return;
            // Write all content to the cache file
            File.WriteAllText(_filePath, ToString());
        }
        public override string ToString() {
            var content = "";
            foreach (var item in Data) {
                var key = item.Key;
                var values = string.Join(",", item.Value.ToArray());
                content += key + ":" + values + Environment.NewLine;
            }
            return content;
        }
    }
}