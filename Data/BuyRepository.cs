using Microsoft.Extensions.Logging;
using BuyWebPage.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BuyWebPage.ViewModels;

namespace BuyWebPage.Data
{
    public class BuyRepository :IBuyRepository
    {
        public BuyDbContext _context;

        public ILogger<BuyRepository> Loger;

        public BuyRepository(BuyDbContext context, ILogger<BuyRepository> loger)
        {
            _context = context;
            this.Loger = loger;
        }
        public IEnumerable<Product> getProducts()
        {
            try
            {
                Loger.LogInformation("gettin all products");
                return _context.Products.OrderBy(p => p.Category).ToList();

            }catch(Exception Ex)
            {
                Loger.LogInformation("Cant get the products" + Ex.Message);
                return null;
            }

        }
        public Product ProductByName(string Title)
        {
            var result = _context.Products.Where(P => P.Title == Title).First();
            return result;
        }
       /* public IEnumerable<Order> GetOrders(bool includeitems)
        {
            
            try {
                Loger.LogInformation("gettin all Orders");
                if (includeitems) { 
                    return _context.Orders
                            .Include(p => p.Items)
                            .ThenInclude(i => i.Product)
                            .ToList();
                }
                else
                {
                    return _context
                          .Orders
                          .ToList();
                }

            }
            catch(Exception Ex)
            {
                Loger.LogError("Occur an problem gettin the orders" +Ex.Message);
                return null;
            }

        }*/
       public Order GetOrdersById(string username, bool includeitems, int id)
        {

            try
            {
                if (includeitems) { 
                    Loger.LogInformation("gettin all Orders");
                    return _context.Orders
                        .Include(O => O.Items)
                        .ThenInclude(i => i.Product)
                        .Where(O => O.Id == id && O.Usuario.UserName == username).
                         First();
                       
                }
                else
                {
                    return _context.Orders
                        .Where(O => O.Id == id && O.Usuario.UserName == username)
                        .First();
                }
            }
            catch (Exception Ex)
            {
                Loger.LogError("Occur an problem gettin the orders by id" + Ex.Message);
                return null;
            }



        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
           

        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        public void AddOrder(Order order)
        {
            foreach(var item in order.Items)
            {
                item.Product = _context.Products.Find(item.Product.ProductId);
            }
            AddEntity(order);

        }
        public Order GetOrderByOrderNumber(string OrderNumber)
        {
            return _context.Orders.Include(p => p.Items)
                    .Where(O => O.OrderNumber == OrderNumber).First();

        }
        public Order GetOrderByOrderDate(DateTime OrderDate)
        {
            return _context.Orders.Include(P => P.Items)
                    .Where(O => O.OrderDate == OrderDate).First();

        }

        public void AddEntity(Object model)
        {

            _context.Add(model);
        }
        public void AddOrderItem(string username,int id, OrderItem item)
        {
            var order = GetOrdersById(username, true, id);
            if(order != null)
            {
                order.Items.Add(item);
                Loger.LogInformation("going to save the item");
                //_context.Orders.Update(order);
            
            }

        }

        public IEnumerable<Order> GetOrdersByUsername(string username, bool includeItems)
        {
            try
            {
                Loger.LogInformation("gettin all Orders");
                if (includeItems)
                {
                    return _context.Orders
                            .Where(o => o.Usuario.UserName == username)
                            .Include(p => p.Items)
                            .ThenInclude(i => i.Product)
                            .OrderBy(O => O.OrderDate).ToList();
                }
                else
                {
                    return _context.Orders
                            .Where(o => o.Usuario.UserName == username)
                            .ToList();
                }

            }
            catch (Exception Ex)
            {
                Loger.LogError("Occur an problem gettin the orders" + Ex.Message);
                return null;
            }
        }

    }
}
