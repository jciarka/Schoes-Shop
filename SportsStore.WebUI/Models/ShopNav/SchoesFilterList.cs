using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoesStore.Domain.Entities.FilterHelperClasses;

namespace ShoesStore.WebUI.Models.ShopNav
{
    public class SchoesFilterList
    {
        //Klasa powstała do obsługi panelu z iltrami produktów na stronie

        //Filtry wybrane wcześniej przez użytkownika
        public SchoesFilterInfo SchoesFilterInfo { get; set; }


        //Zawartość filtrów z pośród której będzie można wybrać
        public string[] schoesModelUser { get; set; }

        public string[] subCategory { get; set; }

        public string[] destiny { get; set; }

        public string[] brand { get; set; }

        public string[] colour { get; set; }

        public string[] soleFabric { get; set; }

        public string[] shankFabric { get; set; }

        public string[] insideFabric { get; set; }

        public string[] bindingMethod { get; set; }       
    }
}