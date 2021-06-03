using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoesStore.Domain.Abstract;
using ShoesStore.Domain;
using ShoesStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using ShoesStore.WebUI.Models;
using ShoesStore.WebUI.HtmlHelpers;
using ShoesStore.Domain.Entities;


namespace ShoesStore.UnitTests
{/*
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductID = 1, Name = "Piłka nożna", Price = 25 },
                new Product { ProductID = 2, Name = "Deska surfingowa", Price = 179 },
                new Product { ProductID = 3, Name = "Buty do biegania", Price = 95 },
                new Product { ProductID = 4, Name = "Deskorolka", Price = 279 },
                new Product { ProductID = 5, Name = "Łyżwy", Price = 195 }
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //IEnumerable<Product> 
            ProductsListViewModel resultModel = (ProductsListViewModel)controller.List(null,2).Model;
            IEnumerable<Product> result = resultModel.Products;

            //asercje
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "Deskorolka");
            Assert.AreEqual(prodArray[1].Name, "Łyżwy");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //przygotowanie
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            //dzialanie
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            string s1 = @"<a class=""btn btn-default"" href=""Strona1"">1</a>";
            string s2 = @"<a class=""btn btn-default btn-primary selected"" href=""Strona2"">2</a>";
            string s3 = @"<a class=""btn btn-default"" href=""Strona3"">3</a>";

            //assercje
            Assert.AreEqual((s1+s2+s3), result.ToString());
        }
    }
    */
}
