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
    internal class AdministratorRepoServices
    {
        [Test]
        public void AdministratorService_GetById_ReturnsAdministratorDto()
        {
            var expected = UnitTestHelper.GetAdministratorsDto().ElementAt(0);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AdministratorRepo.GetById(It.IsAny<int>()))
                .Returns(UnitTestHelper.GetUnitTestDb().Administrators[0]);
            IAdministratorService administratorService = new AdministratorService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = administratorService.GetById(1);

            EqualityComparersHelper.CompareEqualityAdministratorDto(expected, actual);
        }

        [Test]
        public void AdministratorService_GetAll_ReturnsAllAdministratorDto()
        {
            var expected = UnitTestHelper.GetAdministratorsDto();

            var mockUnitOfWork = MoqGenerator.GetMoq();

            IAdministratorService administratorService = new AdministratorService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = administratorService.GetAll();

            for (int i = 0; i < expected.Count(); i++)
            {
                var expectedObj = expected.ElementAt(i);
                var actualObj = actual.ElementAt(i);
                EqualityComparersHelper.CompareEqualityAdministratorDto(expectedObj, actualObj);
            }
        }

        [Test]
        public void AdministratorService_Add_AddsAdministratorDto()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.AdministratorRepo.Add(It.IsAny<Administrator>()));

            IAdministratorService administratorService = new AdministratorService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var administrator = UnitTestHelper.GetAdministratorDto();
            administrator.Id = 5;

            administratorService.Add(administrator);
            var expected = UnitTestHelper.GetAdministrator(UnitTestHelper.GetUnitTestDb());
            expected.Id = 5;

            mockUnitOfWork.Verify(x => x.AdministratorRepo.Add(It.Is<Administrator>(o =>
            EqualityComparersHelper.CompareEqualityAdministrator(expected, o))), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        public void AdministratorService_Delete_DeletesAdministrator(int administratorId)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.AdministratorRepo.DeleteById(It.IsAny<int>()));
            IAdministratorService administratorService = new AdministratorService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            administratorService.DeleteById(administratorId);

            mockUnitOfWork.Verify(x => x.AdministratorRepo.DeleteById(administratorId), Times.Once);
        }

        [Test]
        public void AdministratorService_Update_UpdateAdministrator()
        {
            var administrator = UnitTestHelper.GetAdministratorDto();
            administrator.Id = 1;

            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.AdministratorRepo.Update(It.IsAny<Administrator>()));

            IAdministratorService administratorService = new AdministratorService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            administratorService.Update(administrator);

            var expected = UnitTestHelper.GetAdministrator(UnitTestHelper.GetUnitTestDb());

            mockUnitOfWork.Verify(x => x.AdministratorRepo.Update(It.Is<Administrator>(o =>
            EqualityComparersHelper.CompareEqualityAdministrator(expected, o))), Times.Once);
        }
        [Test]
        public void AdministratorService_GetbyLoginInfo()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();

            IAdministratorService administratorService = new AdministratorService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            LoginInfo loginInfo = new LoginInfo() { Email = "@Ostrovskiy", Password = "Yagd2851" };

            var expected = UnitTestHelper.GetAdministratorsDto().ElementAt(0);

            var actual = administratorService.GetbyLoginInfo(loginInfo);

            EqualityComparersHelper.CompareEqualityAdministratorDto(expected, actual);

        }
    }
}
