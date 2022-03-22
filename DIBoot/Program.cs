using AutoMapper;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using ClassLibrary1.Data;
using ConsoleApp1;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIBoot
{
    internal static class Program
    {
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            services.AddSingleton<ConsoleApp, ConsoleApp>()
                .BuildServiceProvider()
                .GetService<ConsoleApp>()
                .Start();
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductsRepo, ProductsRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IRegisteredUserRepo, RegisteredUserRepo>();
            services.AddScoped<IAdministratorRepo, AdministratorRepo>();

            services.AddScoped<IStoreDB, StoreDB>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());

            }
            ).CreateMapper());

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRegisteredUserService, RegisteredUserService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
        }

        }
    }
