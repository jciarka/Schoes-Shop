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
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities.FilterHelperClasses;
using SportsStore.WebUI.Models.ShopProducts;

namespace SportsStore.WebUI.Models.ProductDetails
{
    public class SchoesDetailsViewModel
    {
        public SchoesModel SchoesModel { get; set; }

        public string ReturnUrl { get; set; }
    }
}