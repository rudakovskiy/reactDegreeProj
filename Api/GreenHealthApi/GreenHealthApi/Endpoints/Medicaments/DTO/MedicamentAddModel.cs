using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Medicaments.DTO
{
    public class MedicamentAddModel
    {
        [JsonProperty("name")]
        public string Name { get; set;}
        
        [JsonProperty("price")]
        public float Price { get; set;}

        [JsonProperty("priceMultiplier")] 
        public float PriceMultiplier { get; set; }

        [JsonProperty("manufacturerName")]
        public string ManufacturerName{ get; set;}
        
        [JsonProperty("isPrescriptionNeeded")]
        public bool IsPrescriptionNeeded { get; set;}

        [JsonProperty("dosageForm")] 
        public string DosageForm { get; set; }
        
        [JsonProperty("specification")]
        public string Specification { get; set;}
        
        [JsonProperty("amount")]
        public float Amount { get; set;}
        
        [JsonProperty("unit")]
        public string Unit { get; set;}
        
        [JsonProperty("category")]
        public string Category { get; set;}

        public MedicamentAddModel()
        {
            
        }

        public MedicamentAddModel(string name, float price, float priceMultiplier, string manufacturerName, bool isPrescriptionNeeded, string dosageForm, string specification, float amount, string unit, string category)
        {
            Name = name;
            Price = price;
            PriceMultiplier = priceMultiplier;
            ManufacturerName = manufacturerName;
            IsPrescriptionNeeded = isPrescriptionNeeded;
            DosageForm = dosageForm;
            Specification = specification;
            Amount = amount;
            Unit = unit;
            Category = category;
        }

    }
}