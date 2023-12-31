using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    }
}