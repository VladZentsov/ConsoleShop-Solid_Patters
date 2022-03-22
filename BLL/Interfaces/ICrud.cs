using System;
using System.Collections.Generic;
using ClassLibrary1.Validation;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {
        /// <summary>Gets all.</summary>
        /// <returns>All models</returns>
        IEnumerable<TModel> GetAll();
        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <para>Model</para>
        /// </returns>
        /// <exception cref="ShopException">Entity id not unique</exception>
        TModel GetById(int id);
        /// <summary>Adds the specified entity.</summary>
        /// <param name="model">The entity.</param>
        /// <exception cref="ShopException">Entity id not unique</exception>
        /// <exception cref="ShopException">Surname can't be null</exception>
        /// <exception cref="ShopException">Name can't be null</exception>
        /// <exception cref="ShopException">Email can't be null</exception>
        /// <exception cref="ShopException">Password can't be null</exception>
        /// <exception cref="ArgumentNullException">Model</exception>
        void Add(TModel model);
        /// <summary>Updates the specified model.</summary>
        /// <param name="model">The model.</param>
        /// <exception cref="ShopException">Entity id not unique</exception>
        /// <exception cref="ShopException">Surname can't be null</exception>
        /// <exception cref="ShopException">Name can't be null</exception>
        /// <exception cref="ShopException">Email can't be null</exception>
        /// <exception cref="ShopException">Password can't be null</exception>
        /// <exception cref="ArgumentNullException">Model</exception>
        void Update(TModel model);
        /// <summary>Deletes the model by identifier.</summary>
        /// <param name="modelId">The model identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No product with such id</exception>
        void DeleteById(int modelId);
    }
}
