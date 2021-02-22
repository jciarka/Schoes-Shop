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
    public class SchoesDestiny
    {
        public SchoesDestiny(string name)
        {
            this.SchoesDestinyName = name;
            this.SchoesModel = new HashSet<SchoesModel>();
        }
        public SchoesDestiny()
        {
            this.SchoesModel = new HashSet<SchoesModel>();
        }

        public int SchoesDestinyID { get; set; }

        [Required(ErrorMessage = "proszę podać nazwę buta")]
        public string SchoesDestinyName { get; set; }

        public virtual ICollection<SchoesModel> SchoesModel { get; set; }
    }
}
