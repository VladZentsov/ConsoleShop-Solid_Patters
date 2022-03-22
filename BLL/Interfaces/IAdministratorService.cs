using BLL.Dto;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAdministratorService:ICrud<AdministratorDto>
    {
        /// <summary>Get Administrator model by the login information.</summary>
        /// <param name="loginInfo">The login information.</param>
        /// <returns>Administrator model</returns>
        public AdministratorDto GetbyLoginInfo(LoginInfo loginInfo);
    }
}
