using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SportsStore.Domain;
using SportsStore.Domain.Entities.Additions;
using System.Reflection;
using SportsStore.Domain.Entities.FilterHelperClasses;
using SportsStore.WebUI.Models.ShopProducts;
using SportsStore.WebUI.Models.ProductDetails;


namespace SportsStore.WebUI.Controllers
{
    //[Authorize(Roles= "StandardUser")]
    public class CartController : Controller
    {
        ISchoesRepository repository;
        IOrderProcessor orderProcessor;

        public CartController(ISchoesRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl,
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails() /*{ Name = "ABC" }*/);//Uzywając text box for przypisane właściwości obiektu zostaną wprowadzone do pól tekstowych 
        } //jeśli wywoła sie widok bez obiektu to obiekt i tak zostanie automatycznie utworzony przez dołączenie obiektu

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Koszyk jest pusty");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        
        public RedirectToRouteResult AddToCart(Cart cart, int modelId, string returnUrl)
        {
            SchoesModel schoesModel = repository.SchoesModelsRepository.FirstOrDefault(p => p.SchoesModelID == modelId);

            if (schoesModel != null)
            {
                cart.AddItem(schoesModel, 1);
            }
            return RedirectToAction("Index", new { returnUrl = returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int modelId, string returnUrl)
        {
            SchoesModel schoesModel = repository.SchoesModelsRepository.FirstOrDefault(p => p.SchoesModelID == modelId);

            if (schoesModel != null)
            {
                cart.RemoveLine(schoesModel);
            }
            return RedirectToAction("Index", new { returnUrl = returnUrl });
        }
    }
}