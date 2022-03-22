using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using BLL.Validation;
using ClassLibrary1.Entities;
using DAL.Interfaces;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Adds the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Add(ProductDto model)
        {
            RepoValidationHelper.CheckCreationModelId(model, _unitOfWork.ProductsRepo.FindAll());

            _unitOfWork.ProductsRepo.Add(_mapper.Map<ProductDto,Product>(model));
        }

        /// <summary>Deletes the by identifier.</summary>
        /// <param name="modelId">The model identifier.</param>
        public void DeleteById(int modelId)
        {
            _unitOfWork.ProductsRepo.DeleteById(modelId);
        }

        /// <summary>Finds products by name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>Products</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public IEnumerable<ProductDto> FindByname(string name)
        {
            if(name == null)
                throw new ArgumentNullException(name);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_unitOfWork.ProductsRepo.FindAll().Where(p => p.Name.ToLower().Contains(name.ToLower())));
        }

        /// <summary>Gets all.</summary>
        /// <returns>Products</returns>
        public IEnumerable<ProductDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_unitOfWork.ProductsRepo.FindAll());
        }

        /// <summary>Gets by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product</returns>
        public ProductDto GetById(int id)
        {
            return _mapper.Map<Product, ProductDto>(_unitOfWork.ProductsRepo.GetById(id));
        }

        /// <summary>Updates the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Update(ProductDto model)
        {
            _unitOfWork.ProductsRepo.Update(_mapper.Map<ProductDto, Product>(model));
        }
    }
}
