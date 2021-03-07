using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GreenHealthApi.Endpoints.Orders.DTO;

namespace GreenHealthApi.Endpoints.Orders
{
    public interface IOrdersReader
    {
        Task<IEnumerable<OrderFullModel>> GetСonfirmed();
        Task<IEnumerable<OrderFullModel>> GetUnconfirmed();
        Task<IEnumerable<OrderFullModel>> GetAllUsersOrders(string userIdentity);
        Task<IEnumerable<CategoryEntity>> GetAllCategories();
    }
    public class OrdersReader : IOrdersReader
    {
        private readonly IConnectionStringGetter _conStr;

        public OrdersReader(IConnectionStringGetter conStr)
        {
            _conStr = conStr;
        }
        
        public async Task<IEnumerable<OrderFullModel>> GetUnconfirmed()
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var getOrderQuery = @"USE GreenHealth;
                                          SELECT o.Id, o.CustomerId, c.fullName, c.phone, o.Address, o.IsConfirmed, o.Date, 
o.City FROM Orders o JOIN Customers c ON o.CustomerId = c.Id WHERE o.IsConfirmed = 0;";
                    var orders = await connection.QueryAsync<OrderFullModel>(getOrderQuery);
                    var ordersAsList = orders.ToList();
                    var i = 0;
                    foreach (var order in ordersAsList)
                    {
                        
                        var getOrderElementsQuery = @"USE GreenHealth; SELECT  oe.Id, 
m.Price * m.PriceMultiplier AS Price, m.Id AS MedicamentId, m.Name AS MedicamentName, oe.Count 
FROM OrderElements oe JOIN Medicaments m ON m.Id = oe.MedicamentId WHERE oe.OrderId = @orderId;";
                        ordersAsList[i].OrderElements =
                            await connection.QueryAsync<OrderElementFullModel>(getOrderElementsQuery, new {orderId = order.Id});
                        ++i;
                    }

                    return ordersAsList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public async Task<IEnumerable<OrderFullModel>> GetСonfirmed()
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var getOrderQuery = @"USE GreenHealth;
                                          SELECT o.Id, o.CustomerId, c.fullName, c.phone, o.Address, o.IsConfirmed, o.Date, 
o.City FROM Orders o JOIN Customers c ON o.CustomerId = c.Id WHERE o.IsConfirmed = 1;";
                    var orders = await connection.QueryAsync<OrderFullModel>(getOrderQuery);
                    var ordersAsList = orders.ToList();
                    var i = 0;
                    foreach (var order in ordersAsList)
                    {
                        
                        var getOrderElementsQuery = @"USE GreenHealth; SELECT  oe.Id, 
m.Price * m.PriceMultiplier AS Price, m.Id AS MedicamentId, m.Name AS MedicamentName, oe.Count 
FROM OrderElements oe JOIN Medicaments m ON m.Id = oe.MedicamentId WHERE oe.OrderId = @orderId;";
                        ordersAsList[i].OrderElements =
                            await connection.QueryAsync<OrderElementFullModel>(getOrderElementsQuery, new {orderId = order.Id});
                        ++i;
                    }

                    return ordersAsList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task<IEnumerable<OrderFullModel>> GetAllUsersOrders(string phone)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var getOrderQuery = @"USE GreenHealth;
                                          SELECT o.Id, o.CustomerId, c.Phone, c.fullName,  o.Address, o.IsConfirmed, o.Date, 
o.City FROM Orders o JOIN Customers c ON o.CustomerId = c.Id WHERE c.Phone = @phone;";
                    var orders = await connection.QueryAsync<OrderFullModel>(getOrderQuery, new{phone = phone});
                    var ordersAsList = orders.ToList();
                    var i = 0;
                    foreach (var order in ordersAsList)
                    {

                        var getOrderElementsQuery = @"USE GreenHealth; SELECT  oe.Id, 
m.Price * m.PriceMultiplier AS Price, m.Id AS MedicamentId, m.Name AS MedicamentName, oe.Count 
FROM OrderElements oe JOIN Medicaments m ON m.Id = oe.MedicamentId WHERE oe.OrderId = @orderId;";
                        ordersAsList[i].OrderElements =
                            await connection.QueryAsync<OrderElementFullModel>(getOrderElementsQuery,
                                new {orderId = order.Id});
                        ++i;
                    }

                    return ordersAsList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllCategories()
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var getCategoriesQuery = @"USE GreenHealth;
                                          SELECT Name, Id FROM Categories;";
                    var categories = await connection.QueryAsync<CategoryEntity>(getCategoriesQuery);
                   
                    return categories;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}