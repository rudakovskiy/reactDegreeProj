using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Medicaments.DTO
{
    public class MedicamentFullResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; }
        
        [JsonProperty("name")]
        public string Name { get; }
        
        [JsonProperty("price")]
        public float Price { get; }
        
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
        
        [JsonProperty("categoryId")]
        public int CategoryId { get; }
        
        [JsonProperty("category")]
        public string Category { get; }
        
        [JsonProperty("isHide")]
        public bool IsHide { get; }

        public MedicamentFullResponseModel(int id, string name, float price, string manufacturerId, string manufacturerPhone, string manufacturerName, bool isPrescriptionNeeded, int dosageFormId, string specification, float amount, string unit, string imageUrl, string category, int catId, bool isHide)
        {
            Id = id;
            Name = name;
            Price = price;
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
            CategoryId = catId;
            IsHide = isHide;
        }

        public MedicamentFullResponseModel()
        {
            
        }
    }
}