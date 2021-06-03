using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Domain.Entities.Additions
{
    public class SubCategory
    {
        public SubCategory(string name)
        {
            this.SubCategoryName = name;
            this.SchoesModel = new HashSet<SchoesModel>();
        }

        public SubCategory()
        {
            this.SchoesModel = new HashSet<SchoesModel>();
        }
        public int SubCategoryID { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę kategrii")]
        public string SubCategoryName { get; set; }

        public virtual ICollection<SchoesModel> SchoesModel { get; set; }
    }
}
