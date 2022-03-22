using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using DAL.UoW;
using BLL.Interfaces;
using BLL.Services;
using BLL.Dto;
using ClassLibrary1.Entities;
using ClassLibrary1.Enums;
using DAL.Enums;

namespace TestProject1.ServicesTests
{
    public class OrderServiceTests
    {
        [Test]
        public void OrderService_GetById_ReturnsOrderDto()
        {
            var expected = UnitTestHelper.GetOrdersDto().ElementAt(0);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepo.GetById(It.IsAny<int>()))
                .Returns(UnitTestHelper.GetUnitTestDb().Orders[0]);
            mockUnitOfWork.Setup(x => x.OrderRepo.CheckExistence(It.IsAny<int>())).Returns(true);
            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = orderService.GetById(1);

            EqualityComparersHelper.CompareEqualityOrderDto(expected, actual);
        }
        [Test]
        public void OrderService_GetAll_ReturnsAllOrderDto()
        {
            var expected = UnitTestHelper.GetOrdersDto();

            var mockUnitOfWork = MoqGenerator.GetMoq();

            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = orderService.GetAll();

            for (int i = 0; i < expected.Count(); i++)
            {
                var expectedObj=expected.ElementAt(i);
                var actualObj=actual.ElementAt(i);
                EqualityComparersHelper.CompareEqualityOrderDto(expectedObj, actualObj);
            }
        }

        [Test]
        public void OrderService_Add_AddsOrderDto()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.OrderRepo.Add(It.IsAny<Order>()));

            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var order = UnitTestHelper.GetOrderDto();
            order.Id = 5;

            orderService.Add(order);
            var expected = UnitTestHelper.GetOrder(UnitTestHelper.GetUnitTestDb());
            expected.Id = 5;

            mockUnitOfWork.Verify(x => x.OrderRepo.Add(It.Is<Order>(o=> 
            EqualityComparersHelper.CompareEqualityOrder(expected, o))), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        public void OrderService_Delete_DeletesOrder(int orderId)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x=>x.OrderRepo.DeleteById(It.IsAny<int>()));
            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            orderService.DeleteById(orderId);

            mockUnitOfWork.Verify(x => x.OrderRepo.DeleteById(orderId), Times.Once);
        }

        [Test]
        public void OrderService_Update_UpdateOrder()
        {
            var order = UnitTestHelper.GetOrderDto();
            order.Id = 1;

            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.OrderRepo.Update(It.IsAny<Order>()));

            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            orderService.Update(order);

            var expected = UnitTestHelper.GetOrder(UnitTestHelper.GetUnitTestDb());

            mockUnitOfWork.Verify(x => x.OrderRepo.Update(It.Is<Order>(o =>
            EqualityComparersHelper.CompareEqualityOrder(expected, o))), Times.Once);
        }

        [Test]
        public void OrderService_SetStatus_SetStatusToOrder()
        {
            var order = UnitTestHelper.GetOrderDto();
            order.Id = 1;

            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.OrderRepo.GetById(It.IsAny<int>())).Returns(UnitTestHelper.GetUnitTestDb().Orders[3]);

            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            orderService.SetStatus(4, Status.Recieved);

            var expected = UnitTestHelper.GetOrdersDto().FirstOrDefault(o => o.Id == 4);
            expected.Status = Status.Recieved;

            var actual = orderService.GetById(4);

            EqualityComparersHelper.CompareEqualityOrderDto(expected, actual);
        }

        [Test]
        public void OrderService_GetOrderByUserId()
        {
            var expected = UnitTestHelper.GetOrdersDto().ElementAt(0);

            var mockUnitOfWork = MoqGenerator.GetMoq();

            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = orderService.GetOrdersByUserId(Roles.RegisteredUser, 1);

            EqualityComparersHelper.CompareEqualityOrderDto(expected, actual.ElementAt(0));
        }

        [Test]
        public void OrderService_GetPrice()
        {
            decimal expected = 44000;

            var mockUnitOfWork = MoqGenerator.GetMoq();

            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = orderService.GetPrice(1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderService_Ordering()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepo.GetById(It.IsAny<int>())).Returns(UnitTestHelper.GetUnitTestDb().Orders.ElementAt(0));

            IOrderService orderService = new OrderService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var expected = UnitTestHelper.GetUnitTestDb().Orders.ElementAt(0);
            expected.Status = Status.New;

            var actual = UnitTestHelper.GetUnitTestDb().Orders.ElementAt(0);

            orderService.Ordering(actual.Id);

            mockUnitOfWork.Verify(x => x.OrderRepo.Update(It.Is<Order>(o =>
            EqualityComparersHelper.CompareEqualityOrder(expected, o))), Times.Once);

        }

    }
}
