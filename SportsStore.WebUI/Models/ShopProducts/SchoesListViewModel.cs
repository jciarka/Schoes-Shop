using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Entities.FilterHelperClasses;

namespace SportsStore.WebUI.Models.ShopProducts
{
    public class SchoesListViewModel
    {
        public IEnumerable<SchoesModel> Schoes { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public SchoesFilterInfo SchoesFilterInfo { get; set; }
    }
}