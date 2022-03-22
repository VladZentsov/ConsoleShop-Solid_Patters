using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IProductService:ICrud<ProductDto>
    {
        /// <summary>Finds products by name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>Products</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public IEnumerable<ProductDto> FindByname(string name);
    }
}
