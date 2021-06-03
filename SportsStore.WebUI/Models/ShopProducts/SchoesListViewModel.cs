using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoesStore.Domain;
using ShoesStore.Domain.Abstract;
using ShoesStore.WebUI.Models;
using ShoesStore.Domain.Entities;
using ShoesStore.Domain.Entities.FilterHelperClasses;

namespace ShoesStore.WebUI.Models.ShopProducts
{
    public class SchoesListViewModel
    {
        public IEnumerable<SchoesModel> Schoes { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public SchoesFilterInfo SchoesFilterInfo { get; set; }
    }
}