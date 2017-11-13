using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver
{
    public static class UI
    {
        /// <summary>
        /// Get a user input on the next line after a label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="operatorFormat"></param>
        /// <returns></returns>
        public static string Input(string label, string operatorFormat = ">>> ")
        {
            Console.WriteLine("[-] " + label);
            Console.Write(operatorFormat);
            return Console.ReadLine();
        }
        /// <summary>
        /// Get a user input on the same line as the label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static string InputInline(string label)
        {
            Console.Write("[-] " + label);
            return Console.ReadLine();
        }
        /// <summary>
        /// Output a info message
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }
        /// <summary>
        /// Output a warning message
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            Console.WriteLine("[WARNING] " + message);
        }
        /// <summary>
        /// Output a error message
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }
        /// <summary>
        /// Output a success message
        /// </summary>
        /// <param name="message"></param>
        public static void Success(string message)
        {
            Console.WriteLine("[SUCCESS] " + message);
        }
        /// <summary>
        /// Output a successful informative message
        /// </summary>
        /// <param name="message"></param>
        public static void Ok(string message)
        {
            Console.WriteLine("[OK] " + message);
        }
        /// <summary>
        /// Output a list items on each line
        /// </summary>
        /// <param name="items"></param>
        /// <param name="label"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void List(List<string> items, string label = null, int start = 0, int end = 0)
        {
            if (label != null) Info(label);
            var length = end == 0 ? items.Count : end;
            for(var i = start; i < length; i++)
                Console.WriteLine(" [-] " + items[i]);   
        }
        /// <summary>
        /// Pause the console
        /// </summary>
        public static void Pause()
        {
            Console.ReadLine();
        }
        /// <summary>
        /// Output a blank line
        /// </summary>
        public static void Blank()  {
            Console.WriteLine();
        }
    }
}

