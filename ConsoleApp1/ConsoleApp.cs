using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Builders;
using BLL.Dto;
using BLL.Interfaces;
using ClassLibrary1.Enums;
using ClassLibrary1.Validation;
using ConsoleApp1.Models;
using ConsoleApp1.Validation;
using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1
{
    public class ConsoleApp
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IRegisteredUserService _registeredUserService;
        private readonly IAdministratorService _administratorService;



        public delegate void Functions();

        private UserDto _user;
        private Roles _role;

        public ConsoleApp(IProductService productService, IOrderService orderService, IRegisteredUserService registeredUserService, IAdministratorService administratorService)
        {
            _productService = productService;
            _orderService = orderService;
            _registeredUserService = registeredUserService;
            _administratorService = administratorService;
        }
        public void Start()
        {
            Console.WriteLine("Wellcome");
            while (true)
            {
                switch (ChoseRoleToLogin())
                {
                    case -1:
                        continue;
                    case 1:
                        RegisteredUserCycle();
                        break;
                    case 2:
                        AdministratorCycle();
                        break;
                    case 3:
                        GuestCycle();
                        break;
                    case 4:
                        CreateAccount();
                        break;
                    case 5:
                        return;
                }
            }
        }
        public void FunctionalityCycle(List<(string, Functions)> functionality)
        {
            while (true)
            {
                Console.WriteLine("Press");
                DisplayFunctionality(functionality);
                int n = CheckIntInput(0, functionality.Count);
                if (n == functionality.Count)
                    break;
                functionality[n - 1].Item2.Invoke();
            }
        }
        public void RegisteredUserCycle()
        {
            LoginInfo refUserloginInfo = AskLoginInfo();
            _user = _registeredUserService.GetbyLoginInfo(refUserloginInfo);
            if(_user == null)
            {
                Console.WriteLine("Incorrect login or password");
                return;
            }
            _role = Roles.RegisteredUser;
            FunctionalityCycle(GetRegisteredUserDictionaryOfFunctionality());
        }

        public void AdministratorCycle()
        {
            LoginInfo refUserloginInfo = AskLoginInfo();
            _user = _administratorService.GetbyLoginInfo(refUserloginInfo);
            if (_user == null)
            {
                Console.WriteLine("Incorrect login or password");
                return;
            }
            _role = Roles.Administrator;

            FunctionalityCycle(GetAdministratorDictionaryOfFunctionality());
        }

        public void GuestCycle()
        {
            FunctionalityCycle(GetGuestDictionaryOfFunctionality());
        }
        public int ChoseRoleToLogin()
        {
            Console.WriteLine("Input: \n1. To login like registered user\n2. to login like admin\n3. to continue like a guest\n4.Create new account \n5. Exit");
            int n = CheckIntInput(1,5);
            return n;
        }
        public int CheckIntInput(int start, int end)
        {
            int n;
            try
            {
                n= int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Please, input number");
                return 0;
            }
            if (n > end || n < start)
            {
                Console.WriteLine("Please, input number in range from " + start + " to " + end);
                n= CheckIntInput(start, end);
            }
            return n;
        }
        public LoginInfo AskLoginInfo()
        {
            LoginInfo loginInfo = new LoginInfo();
            Console.WriteLine("Input your email");
            loginInfo.Email = Console.ReadLine();
            Console.WriteLine("Input your password");
            loginInfo.Password= Console.ReadLine();
            return loginInfo;
        }
        public int[] CreateArray(string str, int start, int end)
        {
            string[] arr = str.Split(' ');
            int[] intArr = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                intArr[i] = int.Parse(arr[i]);
                if (intArr[i] < start || intArr[i] > end)
                    throw new UIException("One of indexes out of range");
            }
            return intArr;
        }

        public List<(string, Functions)> GetRegisteredUserDictionaryOfFunctionality()
        {
            List<(string, Functions)> functionality = new List<(string, Functions)>()
            {
                ("View the list of goods",ViewListOfGoods),
                ("Search for goods by name",SearchGoodsByName),
                ("Creating a new order", CreatingNewOrder),
                ("Ordering", Ordering ),
                ("Cancellation order", Cancellation),
                ("Review the history of orders and the status of their delivery", ReviewHistoryAndStatus ),
                ("Setting the status of the order <<Received>>", SettingStatusReceived),
                ("Change of personal information", ChangePersonalInfo),
                ("Sign out of the account", SingOut)
            };

            return functionality;
        }
        public List<(string, Functions)> GetAdministratorDictionaryOfFunctionality()
        {
            List<(string, Functions)> functionality = new List<(string, Functions)>()
            {
                ("View the list of goods",ViewListOfGoods),
                ("Search for goods by name",SearchGoodsByName),
                ("Creating a new order", CreatingNewOrder),
                ("Creating a new order for user", AttachOrderToUser),
                ("Ordering", Ordering ),
                ("Change registered user order status", SetStatuse),
                ("Viewing and changing personal information of users", ChangeRegisteredUserInfo),
                ("Adding a new product (name, category, description, cost)", AddProduct),
                ("Change of information about the product", ChangeProductInfo ),
                ("Sign out of the account", SingOut)
            };

            return functionality;
        }

        public List<(string, Functions)> GetGuestDictionaryOfFunctionality()
        {
            List<(string, Functions)> functionality = new List<(string, Functions)>()
            {
                ("Search for goods by name",SearchGoodsByName),
                ("Create new account",CreateAccount),
            };
            return functionality;
        }
        public void DisplayFunctionality(List<(string, Functions)> functionality)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------");
            int i = 1;
            foreach (var item in functionality)
            {
                Console.WriteLine(i+" to "+item.Item1 );
                i++;
            }
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
        public void SearchGoodsByName()
        {
            Console.WriteLine("Input name of product");
            string name = Console.ReadLine();
            var result = _productService.FindByname(name);
            if (result == null||!result.Any())
            {
                Console.WriteLine("No products found");
                return;
            }
            int i = 1;
            foreach (var item in result)
            {
                Console.WriteLine(i+". "+item.Name+" Price: "+item.Price);
                i++;
            }
            Console.WriteLine("Press number of product to show description (0 - skip)");
            int inputPruductNumber = CheckIntInput(0, result.Count());

            if (inputPruductNumber == 0)
                return;

            Console.WriteLine(result.ElementAt(inputPruductNumber-1).Description);
        }
        public void ViewListOfGoods()
        {
            var result = _productService.GetAll();
            int i = 1;
            foreach (var item in result)
            {
                Console.WriteLine(i + ". id: "+item.Id +", "+ item.Name+ " Categoty: "+item.Category.ToString() + " Price: " + item.Price);
                i++;
            }

            Console.WriteLine("Press number of product to show description (0 - skip)");
            int inputPruductNumber = CheckIntInput(0, result.Count());

            if (inputPruductNumber == 0)
                return;

            Console.WriteLine(result.ElementAt(inputPruductNumber - 1).Description);
        }
        public void CreatingNewOrder()
        {
            Console.WriteLine("Enter product numbers separated by a space");
            string numberstr=Console.ReadLine();
            int[] numbers;
            try
            {
                numbers = CreateArray(numberstr, 1, _productService.GetAll().Count());
            }
            catch(UIException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            

            var orderDtoBuilder = new OrderDtoBuilder();

            OrderDto order = orderDtoBuilder
                .SetDateTime(DateTime.Now)
                .SetCustomerId(_user.Id)
                .SetProductsIds(numbers)
                .SetCustomerRole(_role)
                .SetStatus(Status.NotCompleted)
                .Build();
            _orderService.Add(order);
        }
        public void ShowOrders(IEnumerable<OrderDto> orders)
        {
            int i = 1;
            foreach (var order in orders)
            {
                Console.WriteLine(i + " Making order date: " + order.CreationTime + ". Price: " + _orderService.GetPrice(order.Id) + " Order status: " + order.Status.ToString() + ". List of Products:");
                int j = 1;
                foreach (var productId in order.ProductsIds)
                {
                    var product = _productService.GetById(productId);
                    Console.WriteLine("   " + j + ". " + product.Name + " " + product.Price);
                    j++;
                }
                i++;
            }
        }
        public void Ordering()
        {
            var orders = _orderService.GetOrdersByUserId(_user.GetRole, _user.Id);

            ShowOrders(orders);
           
            Console.WriteLine("\nEnter number of order to ordering (0 - cancel ordering)");
            int orderNumber = CheckIntInput(0, orders.Count());

            if (orderNumber == 0)
                return;
            try
            {
                _orderService.SetStatus(orders.ElementAt(orderNumber - 1).Id, Status.New);
            }
            catch (ShopException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void Cancellation()
        {
            var orders = _orderService.GetOrdersByUserId(_user.GetRole, _user.Id);

            ShowOrders(orders);

            Console.WriteLine("\nEnter number of order to cancellation (0 - cancel cancellation)");
            int orderNumber = CheckIntInput(1, orders.Count());

            if (orderNumber == 0)
                return;

            try
            {
                _orderService.SetStatus(orders.ElementAt(orderNumber - 1).Id, Status.CanceledByUser);
            }
            catch (ShopException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ReviewHistoryAndStatus()
        {
            var orders = _orderService.GetOrdersByUserId(_user.GetRole, _user.Id);

            ShowOrders(orders);
        }
        public void SettingStatusReceived()
        {
            var orders = _orderService.GetOrdersByUserId(_user.GetRole, _user.Id);

            ShowOrders(orders);

            Console.WriteLine("\nEnter number of order to ordering (0 - cancel ordering)");
            int orderNumber = CheckIntInput(1, orders.Count());

            if (orderNumber == 0)
                return;

            try
            {
                _orderService.SetStatus(orders.ElementAt(orderNumber - 1).Id, Status.Recieved);
            }
            catch (ShopException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ChangePersonalInfo()
        {
            ChangePersonalInfoHelper(_user, _role);
        }
        public void ChangePersonalInfoHelper(UserDto user, Roles role)
        {
            Console.WriteLine("What do you want to change?\n1. Name\n2. Surname\n3.Email\n4. Password\n 0 to cancel");
            int orderNumber = CheckIntInput(0, 4);

            bool flag = true;

            switch (orderNumber)
            {
                case 0:
                    flag = false;
                    break;
                case 1:
                    Console.WriteLine("Input new name");
                    string name = Console.ReadLine();
                    user.Name = name;
                    break;
                case 2:
                    Console.WriteLine("Input new surname");
                    string surname = Console.ReadLine();
                    user.Surname = surname;
                    break;
                case 3:
                    Console.WriteLine("Input new password");
                    string password = Console.ReadLine();
                    user.Pass = password;
                    break;
            }
            if (!UserValidator(user)||flag)
                return;

            if (role == Roles.RegisteredUser)
                _registeredUserService.Update((RegisteredUserDto)user);
            else
                _administratorService.Update((AdministratorDto)user);

        }
        public void SingOut()
        {
            Console.WriteLine("Sing out successfully");
        }
        public void AttachOrderToUser()
        {
            Console.WriteLine("Enter product numbers separated by a space");
            string numberstr = Console.ReadLine();
            int[] numbers;
            try
            {
                numbers = CreateArray(numberstr, 1, _productService.GetAll().Count());
            }
            catch (UIException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("Input id of User");
            int UserId;
            try
            {
                UserId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Please, input correct data");
                return;
            }

            var orderDtoBuilder = new OrderDtoBuilder();

            OrderDto order = orderDtoBuilder
                .SetDateTime(DateTime.Now)
                .SetCustomerId(UserId)
                .SetProductsIds(numbers)
                .SetCustomerRole(Roles.RegisteredUser)
                .SetStatus(Status.NotCompleted)
                .Build();
            _orderService.Add(order);

            try
            {
                _registeredUserService.AttachOrderToUser(UserId, order.Id);
            }
            catch(ShopException ex)
            {
                Console.WriteLine(ex.Message);
                _orderService.DeleteById(order.Id);
            }
        }
        private void SetStatuse()
        {
            var orders = _orderService.GetAll();
            ShowOrders(orders);

            Console.WriteLine("Input number of order to change status");

            int orderNumber = CheckIntInput(0,orders.Count());

            OrderDto order = orders.ElementAt(orderNumber - 1);

            int i = 1;
            foreach (var item in Enum.GetValues(typeof(Status)).Cast<Status>())
            {
                Console.WriteLine(i + ". " + item.ToString());
                i++;
            }

            Console.WriteLine("Enter Status number (0 - exit)");
            int StatusNumber = CheckIntInput(0,i);

            if (StatusNumber == 0)
                return;

            Status newStatus = (Status)(StatusNumber-1);

            try
            {
                _orderService.SetStatus(order.Id, newStatus);
            }
            catch (ShopException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            

            Console.WriteLine("Are you shure you whant to chage status from " + order.Status.ToString() + " to " + newStatus.ToString()+"? 0 - No, 1 - Yes");

            int answer = CheckIntInput(0, 1);
            if (answer == 0)
                return;
            order.Status = newStatus;

            _orderService.Update(order);

        }
        private void ChangeRegisteredUserInfo()
        {
            var users = _registeredUserService.GetAll();

            foreach(var user in users)
            {
                Console.WriteLine("User id: " + user.Id + " " + user.Name + " " + user.Surname + " " + user.Email + ". Orders:");
                ShowOrders(_orderService.GetOrdersByIds(user.OrderIds));
            }
            Console.WriteLine("Input user id to change users personal info or 0 to exit");
            int n = CheckIntInput(0, users.Max(u => u.Id));

            UserDto currentUser;
            try { currentUser = _registeredUserService.GetById(n); }
            catch(ShopException ex) 
            { 
                Console.WriteLine(ex.Message);
                return; 
            }

            ChangePersonalInfoHelper(currentUser, currentUser.GetRole);
        }

        private void AddProduct()
        {
            Console.WriteLine("Input name of product");
            string name = Console.ReadLine();

            Console.WriteLine("Input category number");
            int i = 1;
            foreach (var item in Enum.GetValues(typeof(Categories)).Cast<Categories>())
            {
                Console.WriteLine(i+". "+item.ToString());
                i++;
            }

            int categoryNumber = CheckIntInput(1, i);
            Categories category = (Categories)(categoryNumber-1);

            Console.WriteLine("Input description of product");
            string description = Console.ReadLine();

            Console.WriteLine("Input cost of product");
            decimal cost;
            try
            {
                cost = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Incorrect date input");
                return;
            }

            ProductDto product = new ProductDto() { Category = category, Description = description, Name = name, Price = cost};
            _productService.Add(product);
        }

        public void ChangeProductInfo()
        {
            ViewListOfGoods();

            var products = _productService.GetAll();

            Console.WriteLine("Input product number");
            int n = CheckIntInput(1, products.Count());

            var product = products.ElementAt(n-1);

            Console.WriteLine("Press \n1. to input new name\n2. to input new category\n3. to input new description\n4. to input new cost\n0. to exit");
            int k = CheckIntInput(0, 4);

            switch (k)
            {
                case 1:
                    Console.WriteLine("input new name");
                    product.Name = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("input number of new category");
                    int i = 1;
                    foreach (var item in Enum.GetValues(typeof(Categories)).Cast<Categories>())
                    {
                        Console.WriteLine(i + ". " + item.ToString());
                        i++;
                    }

                    int categoryNumber = CheckIntInput(1, i);
                    Categories category = (Categories)(categoryNumber-1);
                    product.Category = category;
                    break;
                case 3:
                    Console.WriteLine("input new description");
                    product.Description = Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("input new cost");
                    product.Price =Convert.ToDecimal(Console.ReadLine());
                    break;
            }
            _productService.Update(product);
        }

        public void CreateAccount()
        {
            Console.WriteLine("Input your email");
            string email = Console.ReadLine();

            Console.WriteLine("Input your password");
            string password = Console.ReadLine();

            Console.WriteLine("Input your name");
            string name = Console.ReadLine();

            Console.WriteLine("Input your surname");
            string surname = Console.ReadLine();

            var user = new RegisteredUserDto() { Email = email, Pass = password, Name = name, Surname = surname };

            if (UserValidator(user))
                _registeredUserService.Add(user);
        }

        public bool UserValidator(UserDto user)
        {
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                Console.WriteLine("Failed to create an account");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return false;
            }
            else
            {
                Console.WriteLine("Account created successfully");
                return true;
            }
        }
    }
}
