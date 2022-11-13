using Autofac;
using FarmFresh.Framework.Context;
using FarmFresh.Framework.Repositories.Abstract;
using FarmFresh.Framework.Repositories.Concrete;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.Services.Concrete;
using FarmFresh.Framework.UnitOfWorks.Abstract;
using FarmFresh.Framework.UnitOfWorks.Concrete;

namespace FarmFresh.Framework
{
    public class FrameworkModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public FrameworkModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FrameworkContext>()
                   .WithParameter("connectionString", _connectionString)
                   .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>().As<IUserRepository>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<UserUnitOfWork>().As<IUserUnitOfWork>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CategoryUnitOfWork>().As<ICategoryUnitOfWork>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductUnitOfWork>().As<IProductUnitOfWork>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductCategoryRepository>().As<IProductCategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductCategoryUnitOfWork>().As<IProductCategoryUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<CartRepository>().As<ICartRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CartUnitOfWork>().As<ICartUnitOfWork>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<CartService>().As<ICartService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CartItemRepository>().As<ICartItemRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CartItemUnitOfWork>().As<ICartItemUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<CustomerCartRepository>().As<ICustomerCartRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CustomerCartUnitOfWork>().As<ICustomerCartUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<OrderUnitOfWork>().As<IOrderUnitOfWork>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderItemRepository>().As<OrderItemRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<OrderItemUnitOfWork>().As<IOrderItemUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<CustomerOrderRepository>().As<ICustomerOrderRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CustomerOrderUnitOfWork>().As<ICustomerOrderUnitOfWork>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

