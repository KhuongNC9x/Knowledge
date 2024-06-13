using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryCaching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fetching product with ID 1 for the first time:");
            var product = InMemoryCache.GetProductById(1);
            Console.WriteLine($"Product Name: {product.Name}\n");

            Console.WriteLine("Fetching product with ID 1 again:");
            product = InMemoryCache.GetProductById(1); // This time, it should come from the cache
            Console.WriteLine($"Product Name: {product.Name}\n");
            Console.ReadLine();
        }
    }
}
