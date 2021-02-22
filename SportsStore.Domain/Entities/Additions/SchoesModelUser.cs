using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Domain.Entities.Additions
{
    public class SchoesModelUser
    {
        public SchoesModelUser(string name, string prezentationText)
        {
            this.SchoesModelUserName = name;
            this.SchoesModelUserText = prezentationText;
            this.SchoesModel = new HashSet<SchoesModel>();
        }
        public SchoesModelUser()
        {
            this.SchoesModel = new HashSet<SchoesModel>();
        }
        public int SchoesModelUserID { get; set; }

        [Required(ErrorMessage = "Dla kogo przenzaczone są te buty")]
        public string SchoesModelUserName { get; set; }

        [Required(ErrorMessage = "Tekst prezentacyjny np.: Dla niego")]
        public string SchoesModelUserText { get; set; }

        public virtual ICollection<SchoesModel> SchoesModel { get; set; }
    }
}
