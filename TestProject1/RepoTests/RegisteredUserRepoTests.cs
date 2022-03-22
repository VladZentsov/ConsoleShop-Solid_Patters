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
    public class RegisteredUserRepoTests
    {
        [Test]
        public void RegisteredUsersRepo_FindAll_ReturnsAllValues()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);

            var registeredUsers = RegisteredUsersRepo.FindAll();

            Assert.AreEqual(3, registeredUsers.Count());
        }
        [Test]
        public void RegisteredUsersRepo_GetById_ReturnsSingleValue()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);


            var registeredUser = RegisteredUsersRepo.GetById(1);

            Assert.AreEqual(1, registeredUser.Id);
            Assert.AreEqual("Max", registeredUser.Name);
            Assert.AreEqual("Konovalov", registeredUser.Surname);
            Assert.AreEqual(new DateTime(2010, 4, 22), registeredUser.CreatedDate);
            Assert.AreEqual("@Konovalov", registeredUser.Email);
            Assert.AreEqual("Yagd2851", registeredUser.Pass);
        }
        [Test]
        public void RegisteredUsersRepo_Add_AddsValueToDatabase()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);
            var registeredUser = UnitTestHelper.GetRegisteredUser(context);

            RegisteredUsersRepo.Add(registeredUser);

            Assert.AreEqual(4, context.RegisteredUsers.Count);
        }
        [Test]
        public void RegisteredUsersRepo_Add_NullRegisteredUser()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);

            Assert.Throws<ArgumentNullException>(() => RegisteredUsersRepo.Add(null));
        }
        [Test]
        public void RegisteredUsersRepo_Add_NullName()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);
            var registeredUser = UnitTestHelper.GetRegisteredUser(context);
            registeredUser.Name = null;

            Assert.Throws<ShopException>(() => RegisteredUsersRepo.Add(registeredUser));
        }
        [Test]
        public void RegisteredUsersRepo_DeleteById()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);
            RegisteredUsersRepo.DeleteById(1);

            Assert.AreEqual(2, context.RegisteredUsers.Count);
        }
        [Test]
        public void RegisteredUsersRepo_DeleteById_NoSuchId()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);

            Assert.Throws<ShopException>(() => RegisteredUsersRepo.DeleteById(10));
        }
        [Test]
        public void RegisteredUsersRepo_Update_ByEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);

            var registeredUser = UnitTestHelper.GetRegisteredUser(context);

            RegisteredUsersRepo.Update(registeredUser);

            Assert.AreEqual(1, registeredUser.Id);
            Assert.AreEqual("Artem", registeredUser.Name);
            Assert.AreEqual("Sadoshenko", registeredUser.Surname);
            Assert.AreEqual(new DateTime(2005, 2, 14), registeredUser.CreatedDate);
            Assert.AreEqual("@Sadoshenko", registeredUser.Email);
            Assert.AreEqual("Afdc3641", registeredUser.Pass);
            Assert.AreEqual(context.Orders.GetRange(0, 3), registeredUser.Orders);
        }
        [Test]
        public void RegisteredUsersRepo_Update_NullEntity()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);

            Assert.Throws<ArgumentNullException>(() => RegisteredUsersRepo.Update(null));
        }
        [Test]
        public void RegisteredUsersRepo_Update_NullName()
        {
            using var context = UnitTestHelper.GetUnitTestDb();
            var RegisteredUsersRepo = new RegisteredUserRepo(context);

            var registeredUser = UnitTestHelper.GetRegisteredUser(context);
            registeredUser.Name = null;

            Assert.Throws<ShopException>(() => RegisteredUsersRepo.Update(registeredUser));
        }
    }
}
