using MyOnlineShop.Core.Contracts;
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
        IRepository<ProductCategory> ProductCategorycontext;
        public ProductCategoryManagerController(IRepository<ProductCategory> productCategorycontext)
        {
            ProductCategorycontext = productCategorycontext;
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productcats = ProductCategorycontext.Collection().ToList();
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
                ProductCategorycontext.Insert(productcats);
                ProductCategorycontext.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productcats = ProductCategorycontext.Find(Id);
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
            ProductCategory productcatToEdit = ProductCategorycontext.Find(Id);

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
                ProductCategorycontext.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productcatToDelete = ProductCategorycontext.Find(Id);

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
            ProductCategory productcatToDelete = ProductCategorycontext.Find(Id);

            if (productcatToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductCategorycontext.Delete(Id);
                ProductCategorycontext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}