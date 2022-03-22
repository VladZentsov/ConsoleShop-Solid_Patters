using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using BLL.Validation;
using ClassLibrary1.Entities;
using ConsoleApp1.Models;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdministratorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Adds the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Add(AdministratorDto model)
        {
            RepoValidationHelper.CheckCreationModelId(model, _unitOfWork.AdministratorRepo.FindAll());

            _unitOfWork.AdministratorRepo.Add(AdministratorMapperHelper(model));
        }

        /// <summary>Get by the login information.</summary>
        /// <param name="loginInfo">The login information.</param>
        /// <returns>Administrator model</returns>
        public AdministratorDto GetbyLoginInfo(LoginInfo loginInfo)
        {
            return _mapper.Map<Administrator, AdministratorDto>(_unitOfWork.AdministratorRepo
                .FindAll()
                .FirstOrDefault(a => a.Email == loginInfo.Email && a.Pass == loginInfo.Password));
        }

        /// <summary>Deletes the by identifier.</summary>
        /// <param name="modelId">The model identifier.</param>
        public void DeleteById(int modelId)
        {
            _unitOfWork.AdministratorRepo.DeleteById(modelId);
        }


        /// <summary>Gets all.</summary>
        /// <returns>All Administrator models</returns>
        public IEnumerable<AdministratorDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Administrator>, IEnumerable<AdministratorDto>>(_unitOfWork.AdministratorRepo.FindAll());
        }
        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <para>Administrator model</para>
        /// </returns>
        public AdministratorDto GetById(int id)
        {
            return _mapper.Map<Administrator, AdministratorDto>(_unitOfWork.AdministratorRepo.GetById(id));
        }
        /// <summary>Updates the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Update(AdministratorDto model)
        {
            _unitOfWork.AdministratorRepo.Update(AdministratorMapperHelper(model));
        }
        private Administrator AdministratorMapperHelper(AdministratorDto model)
        {
            var orders = _unitOfWork.OrderRepo.FindAll().Where(o => model.OrderIds.Contains(o.Id));

            return _mapper.Map<(AdministratorDto, IEnumerable<Order>), Administrator>((model, orders));
        }
    }
}

