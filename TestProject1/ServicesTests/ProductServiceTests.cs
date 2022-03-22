using BLL.Interfaces;
using BLL.Services;
using ClassLibrary1.Entities;
using DAL.UoW;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject1.ServicesTests
{
    public class ProductServiceTests
    {
        [Test]
        public void ProductService_GetById_ReturnsProductDto()
        {
            var expected = UnitTestHelper.GetProductsDto().ElementAt(0);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ProductsRepo.GetById(It.IsAny<int>()))
                .Returns(UnitTestHelper.GetUnitTestDb().Products[0]);
            IProductService productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = productService.GetById(1);

            EqualityComparersHelper.CompareEqualityProductDto(expected, actual);
        }

        [Test]
        public void ProductService_GetAll_ReturnsAllProductDto()
        {
            var expected = UnitTestHelper.GetProductsDto();

            var mockUnitOfWork = MoqGenerator.GetMoq();

            IProductService productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = productService.GetAll();

            for (int i = 0; i < expected.Count(); i++)
            {
                var expectedObj = expected.ElementAt(i);
                var actualObj = actual.ElementAt(i);
                EqualityComparersHelper.CompareEqualityProductDto(expectedObj, actualObj);
            }
        }

        [Test]
        public void ProductService_Add_AddsProductDto()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.ProductsRepo.Add(It.IsAny<Product>()));

            IProductService productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var product = UnitTestHelper.GetProductDto();
            product.Id = 5;

            productService.Add(product);
            var expected = UnitTestHelper.GetProduct();
            expected.Id = 5;

            mockUnitOfWork.Verify(x => x.ProductsRepo.Add(It.Is<Product>(o =>
            EqualityComparersHelper.CompareEqualityProduct(expected, o))), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        public void ProductService_Delete_DeletesProduct(int productId)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.ProductsRepo.DeleteById(It.IsAny<int>()));
            IProductService productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            productService.DeleteById(productId);

            mockUnitOfWork.Verify(x => x.ProductsRepo.DeleteById(productId), Times.Once);
        }

        [Test]
        public void ProductService_Update_UpdateProduct()
        {
            var product = UnitTestHelper.GetProductDto();
            product.Id = 1;

            var mockUnitOfWork = MoqGenerator.GetMoq();
            mockUnitOfWork.Setup(x => x.ProductsRepo.Update(It.IsAny<Product>()));

            IProductService productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            productService.Update(product);

            var expected = UnitTestHelper.GetProduct();

            mockUnitOfWork.Verify(x => x.ProductsRepo.Update(It.Is<Product>(o =>
            EqualityComparersHelper.CompareEqualityProduct(expected, o))), Times.Once);
        }

        [Test]
        public void ProductService_FindByname()
        {
            var mockUnitOfWork = MoqGenerator.GetMoq();
            IProductService productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var expected = UnitTestHelper.GetProductsDto().ElementAt(0);

            var actual = productService.FindByname("Play").FirstOrDefault();

            EqualityComparersHelper.CompareEqualityProductDto(expected, actual);
        }
    }
}
