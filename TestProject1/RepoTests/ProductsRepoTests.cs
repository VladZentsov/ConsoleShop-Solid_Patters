using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClassLibrary1.Data;
using DAL.Repositories;
using ClassLibrary1.Enums;
using ClassLibrary1.Entities;
using System.Linq;
using ClassLibrary1.Validation;

namespace TestProject1.RepoTests
{
    [TestFixture]
    public class ProductsRepoTests
    {
        [Test]
        public void ProductsRepo_FindAll_ReturnsAllValues()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);

            var products = ProductsRepo.FindAll();

            Assert.AreEqual(3, products.Count());
        }
        [Test]
        public void ProductsRepo_GetById_ReturnsSingleValue()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);

            var product = ProductsRepo.GetById(1);

            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("Play Station 5", product.Name);
            Assert.AreEqual(23000, product.Price);
        }
        [Test]
        public void ProductsRepo_Add_AddsValueToDatabase()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);
            var product = UnitTestHelper.GetProduct();

            ProductsRepo.Add(product);

            Assert.AreEqual(4, context.Products.Count);
        }
        [Test]
        public void ProductsRepo_Add_NullProduct()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);

            Assert.Throws<ArgumentNullException>(() => ProductsRepo.Add(null));
        }
        [Test]
        public void ProductsRepo_Add_NullName()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);
            var product = UnitTestHelper.GetProduct();
            product.Name = null;

            Assert.Throws<ShopException>(() => ProductsRepo.Add(product));
        }
        [Test]
        public void ProductsRepo_DeleteById()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);
            ProductsRepo.DeleteById(1);

            Assert.AreEqual(2, context.Products.Count);
        }
        [Test]
        public void ProductsRepo_DeleteById_NoSuchId()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);

            Assert.Throws<ShopException>(() => ProductsRepo.DeleteById(10));
        }
        [Test]
        public void ProductsRepo_Update_ByEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);

            var product = UnitTestHelper.GetProduct();

            ProductsRepo.Update(product);

            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("Play Station 4", product.Name);
            Assert.AreEqual(9000, product.Price);
        }
        [Test]
        public void ProductsRepo_Update_NullEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);

            Assert.Throws<ArgumentNullException>(() => ProductsRepo.Update(null));
        }
        [Test]
        public void ProductsRepo_Update_NullName()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var ProductsRepo = new ProductsRepo(context);

            var product = UnitTestHelper.GetProduct();
            product.Name = null;

            Assert.Throws<ShopException>(() => ProductsRepo.Update(product));
        }
    }
}
