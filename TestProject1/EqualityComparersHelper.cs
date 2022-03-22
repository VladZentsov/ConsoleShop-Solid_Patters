using BLL.Dto;
using ClassLibrary1.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject1
{
    internal static class EqualityComparersHelper
    {
        public static bool CompareEqualityOrderDto(OrderDto expexted, OrderDto actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.Status, actual.Status);
            Assert.AreEqual(expexted.CreationTime, actual.CreationTime);
            Assert.AreEqual(expexted.CustomerId, actual.CustomerId);
            Assert.AreEqual(expexted.ProductsIds, actual.ProductsIds);

            return true;
        }

        public static bool CompareEqualityOrder(Order expexted, Order actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.CreationTime, actual.CreationTime);
            Assert.AreEqual(expexted.Customer.Id, actual.Customer.Id);
            Assert.AreEqual(expexted.Status, actual.Status);
            Assert.AreEqual(expexted.Products.Count, actual.Products.Count);

            return true;
        }

        public static bool CompareEqualityProductDto(ProductDto expexted, ProductDto actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.Name, actual.Name);
            Assert.AreEqual(expexted.Price, actual.Price);

            return true;
        }
        public static bool CompareEqualityProduct(Product expexted, Product actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.Name, actual.Name);
            Assert.AreEqual(expexted.Price, actual.Price);

            return true;
        }

        public static bool CompareEqualityRegisteredUserDto(RegisteredUserDto expexted, RegisteredUserDto actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.Name, actual.Name);
            Assert.AreEqual(expexted.Surname, actual.Surname);
            Assert.AreEqual(expexted.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expexted.Email, actual.Email);
            Assert.AreEqual(expexted.Pass, actual.Pass);
            Assert.AreEqual(expexted.OrderIds?.Count(), actual.OrderIds.Count());

            return true;
        }
        public static bool CompareEqualityRegisteredUser(RegisteredUser expexted, RegisteredUser actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.Name, actual.Name);
            Assert.AreEqual(expexted.Surname, actual.Surname);
            Assert.AreEqual(expexted.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expexted.Email, actual.Email);
            Assert.AreEqual(expexted.Pass, actual.Pass);
            Assert.AreEqual(expexted.Orders.Count, actual.Orders.Count);
            for (int i = 0; i < expexted.Orders.Count; i++)
            {
                Assert.AreEqual(expexted.Orders.ElementAt(i).Id, actual.Orders.ElementAt(i).Id);
            }

            return true;
        }
        public static bool CompareEqualityAdministratorDto(AdministratorDto expexted, AdministratorDto actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.Name, actual.Name);
            Assert.AreEqual(expexted.Surname, actual.Surname);
            Assert.AreEqual(expexted.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expexted.Email, actual.Email);
            Assert.AreEqual(expexted.Pass, actual.Pass);
            Assert.AreEqual(expexted.OrderIds?.Count(), actual.OrderIds.Count());

            return true;
        }

        public static bool CompareEqualityAdministrator(Administrator expexted, Administrator actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.Name, actual.Name);
            Assert.AreEqual(expexted.Surname, actual.Surname);
            Assert.AreEqual(expexted.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expexted.Email, actual.Email);
            Assert.AreEqual(expexted.Pass, actual.Pass);
            Assert.AreEqual(expexted.Orders.Count, actual.Orders.Count);

            return true;
        }
    }
}
