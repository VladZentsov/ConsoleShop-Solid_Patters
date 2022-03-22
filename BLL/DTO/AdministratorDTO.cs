using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class AdministratorDto:UserDto
    {
        public override Roles GetRole
        {
            get
            {
                return Roles.Administrator;
            }
        }
    }
}
