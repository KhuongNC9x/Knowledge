using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryCaching
{
    // Simulating a product repository
    public class ProductRepository
    {
        public Product GetProductById(int id)
        {
            // Simulate database access
            return new Product
            {
                Id = id,
                Name = $"Product {id}"
            };
        }
    }

    public class Product
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }
}
