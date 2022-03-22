using ClassLibrary1.Validation;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject1.RepoTests
{
    [TestFixture]
    public class AdministratorRepoTests
    {

        [Test]
        public void AdministratorsRepo_FindAll_ReturnsAllValues()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);

            var administrators = AdministratorsRepo.FindAll();

            Assert.AreEqual(3, administrators.Count());
        }
        [Test]
        public void AdministratorsRepo_GetById_ReturnsSingleValue()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);


            var administrator = AdministratorsRepo.GetById(1);

            Assert.AreEqual(1, administrator.Id);
            Assert.AreEqual("Alexey", administrator.Name);
            Assert.AreEqual("Ostrovskiy", administrator.Surname);
            Assert.AreEqual(new DateTime(2010, 4, 22), administrator.CreatedDate);
            Assert.AreEqual("@Ostrovskiy", administrator.Email);
            Assert.AreEqual("Yagd2851", administrator.Pass);
        }
        [Test]
        public void AdministratorsRepo_Add_AddsValueToDatabase()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);
            var administrator = UnitTestHelper.GetAdministrator(context);

            AdministratorsRepo.Add(administrator);

            Assert.AreEqual(4, context.Administrators.Count);
        }
        [Test]
        public void AdministratorsRepo_Add_NullAdministrator()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);

            Assert.Throws<ArgumentNullException>(() => AdministratorsRepo.Add(null));
        }
        [Test]
        public void AdministratorsRepo_Add_NullName()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);
            var administrator = UnitTestHelper.GetAdministrator(context);
            administrator.Name = null;

            Assert.Throws<ShopException>(() => AdministratorsRepo.Add(administrator));
        }
        [Test]
        public void AdministratorsRepo_DeleteById()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);
            AdministratorsRepo.DeleteById(1);

            Assert.AreEqual(2, context.Administrators.Count);
        }
        [Test]
        public void AdministratorsRepo_DeleteById_NoSuchId()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);

            Assert.Throws<ShopException>(() => AdministratorsRepo.DeleteById(10));
        }
        [Test]
        public void AdministratorsRepo_Update_ByEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);

            var administrator = UnitTestHelper.GetAdministrator(context);

            AdministratorsRepo.Update(administrator);

            Assert.AreEqual(1, administrator.Id);
            Assert.AreEqual("Artem", administrator.Name);
            Assert.AreEqual("Sadoshenko", administrator.Surname);
            Assert.AreEqual(new DateTime(2005, 2, 14), administrator.CreatedDate);
            Assert.AreEqual("@Sadoshenko", administrator.Email);
            Assert.AreEqual("Afdc3641", administrator.Pass);
            Assert.AreEqual(context.Orders.GetRange(0, 3), administrator.Orders);
        }
        [Test]
        public void AdministratorsRepo_Update_NullEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);

            Assert.Throws<ArgumentNullException>(() => AdministratorsRepo.Update(null));
        }
        [Test]
        public void AdministratorsRepo_Update_NullName()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var AdministratorsRepo = new AdministratorRepo(context);

            var administrator = UnitTestHelper.GetAdministrator(context);
            administrator.Name = null;

            Assert.Throws<ShopException>(() => AdministratorsRepo.Update(administrator));
        }
    }
}
