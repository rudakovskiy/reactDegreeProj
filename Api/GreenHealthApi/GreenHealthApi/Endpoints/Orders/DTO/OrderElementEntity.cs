using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Orders.DTO
{
    public class OrderElementEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }

        public OrderElementEntity()
        {
            
        }

        public OrderElementEntity(int id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}