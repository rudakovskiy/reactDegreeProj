using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Orders.DTO
{
    public class OrderFullModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("isConfirmed")]
        public bool IsConfirmed { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        
        [JsonProperty("orderElements")]
        public IEnumerable<OrderElementFullModel> OrderElements { get; set; }
    }
}