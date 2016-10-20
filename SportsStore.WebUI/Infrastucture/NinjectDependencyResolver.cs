using System;
using System.Collections.Generic;

using System.Web.Mvc;
using Ninject;
using  Moq;
using SportsStore.Domain.Abstract;
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
            //pull Bindings heremoc
            Mock<IProductRepository> mock = new Mock<IProductRepository>(); 
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name = "Football", Price = 25},
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
            });
            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}