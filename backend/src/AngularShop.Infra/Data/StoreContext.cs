using AngularShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularShop.Infra.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) 
                : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}