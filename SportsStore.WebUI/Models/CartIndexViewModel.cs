using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoesStore.Domain.Entities;
using ShoesStore.Domain.Entities.FilterHelperClasses;
using ShoesStore.WebUI.Models.ShopProducts;


namespace ShoesStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        //public SchoesFilterInfo SchoesFilterInfo { get; set; }
        //public int Page { get; set; }
    }
}