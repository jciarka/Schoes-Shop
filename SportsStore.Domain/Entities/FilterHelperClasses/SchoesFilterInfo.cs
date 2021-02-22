using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Domain.Entities.FilterHelperClasses
{
    public class SchoesFilterInfo
    {
        public string schoesModelUser { get; set; }

        public string subCategory { get; set; }

        public string destiny { get; set; }

        public string brand { get; set; }

        public int? size { get; set; }

        public int? minPrice { get; set; }

        public int? maxPrice { get; set; }

        public int? isSizeOnStock { get; set; }

        public string colour { get; set; }

        [filterAttribute(true)]
        public string soleFabric { get; set; }

        [filterAttribute(true)]
        public string shankFabric { get; set; }

        [filterAttribute(true)]
        public string insideFabric { get; set; }

        [filterAttribute(true)]
        public string bindingMethod { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class filterAttribute : Attribute
    {
        public bool allowCustomFiltering { get; set; }

        public filterAttribute(bool allowCustomFilteringForStrings)
        {
            allowCustomFiltering = allowCustomFilteringForStrings;
        }
    }
}