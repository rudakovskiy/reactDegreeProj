using System.Drawing;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Customers.DTO
{
    public class CustomerAddEntity
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("Login")]
        public string Login { get; set; }
        
        [JsonProperty("Password")]
        public string Password { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }

        public CustomerAddEntity(string fullName, string email, string phone, string login, string password, string city, string address)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            Login = login;
            Password = password;
            City = city;
            Address = address;
        }

        public CustomerAddEntity()
        {
            
        }
    }
}