using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigureAwait_false_
{
    internal class Program
    {
        public async static Task OldApproach()
        {
            await ReadDataAsync().ConfigureAwait(continueOnCapturedContext: true); // Potential deadlock if called from UI thread
        }

        public async static Task ImprovedApproach()
        {
            await ReadDataAsync().ConfigureAwait(false); // Avoids capturing the original context
        }

        private async static Task ReadDataAsync()
        {
            // Simulating an asynchronous I/O operation
            await Task.Delay(1000); // Simulate a delay
        }

        static void Main(string[] args)
        {
            OldApproach();
            ImprovedApproach();
            Console.ReadLine();
        }
    }
}
