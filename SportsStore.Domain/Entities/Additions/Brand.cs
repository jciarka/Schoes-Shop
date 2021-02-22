using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities.Additions
{
    public class Brand
    {
        public Brand(string name)
        {
            this.BrandName = name;
            this.SchoesModel = new HashSet<SchoesModel>();
        }

        public Brand()
        { 
            this.SchoesModel = new HashSet<SchoesModel>();
        }

        public int BrandID { get; set; }

        [Required(ErrorMessage ="Proszę podać markę butów")]
        public string BrandName { get; set; }

        public virtual ICollection<SchoesModel> SchoesModel { get; set; }
    }
}
