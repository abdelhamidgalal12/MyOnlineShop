using MyOnlineShop.Core.Contracts;
using MyOnlineShop.Core.Models;
using MyOnlineShop.Core.ViewModels;
using MyOnlineShop.DA.Inmemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyOnlineShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        IRepository<Product> Productcontext;
        IRepository<ProductCategory> ProductCategorycontext;

        public ProductManagerController(IRepository<Product> productcontext, IRepository<ProductCategory> productCategorycontext)
        {
            Productcontext = productcontext;
            ProductCategorycontext = productCategorycontext;

        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Productcontext.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = ProductCategorycontext.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                Productcontext.Insert(product);
                Productcontext.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string Id)
        {
            Product product = Productcontext.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();

                viewModel.Product = product;
                viewModel.ProductCategories = ProductCategorycontext.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = Productcontext.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                Productcontext.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = Productcontext.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = Productcontext.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Productcontext.Delete(Id);
                Productcontext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}