using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UoW
{
    public interface IUnitOfWork
    {
        IOrderRepo OrderRepo { get; }
        IProductsRepo ProductsRepo { get; }
        IRegisteredUserRepo RegisteredUserRepo { get; }
        IAdministratorRepo AdministratorRepo { get; }
    }
}
