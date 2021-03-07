using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using GreenHealthApi.Endpoints.Customers.DTO;
using GreenHealthApi.Endpoints.Medicaments.DTO;
using GreenHealthApi.Exceptions;

namespace GreenHealthApi.Endpoints.Customers
{
    public interface ICustomerWriter
    {
        Task<bool> AddCustomerAsync(CustomerAddEntity customer);
        Task<bool> ChangePassword(string phone, string oldPsw, string newPsw);
    }
    
    public class CustomerWriter : ICustomerWriter
    {
        private readonly IConnectionStringGetter _conStr;

        public CustomerWriter(IConnectionStringGetter conStr)
        {
            _conStr = conStr;
        }
        
        public async Task<bool> AddCustomerAsync(CustomerAddEntity customer)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    await connection.OpenAsync();
                    var addCustomerQuery =
                        @"USE GreenHealth
                      INSERT INTO Customers (FullName, Email, Phone, Login, Password, City, Address) 
                      VALUES (@fullName, @email, @phone, @login, @password, @city, @address);";
                    
                    await connection.QueryAsync(addCustomerQuery, 
                        new {fullName = customer.FullName, email = customer.Email, 
                            phone = customer.Phone, login = customer.Login, password = customer.Password, 
                            city = customer.City, address = customer.Address});
                }
                catch (Exception e)
                {
                    return false;
                    throw new SqlQueryException(e.Message);
                }
                return true;
            }
        }

        public async Task<bool> ChangePassword(string phone, string oldPsw, string newPsw)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    var getUserPassQuery = @"USE GreenHealth; SELECT Password FROM Customers Where phone = @phone";
                    var password = await connection.QueryFirstOrDefaultAsync<string>(getUserPassQuery, new {phone});

                    if (password != oldPsw)
                        return false;

                    var updateUserPassQuery = @"USE GreenHealth; UPDATE Customers SET Password = @newPsw WHERE phone = @phone";
                    await connection.ExecuteAsync(updateUserPassQuery, new {phone = phone, newPsw = newPsw});
                    return true;

                }
                catch (Exception e)
                {
                    return false;
                    Console.WriteLine(e);
                    throw;
                }

            }
        }
    }
}