using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Entities.FilterHelperClasses;
using SportsStore.WebUI.Models.ShopProducts;


namespace SportsStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        //public SchoesFilterInfo SchoesFilterInfo { get; set; }
        //public int Page { get; set; }
    }
}