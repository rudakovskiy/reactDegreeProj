using System;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Orders.DTO
{
    public class OrderModel
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("isConfirmed")]
        public bool IsConfirmed { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        
        [JsonProperty("orderElements")]
        public OrderElementEntity[] OrderElements { get; set; }
        
    }
}