using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoesStore.Domain.Abstract;

namespace ShoesStore.WebUI.Models
{
    public class CategoryInfo
    {
/*
        public CategoryInfo(IProductRepository repo)
        {

        }
*/
        public IEnumerable<string> Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}