using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;

namespace GreenHealthApi.Models
{
    public class Person
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonConstructor]
        public Person(string phone, string password)
        {
            Phone = phone;
            Password = password;
        }

        public Person()
        {
            
        }
    }
}