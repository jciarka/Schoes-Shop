using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ShoesStore.Domain;
using ShoesStore.Domain.Entities;

namespace ShoesStore.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Product> Products {get; set;}
    }
}


