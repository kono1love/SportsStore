﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    /// <summary>
    /// Summary description for AdminTests
    /// </summary
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                 new Product {ProductID = 2, Name = "P2" },
                  new Product {ProductID = 3, Name = "P3" }
            });
            //Arrange
            AdminController target = new AdminController(mock.Object);

            //Action 
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            //Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);

        }
        [TestMethod]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P1" },
                new Product {ProductID = 3, Name = "P1" },
            });
            AdminController target = new AdminController(mock.Object);

            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }
        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P1" },
                new Product {ProductID = 3, Name = "P1" },
                });
            AdminController target = new AdminController(mock.Object);

            Product result = (Product)target.Edit(4).ViewData.Model;

            Assert.IsNull(result);
        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //Arrange create controller
            AdminController target = new AdminController(mock.Object);
            //Arrange create prod
            Product product = new Product { Name = "Test" };

            //Act
            ActionResult result = target.Edit(product);
            //Assert - checked that the repo was called
            mock.Verify(m => m.SaveProduct(product));
            //Assert = checked method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_Invalid_Changes() {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //Arrange create controller
            AdminController target = new AdminController(mock.Object);
            //Arrange create prod
            Product product = new Product { Name = "Test" };
            //Arrange add an error to the model state 
            target.ModelState.AddModelError("error", "error");
            //Act try to save
            ActionResult result = target.Edit(product);
          //Assert - checked that the repo was called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()),Times.Never());
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        public void Can_Delete_Valid_Products()
        {
            //Arrange -create a prod
            Product prod = new Product { ProductID = 2, Name = "Test" };
            //Arrange = mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                 new Product {ProductID = 3, Name = "P3" }
            });
            //Arrange controller
            AdminController target = new AdminController(mock.Object);
            //ACt
            target.Delete(prod.ProductID);
            //Assert
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }
    }
}
