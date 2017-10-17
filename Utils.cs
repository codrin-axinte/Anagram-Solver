using System;
using System.IO;

namespace AnagramSolver
{
    static class Utils
    {
        public static void EachFileLine(string filePath, Action<string> currentLine)
        {
            try
            {
                // We try to parse the file from the given path using the StreamReader
                using (var reader = new StreamReader(filePath))
                {
                    // This stores the current line while reading and parsing
                    string line;
                    // While there are lines to read, we assing line var, the current reading line
                    while ((line = reader.ReadLine()) != null)
                        currentLine.Invoke(line);
                }
            }
            catch (Exception e)
            {
                // If any error occurs we catch, output it, and throw it again
                UI.Error(e.Message);
                throw;
            }
        }
    }
}
