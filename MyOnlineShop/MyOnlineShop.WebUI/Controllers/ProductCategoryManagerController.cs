using MyOnlineShop.Core.Models;
using MyOnlineShop.DA.Inmemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyOnlineShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> context;
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productcats = context.Collection().ToList();
            return View(productcats);
        }

        public ActionResult Create()
        {
            ProductCategory productcats = new ProductCategory();
            return View(productcats);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productcats)
        {
            if (!ModelState.IsValid)
            {
                return View(productcats);
            }
            else
            {
                context.Insert(productcats);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productcats = context.Find(Id);
            if (productcats == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcats);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productcats, string Id)
        {
            ProductCategory productcatToEdit = context.Find(Id);

            if (productcatToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productcats);
                }
                productcatToEdit.Category = productcats.Category;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productcatToDelete = context.Find(Id);

            if (productcatToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcatToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productcatToDelete = context.Find(Id);

            if (productcatToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}