using MyOnlineShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineShop.DA.Inmemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["Products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["Products"] = products;
        }

        public void Insert(Product prod)
        {
            products.Add(prod);
        }

        public void Update(Product prod)
        {
            Product prodToUpdate = products.Find(i => i.Id == prod.Id);

            if (prodToUpdate != null)
            {
                prodToUpdate = prod;
            }
            else
            {
                throw new Exception("Product  Not found");
            }
        }

        public Product Find(string Id)
        {
            Product prod = products.Find(i => i.Id == Id);
            if (prod != null)
            {
                return prod;
            }
            else
            {
                throw new Exception("Product  Not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product prodToDelete = products.Find(i => i.Id == Id);

            if (prodToDelete != null)
            {
                products.Remove(prodToDelete);
            }
            else
            {
                throw new Exception("Product  Not found");
            }
        }

    }
}
