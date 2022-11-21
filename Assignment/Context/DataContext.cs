using Assignment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderRowsEntity> OrderRows { get; set; }
    }
   
   

}
