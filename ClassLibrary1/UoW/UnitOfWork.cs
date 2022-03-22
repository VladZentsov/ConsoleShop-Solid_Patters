using ClassLibrary1.Data;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IStoreDB _storeDB;
        private IProductsRepo _productsRepo;
        private IOrderRepo _orderRepo;
        private IRegisteredUserRepo _registeredUserRepo;
        private IAdministratorRepo _administratorRepo;

        public UnitOfWork(IStoreDB storeDB)
        {
            _storeDB = storeDB;
        }

        public IOrderRepo OrderRepo
        {
            get
            {
                if (_orderRepo == null)
                    _orderRepo = new OrderRepo(_storeDB);
                return _orderRepo;
            }
        }

        public IProductsRepo ProductsRepo
        {
            get
            {
                if( _productsRepo == null)
                    _productsRepo = new ProductsRepo(_storeDB);
                return _productsRepo;
            }
        }


        public IRegisteredUserRepo RegisteredUserRepo
        {
            get
            {
                if(_registeredUserRepo == null)
                    _registeredUserRepo= new RegisteredUserRepo(_storeDB);
                return _registeredUserRepo;
            }
        }

        public IAdministratorRepo AdministratorRepo
        {
            get
            {
                if (_administratorRepo == null)
                    _administratorRepo = new AdministratorRepo(_storeDB);
                return _administratorRepo;
            }
        }
    }
}
