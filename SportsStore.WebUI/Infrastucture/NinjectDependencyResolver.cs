using System;
using System.Collections.Generic;
using  System.Linq;
using System.Web.Mvc;
using Ninject;
using  Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;


namespace SportsStore.WebUI.Infrastucture
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviveType)
        {
            return kernel.TryGet(serviveType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //pull Bindings here
            /*Mock<IProductRepository> mock = new Mock<IProductRepository>(); 
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name = "Football", Price = 25},
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
            });
            kernel.Bind<IProductRepository>().ToConstant(mock.Object);*/
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}