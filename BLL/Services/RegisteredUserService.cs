using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using BLL.Validation;
using ClassLibrary1.Entities;
using ClassLibrary1.Validation;
using ConsoleApp1.Models;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class RegisteredUserService : IRegisteredUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisteredUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Adds the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Add(RegisteredUserDto model)
        {
            RepoValidationHelper.CheckCreationModelId(model, _unitOfWork.RegisteredUserRepo.FindAll());

            if(model.CreatedDate == new DateTime())
                model.CreatedDate = DateTime.Now;

            _unitOfWork.RegisteredUserRepo.Add(RegisteredUserMapperHelper(model));
        }

        /// <summary>Attaches the order to user.</summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="OrderId">The order identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No registeres user with such id</exception>
        public void AttachOrderToUser(int UserId, int OrderId)
        {
            var user = GetById(UserId);
            if (!_unitOfWork.OrderRepo.CheckExistence(OrderId))
                throw new ShopException("No registeres user with such id");

            List<int> ordersIds = new List<int>(user.OrderIds){ OrderId };

            user.OrderIds= ordersIds;

            Update(user);

        }

        /// <summary>Deletes the by identifier.</summary>
        /// <param name="modelId">The model identifier.</param>
        public void DeleteById(int modelId)
        {
            _unitOfWork.RegisteredUserRepo.DeleteById(modelId);
        }

        /// <summary>Gets all.</summary>
        /// <returns>All RegisteredUsers</returns>
        public IEnumerable<RegisteredUserDto> GetAll()
        {
            return _mapper.Map<IEnumerable<RegisteredUser>, IEnumerable<RegisteredUserDto>>(_unitOfWork.RegisteredUserRepo.FindAll());
        }

        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RegisteredUser</returns>
        public RegisteredUserDto GetById(int id)
        {
            return _mapper.Map<RegisteredUser, RegisteredUserDto>(_unitOfWork.RegisteredUserRepo.GetById(id));
        }

        /// <summary>Getbies the login information.</summary>
        /// <param name="loginInfo">The login information.</param>
        /// <returns>RegisteredUser witch such id</returns>
        public RegisteredUserDto GetbyLoginInfo(LoginInfo loginInfo)
        {
            return _mapper.Map<RegisteredUser, RegisteredUserDto>(_unitOfWork.RegisteredUserRepo
                .FindAll()
                .FirstOrDefault(r=>r.Email==loginInfo.Email&&r.Pass==loginInfo.Password));
        }

        /// <summary>Updates the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Update(RegisteredUserDto model)
        {
            _unitOfWork.RegisteredUserRepo.Update(RegisteredUserMapperHelper(model));
        }

        private RegisteredUser RegisteredUserMapperHelper(RegisteredUserDto model)
        {
            var orders = _unitOfWork.OrderRepo.FindAll().Where(o=>model.OrderIds.Contains(o.Id));

            return _mapper.Map<(RegisteredUserDto, IEnumerable<Order>), RegisteredUser>((model, orders));
        }
    }
}
