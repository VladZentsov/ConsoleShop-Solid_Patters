using BLL.Dto;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IRegisteredUserService:ICrud<RegisteredUserDto>
    {
        /// <summary>Getbies the login information.</summary>
        /// <param name="loginInfo">The login information.</param>
        /// <returns>RegisteredUser witch such id</returns>
        public RegisteredUserDto GetbyLoginInfo(LoginInfo loginInfo);
        /// <summary>Attaches the order to user.</summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="OrderId">The order identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No registeres user with such id</exception>
        public void AttachOrderToUser(int UserId, int OrderId);
    }
}
