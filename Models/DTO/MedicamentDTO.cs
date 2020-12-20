using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class MedicamentDTO
    {
        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("activeSubstance")]
        public string ActiveSubstance { get; }

        [JsonProperty("barCode")]
        public string BarCode { get; }

        [JsonProperty("recipeRequired")]
        public bool RecipeRequired { get; }

        [JsonProperty("isReimbursed")]
        public bool IsReimbursed { get; }

        [JsonProperty("country")]
        public string Country { get; }

        [JsonProperty("form")]
        public string Form { get; }

        public MedicamentDTO(Medicament m)
        {
            Id = m.Id;
            Name = m.Name;
            ActiveSubstance = m.ActiveSubstance;
            BarCode = m.BarCode;
            RecipeRequired = m.RecipeRequired;
            IsReimbursed = m.IsReimbursed;
            Country = m.Country;
            Form = m.Form.ToString();
        }
    }
}
