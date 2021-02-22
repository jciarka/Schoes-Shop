using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Models
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