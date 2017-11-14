using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver
{
    public enum DatabaseType
    {
        Text,
        CSV,
        SQL       
    }

    public class DatabaseManager
    {
        /// <summary>
        /// Stores the database file path
        /// </summary>
        private string _filePath;
        private DatabaseType _type;
        /// <summary>
        /// Encode db file path, so we pair the cache with the file path database. Different databases may have different solutions
        /// </summary>
        public string CacheFile {
            get {
                return Base64Encode(_filePath);
            }
        }

        public DatabaseManager(string path, DatabaseType type = DatabaseType.Text) {
            _filePath = path;
            _type = type;
        }

       public void ReadLines(Action<string> callback)
        {
            try
            {
                // We try to parse the file from the given path using the StreamReader
                using (var reader = new StreamReader(_filePath))
                {
                    // This stores the current line while reading and parsing
                    string line;
                    // While there are lines to read, we assing line var, the current reading line
                    while ((line = ParseLine(reader.ReadLine())) != null)
                        callback(line);                    
                }
            }
            catch (Exception e)
            {
                // If any error occurs we catch, output it
                UI.Error(e.Message);
            }
        }

        /// <summary>
        /// We split the current line by a comma. Assumming that the format is: value, length (or anything else, it will be just ignored)
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string ParseLine(string line) {
            switch (_type)  {
                case DatabaseType.CSV:
                    // We store the first row as lowercase for avoiding any case issues
                    var row = line.Split(',');

                    // We convert the second row to be able to compare it
                    // Never trust the client, we will get the length by code
                    // Make the word lower to avoid any error
                    return row[0].ToLower();                    
                default:
                    return line.ToLower();                   
            }           
           
        }
           

        /// <summary>
        /// Encode string to Base64
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText); // We get the bytes from the plain text we want to encode
            return Convert.ToBase64String(plainTextBytes); // We encode the bytes to a base64 string
        }
    }
}
