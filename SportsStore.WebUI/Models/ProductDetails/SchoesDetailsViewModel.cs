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
using System.Web.Mvc;
using ShoesStore.WebUI.Models;
using ShoesStore.Domain.Entities.FilterHelperClasses;
using ShoesStore.WebUI.Models.ShopProducts;

namespace ShoesStore.WebUI.Models.ProductDetails
{
    public class SchoesDetailsViewModel
    {
        public SchoesModel SchoesModel { get; set; }

        public string ReturnUrl { get; set; }
    }
}