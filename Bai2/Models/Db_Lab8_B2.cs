using Microsoft.EntityFrameworkCore;

namespace Bai2.Models
{
    public class Db_Lab8_B2 : DbContext
    {
        public DbSet<Customers> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=C#4_Lab8_B2;Integrated Security=True;TrustServerCertificate=true");
            //optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=DuAn1_C#;Integrated Security=True ; TrustServerCertificate=true");
        }
    }
}
