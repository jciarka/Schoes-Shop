using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain;
using SportsStore.Domain.Entities.Additions;
using System.Reflection;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities.FilterHelperClasses;
using SportsStore.WebUI.Models.ShopProducts;

namespace SportsStore.WebUI.Controllers
{
    [Authorize(Roles = "ProductsAdmin")]
    public class StockAdminController : Controller
    {
        private ISchoesRepository repository;
        
        public StockAdminController(ISchoesRepository repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.SchoesModelsRepository);
        }

        /*
        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["Message"] = $"Zapisano {product.Name}";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int ProductID)
        {
            Product deletedProduct = repository.DeleteProduct(ProductID);
            if (deletedProduct != null)
            {
                TempData["Message"] = $"Usunięto {deletedProduct.Name}";
            }
            return RedirectToAction("Index");
        }
        */
    }
}