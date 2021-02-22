using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SportsStore.Domain.Entities
{
    public class CartLine
    {
        public SchoesModel SchoesModel { get; set; }
        public int Quantity { get; set; }
    }
}
