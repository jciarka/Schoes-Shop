using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShoesStore.Domain;
using ShoesStore.Domain.Abstract;
using ShoesStore.Domain.Entities;
using ShoesStore.Domain.Entities.Additions;
using System.Reflection;
using System.Web.Mvc;
using ShoesStore.WebUI.Models;
using ShoesStore.Domain.Entities.FilterHelperClasses;
using ShoesStore.WebUI.Models.ShopProducts;
using ShoesStore.WebUI.Models.ProductDetails;

namespace ShoesStore.WebUI.Controllers
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