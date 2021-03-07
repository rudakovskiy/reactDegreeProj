using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Orders.DTO
{
    public class OrderElementFullModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("medicamentId")]
        public int MedicamentId { get; set; }
        [JsonProperty("medicamentName")]
        public string MedicamentName { get; set; }
        [JsonProperty("price")]
        public float Price { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}