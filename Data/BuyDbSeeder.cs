using BuyWebPage.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BuyWebPage.Data
{
    public class BuyDbSeeder
    {
        private BuyDbContext Context;
        private IHostingEnvironment Env2;
        private readonly UserManager<Users> manager;

        private ILogger<BuyDbSeeder> _Loger;

        //public ILogger<BuyDbSeeder> loger;

        public BuyDbSeeder(BuyDbContext _context, IHostingEnvironment env2, UserManager<Users> Manager, ILogger<BuyDbSeeder> Loger)
        {
            Context = _context;
            Env2 = env2;
            manager = Manager;
            _Loger = Loger;
        }


        public async Task Seed()
        {

            var user = await manager.FindByEmailAsync("dionisjosue@gmail.com");

            if (user == null)
            {
                user = new Users()
                {
                    FirstName = "Dionis",
                    LastName = "Vargas",
                    Email = "dionisjosue@gmail.com",
                    UserName = "dionisjosue12",
                    Fecha_Nacimiento = new DateTime(1998, 02, 12),
                    Genero = "masculino"
                };
                var registerUser = await manager.CreateAsync(user, "P@ssw0rd!");

                if (registerUser != IdentityResult.Success)
                {
                    throw new InvalidOperationException("failed to create the user");
                }
            }

            _Loger.LogInformation($"PROBANDO   {Env2.ContentRootPath}");
            if (!Context.Products.Any())
            {
                var path = Path.Combine
                    (Env2.ContentRootPath, "Data/Products.json");
                var Json = File.ReadAllText(path);
                var products = JsonConvert.DeserializeObject
                    <IEnumerable<Product>>(Json);

                Context.Products.AddRange(products);
                Order order = new Order()
                {
                    //Id = 1,
                    OrderDate = DateTime.UtcNow,
                    OrderNumber = "4",
                    Usuario = user,
                    Items = new List<OrderItem>()
                        {
                            new OrderItem()
                            {
                                Product = products.ElementAt(5),
                                Quantity = 3,
                                UnitPrice = products.ElementAt(5).Price,
                                

                            }

                        }
                };
                Context.Orders.Add(order);
            }
            
            Context.SaveChanges();
        }

    }
}

