using ClassLibrary1.Entities;
using DAL.UoW;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1
{
    internal static class MoqGenerator
    {
        public static Mock<IUnitOfWork> GetMoq()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepo.FindAll()).Returns(UnitTestHelper.GetUnitTestDb().Orders);
            mockUnitOfWork.Setup(x => x.ProductsRepo.FindAll()).Returns(UnitTestHelper.GetUnitTestDb().Products);
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.FindAll()).Returns(UnitTestHelper.GetUnitTestDb().RegisteredUsers);
            mockUnitOfWork.Setup(x => x.AdministratorRepo.FindAll()).Returns(UnitTestHelper.GetUnitTestDb().Administrators);
            mockUnitOfWork.Setup(x => x.OrderRepo.CheckExistence(It.IsAny<int>())).Returns(true);
            mockUnitOfWork.Setup(x => x.RegisteredUserRepo.CheckExistence(It.IsAny<int>())).Returns(true);

            return mockUnitOfWork;
        }
    }
}
