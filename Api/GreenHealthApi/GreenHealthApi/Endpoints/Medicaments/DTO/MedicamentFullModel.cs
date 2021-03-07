using System.Security.Cryptography;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Medicaments.DTO
{
    public class MedicamentFullModel
    {
        [JsonProperty("id")]
        public int Id { get; }
        
        [JsonProperty("name")]
        public string Name { get; }
        
        [JsonProperty("price")]
        public float Price { get; }
        
        [JsonProperty("priceMultiplier")]
        public float PriceMultiplier { get; }
        
        [JsonProperty("manufacturerId")]
        public string ManufacturerId { get; }
        
        [JsonProperty("manufacturerPhone")]
        public string ManufacturerPhone { get; }
        
        [JsonProperty("manufacturerName")]
        public string ManufacturerName{ get; }
        
        [JsonProperty("isPrescriptionNeeded")]
        public bool IsPrescriptionNeeded { get; }
        
        [JsonProperty("dosageFormId")]
        public int DosageFormId { get; }

        [JsonProperty("dosageForm")] 
        public string DosageForm;
        
        [JsonProperty("specification")]
        public string Specification { get; }
        
        [JsonProperty("amount")]
        public float Amount { get; }
        
        [JsonProperty("unit")]
        public string Unit { get; }
        
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        
        [JsonProperty("category")]
        public string Category { get; }

        public MedicamentFullModel()
        {
            
        }
        
        public MedicamentFullModel(
            int id,
            string name,
            float price,
            float priceMultiplier,
            string manufacturerId,
            string manufacturerPhone,
            string manufacturerName,
            bool isPrescriptionNeeded,
            int dosageFormId,
            string specification,
            float amount,
            string unit,
            string imageUrl,
            string category)
        {
            Id = id;
            Name = name;
            Price = price;
            PriceMultiplier = priceMultiplier;
            ManufacturerId = manufacturerId;
            ManufacturerPhone = manufacturerPhone;
            ManufacturerName = manufacturerName;
            IsPrescriptionNeeded = isPrescriptionNeeded;
            DosageFormId = dosageFormId;
            Specification = specification;
            Amount = amount;
            Unit = unit;
            ImageUrl = imageUrl;
            Category = category;
        }
    }
}