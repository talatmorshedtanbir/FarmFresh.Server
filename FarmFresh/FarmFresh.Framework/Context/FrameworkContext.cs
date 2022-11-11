using FarmFresh.Framework.Entities.Carts;
using FarmFresh.Framework.Entities.Categories;
using FarmFresh.Framework.Entities.Orders;
using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Entities.Roles;
using FarmFresh.Framework.Entities.Stores;
using FarmFresh.Framework.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Framework.Context
{
    public class FrameworkContext : DbContext
    {

        private string _connectionString;
        private string _migrationAssemblyName;

        public FrameworkContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<ProductStore> ProductStores { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CustomerCart> CustomerCarts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}
