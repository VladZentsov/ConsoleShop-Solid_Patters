using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary1.Enums;

namespace ClassLibrary1.Data
{
    public class StoreDB : IStoreDB, IDisposable
    {
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<RegisteredUser> RegisteredUsers { get; set; }
        public List<Administrator> Administrators { get; set; }

        public StoreDB()
        {
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Play Station 5",
                    Price = 23000,
                    Description ="CPU: x86-64-AMD Ryzen Zen 8 Cores / 16 Threads at 3.5GHz\n" +
                    "GPU: Ray Tracing Acceleration Up to 2.23 GHz (10.3 TFLOPS)\n" +
                    "GPU Architecture: AMD Radeon RDNA 2-based graphics engine\n" +
                    "Memory/Interface: 16GB GDDR6/256-bit"
                },
                new Product()
                {
                    Id=2,
                    Name ="Xbox Series X",
                    Price =21000,
                    Description ="CPU: AMD 8-core Zen 2 architecture @ 3.8 GHz (3.6 GHz with SMT)\n" +
                    "GPU: GPU with 52 compute units on AMD RDNA 2 architecture with a performance of 12 teraflops\n" +
                    "Memory/Interface: 16 GB GDDR6 with 320-bit bus"
                },
                new Product()
                {
                    Id=3,
                    Name ="Xbox Series S",
                    Price = 11000,
                    Description ="CPU: Modified AMD Zen 2 8 cores @ 3.6GHz (3.4GHz with SMT)\n" +
                    "GPU: Modified RDNA 2 20 CUs @ 1.565 GHz 4 TFLOPS\n" +
                    "Memory/Interface: 10 ГБ GDDR6 с 128-битной шиной 8 ГБ @ 224 ГБ/с, 2 ГБ @ 56 ГБ/с"
                }
            };
            RegisteredUsers = new List<RegisteredUser>()
            {
                new RegisteredUser(){
                    Id = 1,
                    Name ="Max",
                    Surname ="Konovalov",
                    CreatedDate=new DateTime(2010, 4,22),
                    Email="@Konovalov",
                    Pass="Yagd2851",
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
            Orders = new List<Order>()
            {
                new Order() { Id = 1, 
                    CreationTime = new DateTime(2022, 1, 10), 
                    Customer=RegisteredUsers[0],
                    Status=Status.NotCompleted,
                    Products=this.Products.GetRange(0,2)
                },
                new Order() {
                    Id = 2,
                    CreationTime = new DateTime(2022,1,15),
                    Customer=RegisteredUsers[1],
                    Status=Status.NotCompleted,
                    Products=this.Products.GetRange(1,2)
                    },
                new Order() {
                    Id=3,
                    CreationTime = new DateTime(2022,1,17),
                    Customer = RegisteredUsers[2],
                    Status=Status.NotCompleted,
                    Products=this.Products.GetRange(2,1)
                },

            };
            RegisteredUsers[0].Orders =new List<Order>() { Orders[0] };
            RegisteredUsers[1].Orders = new List<Order>() { Orders[1] };
            RegisteredUsers[2].Orders = new List<Order>() { Orders[2] };
            Administrators = new List<Administrator>()
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
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
