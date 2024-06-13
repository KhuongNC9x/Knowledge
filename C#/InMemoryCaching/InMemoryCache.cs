using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryCaching
{
    public static class InMemoryCache
    {
        private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private static ProductRepository _productRepository = new ProductRepository();

        public static Product GetProductById(int id)
        {
            if (!_cache.TryGetValue(id, out Product product))
            {
                Console.WriteLine("Fetching data from database...");
                product = _productRepository.GetProductById(id);
                _cache.Set(id, product, TimeSpan.FromMinutes(30));
            }
            else
            {
                Console.WriteLine("Fetching from cache...");
            }

            return product;
        }
    }
}
