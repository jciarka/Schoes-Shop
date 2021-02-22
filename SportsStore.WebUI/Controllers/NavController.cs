using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {

        IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            // ten fragment po mojemu, alternatywa na stronie 212 i 213 z użyciem ViewBag
            CategoryInfo categoryInfo = new CategoryInfo
            {
                SelectedCategory = category,
                Categories = repository.Products
                    .Select(product => product.Category)
                    .Distinct()
                    .OrderBy(cat => cat)
            };
            
            return PartialView(categoryInfo);    
        }
    }
}