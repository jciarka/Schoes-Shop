using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models.ShopProducts;

namespace SportsStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}