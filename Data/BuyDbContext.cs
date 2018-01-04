using BuyWebPage.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BuyWebPage.Data

{
    public class BuyDbContext :IdentityDbContext<Users>
    {
        //tienen que tener gets and set para poder hacer las tablas de 
        //esas propiedades
        public BuyDbContext(DbContextOptions<BuyDbContext> ctx): base(ctx)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
