using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Entities.Additions;
using System.Reflection;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities.FilterHelperClasses;
using SportsStore.WebUI.Models.ShopProducts;
using SportsStore.WebUI.Models.ProductDetails;

namespace SportsStore.WebUI.Controllers
{
    public class ProductDetailsController : Controller
    {
        ISchoesRepository repository;

        public ProductDetailsController(ISchoesRepository schoesRepo)
        {
            repository = schoesRepo;
        }

        public ActionResult ProductDetailsSite(int modelId, string returnUrl)
        {
            SchoesModel schoesModel = repository.SchoesModelsRepository.FirstOrDefault(model => model.SchoesModelID == modelId);
            if (schoesModel == null)
            {
                return new HttpUnauthorizedResult();
            }

            SchoesDetailsViewModel schoesViewModel = new SchoesDetailsViewModel
            {
                SchoesModel = schoesModel,
                ReturnUrl = returnUrl
            };
            return View(schoesViewModel);
        }
    }
}