using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyInitialization
{
    internal class Program
    {
        ////Developers common mistake
        //// Initializing expensive resources upfront
        //private readonly static ExpensiveResource _expensiveObjectMistake = new ExpensiveResource();
        //public ExpensiveResource ExpensiveObjectMistake => _expensiveObjectMistake;

        // Efficient Method
        // Using Lazy<T> to initialize resources only when needed
        private readonly static Lazy<ExpensiveResource> _expensiveObject = new Lazy<ExpensiveResource>();
        public ExpensiveResource ExpensiveObject => _expensiveObject.Value;
        static void Main(string[] args)
        {
            Console.WriteLine("Application started.");
            Console.WriteLine("Press any key to use the expensive resource...");
            Console.ReadKey();

            // Accessing the Value property of _lazyExpensiveResource for the first time triggers the initialization.
            _expensiveObject.Value.UseResource();

            //_expensiveObjectMistake.UseResource();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
