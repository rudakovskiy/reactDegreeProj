using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Medicaments.DTO
{
    public class MedicamentEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("price")]
        public float Price { get; set; }
        
        [JsonProperty("priceMultiplier")]
        public float PriceMultiplier { get; set; }
        
        [JsonProperty("manufacturerId")]
        public int ManufacturerId { get; set; }
        
        [JsonProperty("isPrescriptionNeeded")]
        public bool IsPrescriptionNeeded { get; set; }
        
        [JsonProperty("dosageFormId")]
        public int DosageFormId { get; set; }
        
        [JsonProperty("specification")]
        public string Specification { get; set; }
        
        [JsonProperty("amount")]
        public float Amount { get; set; }
        
        [JsonProperty("unit")]
        public string Unit { get; set; }
        
        [JsonProperty("imageName")]
        public string ImageName { get; set; }
        
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonConstructor]
        public MedicamentEntity(int id, string name, float price, float priceMultiplier, int manufacturerId, bool isPrescriptionNeeded, int dosageFormId, string specification, float amount, string unit, string imageName, int categoryId)
        {
            Id = id;
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
            CategoryId = categoryId;
        }



        

        public MedicamentEntity()
        {
            
        }
       
    }
}