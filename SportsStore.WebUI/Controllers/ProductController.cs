using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoesStore.Domain.Abstract;
using ShoesStore.Domain.Entities;
using ShoesStore.WebUI.Models;
using ShoesStore.WebUI.Models.ShopProducts;
using ShoesStore.Domain.Concrete;//```````````````````````````
using ShoesStore.Domain.Entities.Additions;//```````````````````````````

namespace ShoesStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository)
        {
            /*using (ShoesDbContext bdContext = new ShoesDbContext())
            {
                SchoesModel model = new SchoesModel()
                {
                    SchoesModelName = "Mandy",
                    Description = "Eleganckie damskie kozaki na wysokim obcasie",
                    Price = 599,
                    Brand = new Brand("Nike"),
                    SchoesModelUser = new SchoesModelUser("Mężczyzna"),
                    SchoesDestiny = new HashSet<SchoesDestiny>(new SchoesDestiny[]
                        {
                            new SchoesDestiny("Koszykówka"),
                        }),
                    SubCategory = new HashSet<SubCategory>(new SubCategory[]
                        {
                            new SubCategory("Sportowe")
                        }),
                    Colour = new string[] { "Białe", "Czarne" },
                    SizeArray = new int[] { 39, 40, 41, 42, 44, 45, 46 },
                    OriginCountry = "Polska",
                    InsideFabric = "Skóra naturalna",
                    ShankFabric = "Skóra",
                    SoleFabric = "Guma"
                };

                bdContext.SaveSchoesModel(model);

            }//```````````````````````````*/
            this.repository = productRepository;
        }

        // GET: Product
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel {
                Products = repository.Products
                    .Where(product => category == null || product.Category == category)
                    .OrderBy(product => product.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = (category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(product => product.Category == category).Count())
                    },

                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int productID)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}