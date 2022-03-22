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
    public class OrderRepoTests
    {
        [Test]
        public void OrderRepo_FindAll_ReturnsAllValues()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            var orders = OrderRepo.FindAll();

            Assert.AreEqual(4, orders.Count());
        }
        [Test]
        public void OrderRepo_GetById_ReturnsSingleValue()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            var order = OrderRepo.GetById(1);

            Assert.AreEqual(1, order.Id);
            Assert.AreEqual(new DateTime(2022, 1, 10), order.CreationTime);
            Assert.AreEqual(Status.NotCompleted, order.Status);
            Assert.AreEqual(context.RegisteredUsers[0], order.Customer);
        }
        [Test]
        public void OrderRepo_Add_AddsValueToDatabase()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);
            var order = UnitTestHelper.GetOrder(context);

            OrderRepo.Add(order);

            Assert.AreEqual(5, context.Orders.Count);
        }
        [Test]
        public void OrderRepo_Add_NullOrder()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            Assert.Throws<ArgumentNullException>(() => OrderRepo.Add(null));
        }
    [Test]
        public void OrderRepo_Add_NullProduct()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);
            var order = UnitTestHelper.GetOrder(context);
            order.Products = null;

            Assert.Throws<ShopException>(() => OrderRepo.Add(order));
        }
    [Test]
        public void OrderRepo_DeleteById()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);
            OrderRepo.DeleteById(1);

            Assert.AreEqual(3, context.Orders.Count);
        }
        [Test]
        public void OrderRepo_DeleteById_NoSuchId()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            Assert.Throws<ShopException>(() => OrderRepo.DeleteById(10));
        }
        [Test]
        public void OrderRepo_Update_ByEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            var order = UnitTestHelper.GetOrder(context);

            OrderRepo.Update(order);

            var orerFromDB = context.Orders.FirstOrDefault(o => o.Id == 1);

            Assert.AreEqual(1, orerFromDB.Id);
            Assert.AreEqual(new DateTime(2022, 1, 23), orerFromDB.CreationTime);
            Assert.AreEqual(context.RegisteredUsers[2], orerFromDB.Customer);
            Assert.AreEqual(Status.CanceledByAdmin, orerFromDB.Status);
            Assert.AreEqual(context.Products.GetRange(0, 2), orerFromDB.Products);
        }
        [Test]
        public void OrderRepo_Update_NullEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            Assert.Throws<ArgumentNullException>(() => OrderRepo.Update(null));
        }
        [Test]
        public void OrderRepo_Update_NullProducts()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            var order = UnitTestHelper.GetOrder(context);
            order.Products = new List<Product>();

            Assert.Throws<ShopException>(() => OrderRepo.Update(order));
        }
        [Test]
        public void OrderRepo_Update_NullCustomer()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var OrderRepo = new OrderRepo(context);

            var order = UnitTestHelper.GetOrder(context);
            order.Customer = null;

            Assert.Throws<ShopException>(() => OrderRepo.Update(order));
        }
    }
}
