using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
