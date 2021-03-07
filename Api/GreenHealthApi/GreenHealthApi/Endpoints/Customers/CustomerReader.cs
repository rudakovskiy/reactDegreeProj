using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using GreenHealthApi.Endpoints.Authentication;
using GreenHealthApi.Endpoints.Customers.DTO;
using GreenHealthApi.Exceptions;
using GreenHealthApi.Models;

namespace GreenHealthApi.Endpoints.Customers
{
    public interface ICustomerReader
    {
        Task<CustomerModel> GetCustomerByIdAsync(int id);
        Task<UserModel> GetIdentityUserAsync(string phone, string password);
    }

    public class CustomerReader : ICustomerReader
    {
        private readonly IConnectionStringGetter _conStr;

        public CustomerReader(IConnectionStringGetter conStr)
        {
            _conStr = conStr;
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    await connection.OpenAsync();
                    var getUserQuery =
                        @"USE GreenHealth;
                      SELECT Id, FullName, Email, Phone, Login, City, Address
                      FROM Customers 
                      WHERE Id = @id;";
                    var customer =
                        await connection.QueryFirstOrDefaultAsync<CustomerModel>(getUserQuery, new {id = id});
                    return customer;
                }
                catch (Exception e)
                {
                    throw new SqlQueryException(e.Message);
                }
            }
        }

        public async Task<UserModel> GetIdentityUserAsync(string phone, string password)
        {
            using (var connection = new SqlConnection(_conStr.Get()))
            {
                try
                {
                    await connection.OpenAsync();
                    var getUserQuery =
                        @"USE GreenHealth;
                      SELECT Id, FullName, Email, Phone, Login, City, Address
                      FROM Customers
                      WHERE Phone = @phone AND Password = @password;";
                    var user = await connection.QueryFirstOrDefaultAsync<UserModel>(getUserQuery,
                        new {phone = @phone, password = @password});
                    if (user != null)
                    {
                        user.Role = "customer";
                    }
                    else
                    {
                        getUserQuery =
                            @"USE GreenHealth;
                      SELECT Id, FullName, Email, Phone, Login, City, Address
                      FROM Admins 
                      WHERE Phone = @phone AND Password = @password;";
                        user = await connection.QueryFirstOrDefaultAsync<UserModel>(getUserQuery,
                            new {phone = @phone, password = @password});
                        if (user != null)
                        {
                            user.Role = "admin";
                        }
                    }

                    return user;
                }
                catch (Exception e)
                {
                    throw new SqlQueryException(e.Message);
                }
            }
        }
    }
}