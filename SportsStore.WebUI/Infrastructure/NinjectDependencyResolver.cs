using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Ninject;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        public IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            /*
            Mock < IProductRepository > mock = new Mock< IProductRepository >();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Name = "Piłka nożna", Price = 25 },
                new Product { Name = "Deska surfingowa", Price = 179 },
                new Product { Name = "Buty do biegania", Price = 95 }
            });

            kernel.Bind<IProductRepository>().ToConstant(mock.Object); 
            */

            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<ISchoesRepository>().To<SchoesDbContext>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            kernel.Bind<IAuthProvider>().To<AppUserAuthProvider>();


        }



    }
}