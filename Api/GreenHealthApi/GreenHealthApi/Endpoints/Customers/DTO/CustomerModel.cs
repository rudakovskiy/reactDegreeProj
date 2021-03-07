using System;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Customers.DTO
{
    public class CustomerModel
    {
        [JsonProperty("id")]
        public int Id { get; }
        
        [JsonProperty("fullName")]
        public string FullName { get; }
        
        [JsonProperty("email")]
        public string Email { get; }
        
        [JsonProperty("phone")]
        public string Phone { get; }
        
        [JsonProperty("Login")]
        public string Login { get; }
        
        [JsonProperty("city")]
        public string City { get; }
        
        [JsonProperty("address")]
        public string Address { get; }
        
        
        
        [JsonConstructor]
        public CustomerModel(int id, string fullName, string email, string phone, string login, string city, string address)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Phone = phone;
            Login = login;
            City = city;
            Address = address;
        }

        public CustomerModel()
        {
            
        }
    }
}