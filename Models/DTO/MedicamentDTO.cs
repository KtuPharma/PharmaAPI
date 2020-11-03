using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class MedicamentDTO
    {
        [JsonProperty("name")] public string Name { get; }
        [JsonProperty("active_substance")] public string ActiveSubstance { get; }
        [JsonProperty("bar_code")] public string BarCode { get; }
        [JsonProperty("recipe_required")] public bool RecipeRequired { get; }
        [JsonProperty("is_reimbursed")] public bool IsReimbursed { get; }
        [JsonProperty("country")] public string Country { get; }
        [JsonProperty("form")] public string Form { get; }

        public MedicamentDTO(Medicament m)
        {
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
