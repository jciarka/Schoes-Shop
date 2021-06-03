using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoesStore.Domain.Entities;
using System.Linq;

namespace ShoesStore.UnitTests
{
    /*
    /// <summary>
    /// Opis podsumowujący elementu CartTests
    /// </summary>
    [TestClass]
    public class CartTests
    {
        public CartTests()
        {
            //
            // TODO: Dodaj tutaj logikę konstruktora
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Pobiera lub ustawia kontekst testu, który udostępnia
        ///funkcjonalność i informację o bieżącym przebiegu testu.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Dodatkowe atrybuty testu
        //
        // Można użyć następujących dodatkowych atrybutów w trakcie pisania testów:
        //
        // Użyj ClassInitialize do uruchomienia kodu przed uruchomieniem pierwszego testu w klasie
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Użyj ClassCleanup do uruchomienia kodu po wykonaniu wszystkich testów w klasie
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Użyj TestInitialize do uruchomienia kodu przed uruchomieniem każdego testu 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Użyj TestCleanup do uruchomienia kodu po wykonaniu każdego testu
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Can_Add_New_Line()
        {
            //przygotowanie
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            //działanie
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            CartLine[] result = target.Lines.ToArray();

            //asetcje
            Assert.AreEqual(target.Lines.Count(), 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Line()
        {
            //przygotowanie

            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            //działanie
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);


            CartLine[] result = target.Lines.ToArray();

            //asetcje
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
        }


        [TestMethod]
        public void Can_Remove_Line()
        {
            //przygotowanie

            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            //działanie
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);

            target.RemoveLine(p2);

            CartLine[] result = target.Lines.ToArray();

            //asetcje
            Assert.AreEqual(target.Lines.Where(c=>c.Product == p2).Count(),0);
            Assert.AreEqual(target.Lines.Count(),2);

        }
    }
    */
}
