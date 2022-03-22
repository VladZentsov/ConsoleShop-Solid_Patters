using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class RegisteredUserDto:UserDto
    {
        public override Roles GetRole
        {
            get
            {
                return Roles.RegisteredUser;
            }
        }
    }
}
