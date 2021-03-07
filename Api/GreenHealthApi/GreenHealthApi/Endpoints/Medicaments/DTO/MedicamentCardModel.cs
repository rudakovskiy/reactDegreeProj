using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Medicaments.DTO
{
    public class MedicamentCardModel
    {
        [JsonProperty("id")]
        public int Id { get; }
        
        [JsonProperty("name")]
        public string Name { get; }
        
        [JsonProperty("price")]
        public float Price { get; }
        
        [JsonProperty("priceMultiplier")]
        public float PriceMultiplier { get; }
        
        [JsonProperty("isPrescriptionNeeded")]
        public bool IsPrescriptionNeeded { get; }
        
        [JsonProperty("manufacturerName")]
        public string ManufacturerName{ get; }
        
        [JsonProperty("dosageForm")] 
        public string DosageForm;
        
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; }
    }
}