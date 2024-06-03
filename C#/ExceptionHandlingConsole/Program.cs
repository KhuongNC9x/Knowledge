using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandlingConsole
{
    internal class Program
    {
        /// <summary>
        /// Outputs
        /// Hello from try block
        /// Hello from inner finally block
        /// Hello from exception block
        /// Hello from outer finally block
        /// </summary>
        public static void NestedExceptionBlock()
        {
            try
            {
                // Step 1: code execution begins
                try
                {
                    // Step 2: an exception occurs here
                    Console.WriteLine("Hello from try block");
                    throw new NotImplementedException();
                }
                catch
                {
                    Console.WriteLine("Hello from inner catch block");
                }
                finally
                {
                    // Step 4: the system executes the finally code block associated with the try statement where the exception occurred
                    Console.WriteLine("Hello from inner finally block");
                }
            }
            catch // Step 3: the system finds a catch clause that can handle the exception
            {
                // Step 5: the system transfers control to the first line of the catch code block
                Console.WriteLine("Hello from outer catch block");
            }
            finally
            {
                Console.WriteLine("Hello from outer finally block");
            }
        }
        static void Main(string[] args)
        {
            NestedExceptionBlock();
            Console.ReadLine();
        }
    }
}
