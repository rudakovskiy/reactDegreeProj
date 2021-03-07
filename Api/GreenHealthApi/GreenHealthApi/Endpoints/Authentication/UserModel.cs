using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Authentication
{
    public class UserModel
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
        
        [JsonProperty("city")]
        public string City { get; }
        
        [JsonProperty("address")]
        public string Address { get; }
        [JsonProperty("role")]
        public string Role { get; set;  }
        
        [JsonConstructor]
        public UserModel(string id, string fullName, string email, string phone, string login, string city, string address)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Phone = phone;
            Login = login;
            City = city;
            Address = address;
        }

        public UserModel()
        {
            
        }
    }
}




















































































