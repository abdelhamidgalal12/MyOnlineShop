using MyOnlineShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineShop.DA.Inmemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> prodcats;

        public ProductCategoryRepository()
        {
            prodcats = cache["ProductCategory"] as List<ProductCategory>;
            if (prodcats == null)
            {
                prodcats = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["ProductCategory"] = prodcats;
        }

        public void Insert(ProductCategory prodcat)
        {
            prodcats.Add(prodcat);
        }

        public void Update(ProductCategory prodcat)
        {
            ProductCategory prodcatToUpdate = prodcats.Find(i => i.Id == prodcat.Id);

            if (prodcatToUpdate != null)
            {
                prodcatToUpdate = prodcat;
            }
            else
            {
                throw new Exception("Product  Not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory prodcat = prodcats.Find(i => i.Id == Id);
            if (prodcat != null)
            {
                return prodcat;
            }
            else
            {
                throw new Exception("Product  Not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return prodcats.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory prodToDelete = prodcats.Find(i => i.Id == Id);

            if (prodToDelete != null)
            {
                prodcats.Remove(prodToDelete);
            }
            else
            {
                throw new Exception("Product  Not found");
            }
        }

    }
}
