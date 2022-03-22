using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using BLL.Validation;
using ClassLibrary1.Entities;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClassLibrary1.Enums;
using ClassLibrary1.Validation;
using DAL.Enums;

namespace BLL.Services
{
    public class OrderService: IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>Adds the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Add(OrderDto model)
        {
            RepoValidationHelper.CheckCreationModelId(model, _unitOfWork.OrderRepo.FindAll());

            _unitOfWork.OrderRepo.Add(OrderMapperHelper(model));
        }


        /// <summary>Deletes the by identifier.</summary>
        /// <param name="modelId">The model identifier.</param>
        public void DeleteById(int modelId)
        {
            _unitOfWork.OrderRepo.DeleteById(modelId);
        }


        /// <summary>Gets all.</summary>
        /// <returns>
        ///   <para>All Orders</para>
        /// </returns>
        public IEnumerable<OrderDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(_unitOfWork.OrderRepo.FindAll());
        }


        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order</returns>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No order with such id " + id</exception>
        public OrderDto GetById(int id)
        {
            if (!_unitOfWork.OrderRepo.CheckExistence(id))
                throw new ShopException("No order with such id " + id);
            return _mapper.Map<Order, OrderDto>(_unitOfWork.OrderRepo.GetById(id));
        }


        /// <summary>Gets the orders by user identifier.</summary>
        /// <param name="role">The role.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>All user Orders</returns>
        public IEnumerable<OrderDto> GetOrdersByUserId(Roles role, int customerId)
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(_unitOfWork.OrderRepo
                .FindAll()
                .Where(o => o.Customer.GetRole == role && o.Customer.Id== customerId));
        }

        /// <summary>Gets the price.</summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>Order price</returns>
        public decimal GetPrice(int orderId)
        {
            var order = _unitOfWork.OrderRepo.FindAll().FirstOrDefault(o => o.Id == orderId);
            decimal sum = 0;
            foreach (var product in order.Products)
            {
                sum+=product.Price;
            }
            return sum;
        }
        /// <summary>Orderings the specified order identifier.</summary>
        /// <param name="orderId">The order identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">Order already " + order.Status</exception>
        public void Ordering(int orderId)
        {
            var order = _unitOfWork.OrderRepo.GetById(orderId);
            if (order.Status != 0)
                throw new ShopException("Order already " + order.Status);

            order.Status = Status.New;
            Update(order);
        }

        /// <summary>Upcasts the order status.</summary>
        /// <param name="orderId">The order identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">Cannot upcast order status. Order status is "+order.Status.ToString()</exception>
        public void UpcastOrderStatus(int orderId)
        {
            var order = GetById(orderId);
            if((int)order.Status>3)
                throw new ShopException("Cannot upcast order status. Order status is "+order.Status.ToString());
            order.Status = (Status)((int)order.Status + 1);
            Update(order);
        }

        /// <summary>Sets the Order status.</summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="status">The status.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException"></exception>
        public void SetStatus(int orderId, Status status)
        {
            Order CurrentOrder = _unitOfWork.OrderRepo.GetById(orderId);
            Status CurrentOrderStatus = CurrentOrder.Status;

            if(status == Status.CanceledByAdmin)
            {
                CurrentOrder.Status = status;
                _unitOfWork.OrderRepo.Update(CurrentOrder);
                return;
            }
                

            if (CurrentOrderStatus == Status.CanceledByAdmin || CurrentOrderStatus == Status.Completed || CurrentOrderStatus == Status.CanceledByUser)
            {
                throw new ShopException("Order is already " + status.ToString());
            }

            if (status == Status.NotCompleted)
            {
                if (CurrentOrderStatus != Status.NotCompleted)
                    throw new ShopException("Order is already" + CurrentOrderStatus.ToString()+ " and cannot be lowcast to NotCompleted");
                CurrentOrder.Status = status;
                _unitOfWork.OrderRepo.Update(CurrentOrder);
                return;
            }

            if(status == Status.CanceledByUser)
            {
                if(CurrentOrderStatus >= Status.Recieved)
                    throw new ShopException("Order is already" + CurrentOrderStatus.ToString() + " and cannot be Canceled");
                CurrentOrder.Status = status;
                _unitOfWork.OrderRepo.Update(CurrentOrder);
                return;
            }

            if ((int)status - 1 < (int)CurrentOrderStatus)
                throw new ShopException("Order is " + CurrentOrderStatus.ToString() + " and cannot be lowcasted");

            CurrentOrder.Status = status;
            _unitOfWork.OrderRepo.Update(CurrentOrder);
        }

        /// <summary>Updates the specified model.</summary>
        /// <param name="model">The model.</param>
        public void Update(OrderDto model)
        {
            _unitOfWork.OrderRepo.Update(OrderMapperHelper(model));
        }

        private void Update(Order model)
        {
            _unitOfWork.OrderRepo.Update(model);
        }

        private Order OrderMapperHelper(OrderDto model)
        {
            User Customer = new RegisteredUser();
            if (model.CustomerRole == Roles.RegisteredUser)
            {
                Customer = _unitOfWork.RegisteredUserRepo.FindAll().FirstOrDefault(c => c.Id == model.CustomerId);
            }
            else if (model.CustomerRole == Roles.Administrator)
            {
                Customer = _unitOfWork.AdministratorRepo.FindAll().FirstOrDefault(c => c.Id == model.CustomerId);
            }

            var products = _unitOfWork.ProductsRepo.FindAll().Where(p => model.ProductsIds.Contains(p.Id)).ToList();

            return _mapper.Map<(OrderDto, User, List<Product>), Order>((model, Customer, products));
        }

        /// <summary>Gets the orders by ids.</summary>
        /// <param name="ids">The ids.</param>
        /// <returns>orders with such ids</returns>
        public IEnumerable<OrderDto> GetOrdersByIds(IEnumerable<int> ids)
        {
            var orders = new List<OrderDto>();
            foreach (var id in ids)
            {
                orders.Add(GetById(id));
            }
            return orders;
        }
    }
}
