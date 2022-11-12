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

            base.Load(builder);
        }
    }
}

