using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Medicaments.DTO
{
    public class MedicamentModel
    {
        [JsonProperty("name")]
        public string Name { get; }
        
        [JsonProperty("price")]
        public float Price { get; }
        
        [JsonProperty("priceMultiplier")]
        public float PriceMultiplier { get; }
        
        [JsonProperty("manufacturerId")]
        public string ManufacturerId { get; }
        
        [JsonProperty("isPrescriptionNeeded")]
        public bool IsPrescriptionNeeded { get; }
        
        [JsonProperty("dosageFormId")]
        public int DosageFormId { get; }
        
        [JsonProperty("specification")]
        public string Specification { get; }
        
        [JsonProperty("amount")]
        public float Amount { get; }
        
        [JsonProperty("unit")]
        public string Unit { get; }
        
        [JsonProperty("imageName")]
        public string ImageName { get; }

        public MedicamentModel(string name, float price, float priceMultiplier, string manufacturerId, float amount, 
            bool isPrescriptionNeeded, int dosageFormId, string specification, string unit, string imageName)
        {
            Name = name;
            Price = price;
            PriceMultiplier = priceMultiplier;
            ManufacturerId = manufacturerId;
            IsPrescriptionNeeded = isPrescriptionNeeded;
            DosageFormId = dosageFormId;
            Specification = specification;
            Amount = amount;
            Unit = unit;
            ImageName = imageName;    
        }
    }
}