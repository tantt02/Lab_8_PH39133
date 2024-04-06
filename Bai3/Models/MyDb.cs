using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bai3.Models
{
    public class MyDb : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Carts> Carts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=C#4_Lab8_B3;Integrated Security=True;TrustServerCertificate=true");
            //optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=DuAn1_C#;Integrated Security=True ; TrustServerCertificate=true");
        }
    }
}
