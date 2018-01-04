using System;
using BuyWebPage.Data.Entities;
using System.Collections;
using System.Collections.Generic;
using BuyWebPage.ViewModels;

namespace BuyWebPage.Data
{
    public interface IBuyRepository
    {

        IEnumerable<Product> getProducts();
        Product ProductByName(string Title);
        void AddProduct(Product product);
        //IEnumerable<Order> GetOrders(bool includeItems);
        Order GetOrderByOrderNumber(string OrderNumber);
        Order GetOrderByOrderDate(DateTime OrderDate);
        Order GetOrdersById(string username, bool includeItems, int id);
        void AddOrder(Order order);
        bool SaveChanges();
        void AddEntity(Object model);
        void AddOrderItem(string username,int id, OrderItem item);
        IEnumerable<Order> GetOrdersByUsername(string username, bool includeItems);
      
    }
}