using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilePathConsole
{
    internal class Program
    {
        public static class FilePath
        {
            /// <summary>
            /// Outputs
            /// D:\Workspace\30DayChallenge.Net\30DayChallenge.Net\bin\Debug\net8.0
            /// </summary>
            /// 
            public static void DisplayCurrentDirectory()
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
            }
            
            /// <summary>
            /// Outputs
            /// C:\Users\admin\Documents
            /// </summary>
            public static void DisplaySpecialDirectory()
            {
                Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            }

            /// <summary>
            /// Outputs
            /// For windows: \sample        
            /// </summary>
            public static void DisplayOSPathCharacters()
            {
                Console.WriteLine($"For windows: {Path.DirectorySeparatorChar}sample");
            }
        }
        static void Main(string[] args)
        {
            FilePath.DisplayCurrentDirectory();
            FilePath.DisplaySpecialDirectory();
            FilePath.DisplayOSPathCharacters();
            Console.ReadLine();
        }
    }
}
