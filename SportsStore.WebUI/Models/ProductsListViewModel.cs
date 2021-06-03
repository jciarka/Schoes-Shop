using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoesStore.Domain;
using ShoesStore.Domain.Abstract;
using ShoesStore.WebUI.Models;
using ShoesStore.Domain.Entities;
using ShoesStore.WebUI.Models.ShopProducts;

namespace ShoesStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}