using BLL.Interfaces;
using BLL.Services;
using ClassLibrary1.Entities;
using ConsoleApp1.Models;
using DAL.UoW;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject1.ServicesTests
{
    internal class RegisteredUserServiceTests
    {
        [Test]
        public void RegisteredUserService_GetById_ReturnsRegisteredUserDto()
        {
            var expected = UnitTestHelper.GetRegisteredUsersDto().ElementAt(0);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.GetById(It.IsAny<int>()))
                .Returns(UnitTestHelper.GetUnitTestDb().RegisteredUsers[0]);
            IRegisteredUserService registeredUserService = new RegisteredUserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = registeredUserService.GetById(1);

            EqualityComparersHelper.CompareEqualityRegisteredUserDto(expected, actual);
        }

        [Test]
        public void RegisteredUserService_GetAll_ReturnsAllRegisteredUserDto()
        {
            var expected = UnitTestHelper.GetRegisteredUsersDto();

            var mockUnitOfWork = MoqGenerator.GetMoq();

            IRegisteredUserService registeredUserService = new RegisteredUserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = registeredUserService.GetAll();

            for (int i = 0; i < expected.Count(); i++)
            {
                var expectedObj = expected.ElementAt(i);
                var actualObj = actual.ElementAt(i);
                EqualityComparersHelper.CompareEqualityRegisteredUserDto(expectedObj, actualObj);
            }
        }

        [Test]
        public void RegisteredUserService_Add_AddsRegisteredUserDto()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.Add(It.IsAny<RegisteredUser>()));

            IRegisteredUserService registeredUserService = new RegisteredUserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var registeredUser = UnitTestHelper.GetRegisteredUserDto();
            registeredUser.Id = 5;

            registeredUserService.Add(registeredUser);
            var expected = UnitTestHelper.GetRegisteredUser(UnitTestHelper.GetUnitTestDb());
            expected.Id = 5;

            mockUnitOfWork.Verify(x => x.RegisteredUserRepo.Add(It.Is<RegisteredUser>(o =>
            EqualityComparersHelper.CompareEqualityRegisteredUser(expected, o))), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        public void RegisteredUserService_Delete_DeletesRegisteredUser(int registeredUserId)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.DeleteById(It.IsAny<int>()));
            IRegisteredUserService registeredUserService = new RegisteredUserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            registeredUserService.DeleteById(registeredUserId);

            mockUnitOfWork.Verify(x => x.RegisteredUserRepo.DeleteById(registeredUserId), Times.Once);
        }

        [Test]
        public void RegisteredUserService_Update_UpdateRegisteredUser()
        {
            var registeredUser = UnitTestHelper.GetRegisteredUserDto();
            registeredUser.Id = 1;

            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.Update(It.IsAny<RegisteredUser>()));

            IRegisteredUserService registeredUserService = new RegisteredUserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            registeredUserService.Update(registeredUser);

            var expected = UnitTestHelper.GetRegisteredUser(UnitTestHelper.GetUnitTestDb());

            mockUnitOfWork.Verify(x => x.RegisteredUserRepo.Update(It.Is<RegisteredUser>(o =>
            EqualityComparersHelper.CompareEqualityRegisteredUser(expected, o))), Times.Once);
        }
        [Test]
        public void RegisteredUserService_GetbyLoginInfo()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();

            IRegisteredUserService registeredUserService = new RegisteredUserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            LoginInfo loginInfo = new LoginInfo() { Email = "@Konovalov", Password = "Yagd2851" };

            var expected = UnitTestHelper.GetRegisteredUsersDto().ElementAt(0);

            var actual = registeredUserService.GetbyLoginInfo(loginInfo);

            EqualityComparersHelper.CompareEqualityRegisteredUserDto(expected, actual);
        }

        [Test]
        public void RegisteredUserService_AttachOrderToUser()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.Update(It.IsAny<RegisteredUser>()));
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.GetById(It.IsAny<int>())).Returns(UnitTestHelper.GetUnitTestDb().RegisteredUsers.ElementAt(0));

            IRegisteredUserService registeredUserService = new RegisteredUserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            registeredUserService.AttachOrderToUser(1, 1);

            var expected = UnitTestHelper.GetUnitTestDb().RegisteredUsers.ElementAt(0);
            expected.Orders = new List<Order>() { UnitTestHelper.GetUnitTestDb().Orders.ElementAt(0) };

            mockUnitOfWork.Verify(x => x.RegisteredUserRepo.Update(It.Is<RegisteredUser>(o =>
            EqualityComparersHelper.CompareEqualityRegisteredUser(expected, o))), Times.Once);
        }
    }
}
