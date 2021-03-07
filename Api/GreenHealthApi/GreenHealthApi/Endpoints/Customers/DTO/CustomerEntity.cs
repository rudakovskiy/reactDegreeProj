using System;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Customers.DTO
{
    public class CustomerEntity
    {
        [JsonProperty("id")]
        public string Id { get; }
        
        [JsonProperty("fullName")]
        public string FullName { get; }
        
        [JsonProperty("email")]
        public string Email { get; }
        
        [JsonProperty("phone")]
        public string Phone { get; }
        
        [JsonProperty("Login")]
        public string Login { get; }
        
        [JsonProperty("Password")]
        public string Password { get; }
        
        [JsonProperty("city")]
        public string City { get; }
        
        [JsonProperty("address")]
        public string Address { get; }
    
        public CustomerEntity(string id, string fullName, string email, string phone, string login, string password, 
            string city, string address)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Phone = phone;
            Login = login;
            Password = password;
            City = city;
            Address = address;
        }        

        public CustomerEntity()
        {
            
        }
    }
    
}