using AutoMapper;
using BLL;
using BLL.Dto;
using ClassLibrary1.Data;
using ClassLibrary1.Entities;
using ClassLibrary1.Enums;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    internal static class UnitTestHelper
    {
        public static StoreDB GetUnitTestDb()
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Play Station 5",
                    Price = 23000
                },
                new Product()
                {
                    Id=2,
                    Name ="Xbox Series X",
                    Price =21000
                },
                new Product()
                {
                    Id=3,
                    Name ="Xbox Series S",
                    Price = 11000
                }
            };
            var registeredUsers = new List<RegisteredUser>()
                {
                    new RegisteredUser(){
                        Id = 1,
                        Name ="Max",
                        Surname ="Konovalov",
                        CreatedDate=new DateTime(2010, 4,22),
                        Email="@Konovalov",
                        Pass="Yagd2851"
                    },
                    new RegisteredUser()
                    {
                        Id = 2,
                        Name ="Ivan",
                        Surname = "Ivanov",
                        CreatedDate = new DateTime(2012,10,11),
                        Email="@Ivanov",
                        Pass ="Osnv2852"
                    },
                    new RegisteredUser()
                    {
                        Id=3,
                        Name ="Petro",
                        Surname="Petrov",
                        CreatedDate=new DateTime(2008,5,12),
                        Email="@Petrov",
                        Pass = "Hdks3715"
                    }
                };
            var orders = new List<Order>()
                {
                    new Order() { Id = 1,
                        CreationTime = new DateTime(2022, 1, 10),
                        Customer=registeredUsers[0],
                        Status=Status.NotCompleted,
                        Products=products.GetRange(0,2)
                    },
                    new Order() {
                        Id = 2,
                        CreationTime = new DateTime(2022,1,15),
                        Customer=registeredUsers[1],
                        Status=Status.NotCompleted,
                        Products=products.GetRange(1,2)
                        },
                    new Order() {
                        Id=3,
                        CreationTime = new DateTime(2022,1,17),
                        Customer = registeredUsers[2],
                        Status=Status.CanceledByUser,
                        Products=products.GetRange(2,1)
                    },
                    new Order(){
                        Id=4,
                        CreationTime = new DateTime(2022,1,20),
                        Customer = registeredUsers[2],
                        Status=Status.Sent,
                        Products=products.GetRange(2,1)
                    }

                };
            var administrators = new List<Administrator>()
            {
                new Administrator(){
                        Id = 1,
                        Name ="Alexey",
                        Surname ="Ostrovskiy",
                        CreatedDate=new DateTime(2010, 4,22),
                        Email="@Ostrovskiy",
                        Pass="Yagd2851"
                    },
                    new Administrator()
                    {
                        Id = 2,
                        Name ="Anton",
                        Surname = "Kamenev",
                        CreatedDate = new DateTime(2012,10,11),
                        Email="@Kamenev",
                        Pass ="Osnv2852"
                    },
                    new Administrator()
                    {
                        Id=3,
                        Name ="Artem",
                        Surname="Manukian",
                        CreatedDate=new DateTime(2008,5,12),
                        Email="@Manukian",
                        Pass = "Hdks3715"
                    }
            };
            StoreDB db = new StoreDB()
            {
                RegisteredUsers = registeredUsers,
                Orders = orders,
                Products = products,
                Administrators = administrators
            };
            return db;
        }

        public static Order GetOrder(StoreDB context)
        {
            return new Order() {
                Id = 1,
                CreationTime = new DateTime(2022, 1, 23),
                Customer = context.RegisteredUsers[2],
                Status = Status.CanceledByAdmin,
                Products = context.Products.GetRange(0, 2)
            };
        }

        public static Product GetProduct()
        {
            return new Product() {
                Id = 1,
                Name = "Play Station 4",
                Price = 9000
            };
        }
        public static RegisteredUser GetRegisteredUser(StoreDB context)
        {
            return new RegisteredUser() {
                Id = 1,
                Name = "Artem",
                Surname = "Sadoshenko",
                CreatedDate = new DateTime(2005, 2, 14),
                Email = "@Sadoshenko",
                Pass = "Afdc3641",
                Orders = context.Orders.GetRange(0, 3)
            };

        }
        public static IEnumerable<ProductDto> GetProductsDto()
        {
            var products = new List<ProductDto>()
            {
                new ProductDto()
                {
                    Id = 1,
                    Name = "Play Station 5",
                    Price = 23000
                },
                new ProductDto()
                {
                    Id=2,
                    Name ="Xbox Series X",
                    Price =21000
                },
                new ProductDto()
                {
                    Id=3,
                    Name ="Xbox Series S",
                    Price = 11000
                }
            };
            return products;
        }
        public static IEnumerable<RegisteredUserDto> GetRegisteredUsersDto()
        {
            var registeredUsers = new List<RegisteredUserDto>()
            {
                new RegisteredUserDto(){
                    Id = 1,
                    Name ="Max",
                    Surname ="Konovalov",
                    CreatedDate=new DateTime(2010, 4,22),
                    Email="@Konovalov",
                    Pass="Yagd2851",
                    OrderIds=new List<int>()
                },
                new RegisteredUserDto()
                {
                    Id = 2,
                    Name ="Ivan",
                    Surname = "Ivanov",
                    CreatedDate = new DateTime(2012,10,11),
                    Email="@Ivanov",
                    Pass ="Osnv2852",
                    OrderIds=new List<int>()
                },
                new RegisteredUserDto()
                {
                    Id=3,
                    Name ="Petro",
                    Surname="Petrov",
                    CreatedDate=new DateTime(2008,5,12),
                    Email="@Petrov",
                    Pass = "Hdks3715",
                    OrderIds=new List<int>()
                }
            };
            return registeredUsers;
        }
        public static RegisteredUserDto GetRegisteredUserDto()
        {
            return new RegisteredUserDto()
            {
                Id = 1,
                Name = "Artem",
                Surname = "Sadoshenko",
                CreatedDate = new DateTime(2005, 2, 14),
                Email = "@Sadoshenko",
                Pass = "Afdc3641",
                OrderIds = new List<int>() { 1, 2, 3 }
            };
        }
        public static IEnumerable<OrderDto> GetOrdersDto()
        {
            var orders = new List<OrderDto>()
            {
                new OrderDto() { Id = 1,
                    CreationTime = new DateTime(2022, 1, 10),
                    CustomerId=1,
                    Status=Status.NotCompleted,
                    ProductsIds = new List<int>(){1,2},
                    CustomerRole = Roles.RegisteredUser
                },
                new OrderDto() {
                    Id = 2,
                    CreationTime = new DateTime(2022,1,15),
                    CustomerId=2,
                    Status=Status.NotCompleted,
                    ProductsIds = new List<int>(){2,3},
                    CustomerRole = Roles.RegisteredUser
                    },
                new OrderDto() {
                    Id=3,
                    CreationTime = new DateTime(2022,1,17),
                    CustomerId=3,
                    Status=Status.CanceledByUser,
                    ProductsIds = new List<int>(){3},
                    CustomerRole = Roles.RegisteredUser
                },
                    new OrderDto(){
                    Id=4,
                    CreationTime = new DateTime(2022,1,20),
                    CustomerId = 3,
                    Status=Status.Sent,
                    ProductsIds = new List<int>(){3},
                    CustomerRole = Roles.RegisteredUser
                }
            };
            return orders;
        }
        public static Mapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }
        public static ProductDto GetProductDto()
        {
            return new ProductDto() {
                Id= 4, 
                Name= "Play Station 4",
                Price = 9000
            };
        }
        public static OrderDto GetOrderDto()
        {
            OrderDto orderDto = new OrderDto()
            {
                Id = 4,
                CreationTime = new DateTime(2022, 1, 23),
                CustomerId = 3,
                Status = Status.CanceledByAdmin, 
                ProductsIds = new List<int> { 1,2}, 
                CustomerRole = Roles.RegisteredUser

            };
            return orderDto;
        }
        public static Administrator GetAdministrator(StoreDB context)
        {
            return new Administrator()
            {
                Id = 1,
                Name = "Artem",
                Surname = "Sadoshenko",
                CreatedDate = new DateTime(2005, 2, 14),
                Email = "@Sadoshenko",
                Pass = "Afdc3641",
                Orders = context.Orders.GetRange(0, 3)
            };
        }
        public static IEnumerable<AdministratorDto> GetAdministratorsDto()
        {
            var administrators = new List<AdministratorDto>()
            {
                new AdministratorDto(){
                    Id = 1,
                    Name ="Alexey",
                    Surname ="Ostrovskiy",
                    CreatedDate=new DateTime(2010, 4,22),
                    Email="@Ostrovskiy",
                    Pass="Yagd2851",
                    OrderIds=new List<int>()
                },
                new AdministratorDto()
                {
                    Id = 2,
                    Name ="Anton",
                    Surname = "Kamenev",
                    CreatedDate = new DateTime(2012,10,11),
                    Email="@Kamenev",
                    Pass ="Osnv2852",
                    OrderIds=new List<int>()
                },
                new AdministratorDto()
                {
                    Id=3,
                    Name ="Artem",
                    Surname="Manukian",
                    CreatedDate=new DateTime(2008,5,12),
                    Email="@Manukian",
                    Pass = "Hdks3715",
                    OrderIds=new List<int>()
                }
            };
            return administrators;
        }
        public static AdministratorDto GetAdministratorDto()
        {
            return new AdministratorDto()
            {
                Id = 1,
                Name = "Artem",
                Surname = "Sadoshenko",
                CreatedDate = new DateTime(2005, 2, 14),
                Email = "@Sadoshenko",
                Pass = "Afdc3641",
                OrderIds = new List<int>() { 1, 2, 3 }
            };
        }
    }
}