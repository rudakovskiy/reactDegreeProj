using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http.Validation;
using Dapper;
using GreenHealthApi.Endpoints.Customers.DTO;
using GreenHealthApi.Endpoints.Orders.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GreenHealthApi.Endpoints.Orders
{
    public interface IOrdersWriter
    {
        Task ChangeOrderElCount(int orderId, int orderElId, int count);
        Task AddOrderAsync(OrderModel model);
        Task ConfirmOrderById(int id);
        Task DeleteOrderById(int id);
        Task DelCategory(int id);
    }
    public class OrdersWriter : IOrdersWriter
    {
        private readonly IConnectionStringGetter _conStr;

        public OrdersWriter(IConnectionStringGetter conStr)
        {
            _conStr = conStr;
        }

        public async Task ChangeOrderElCount(int orderId, int orderElId, int count)
        {
            await using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    await connection.OpenAsync();
                    var getUserQuery =
                        @"USE GreenHealth;
                      UPDATE OrderElements SET Count = @count WHERE Id=@id";
                    var user = await connection.ExecuteAsync(getUserQuery,
                        new {count = count, id = orderElId});
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
        }

        public async Task AddOrderAsync(OrderModel model)
        {
            model.Date = DateTime.Now;
            await using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    await connection.OpenAsync();
                    var getUserQuery =
                        @"USE GreenHealth;
                      SELECT Id, FullName, Email, Phone, Login, City, Address
                      FROM Customers 
                      WHERE Phone = @phone;";
                    var user = await connection.QueryFirstOrDefaultAsync<CustomerModel>(getUserQuery,
                        new {phone = model.Phone});
                    int userId;
                    if (user == null)
                    {
                        var addCustomerQuery = @"USE GreenHealth
                                              INSERT INTO Customers (FullName, Phone, City, Address) 
                                              VALUES (@fullName, @phone, @city, @address);
                                              SELECT SCOPE_IDENTITY() FROM Customerы;";

                        userId = await connection.QueryFirstOrDefaultAsync<int>(addCustomerQuery,
                            new
                                {
                                    fullName = model.FullName,
                                    phone = model.Phone,
                                    city = model.City,
                                    address = model.Address
                                });
                    }
                    else
                    {
                        userId = user.Id;
                    }
                    //order
                    var addOrderQuery = @"USE GreenHealth;
                                          INSERT INTO Orders (CustomerId, Address, IsConfirmed, Date, City) 
                                          VALUES (@customerId, @address, @isConfirmed, @date, @city);
                                          SELECT SCOPE_IDENTITY() FROM Orders;";
                    var OrderId = await connection.QueryFirstOrDefaultAsync<int>(addOrderQuery, new
                    {
                        customerId = userId, 
                        address = model.Address, 
                        isConfirmed = model.IsConfirmed, 
                        date = model.Date,
                        city = model.City
                    });
                    foreach (var product in model.OrderElements)
                    {
                        var addOrderElementQuery = 
                            @"USE GreenHealth;
                              INSERT INTO OrderElements (OrderId, MedicamentId, Count) 
                              VALUES (@orderId, @medicamentId, @count)";
                        await connection.QueryFirstOrDefaultAsync(addOrderElementQuery,
                            new {orderId = OrderId, medicamentId = product.Id, count = product.Count});
                    }
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
        }

        public async Task ConfirmOrderById(int id)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var confirmOrderQuery = @"USE GreenHealth; UPDATE Orders SET IsConfirmed = 1 WHERE Id = @id";
                    await connection.QueryFirstOrDefaultAsync(confirmOrderQuery, new {id = id});
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public async Task DeleteOrderById(int id)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var delOrderQuery = @"Use GreenHealth; DELETE FROM OrderElements WHERE OrderId = @id; DELETE FROM Orders WHERE Id = @id";
                    await connection.QueryFirstOrDefaultAsync(delOrderQuery, new {id = id});
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task DelCategory(int id)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var delCategoryQuery = @"Use GreenHealth; DELETE FROM Medicaments WHERE CategoryId = @id; DELETE FROM Categories WHERE Id = @id";
                    await connection.QueryFirstOrDefaultAsync(delCategoryQuery, new {id = id});
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