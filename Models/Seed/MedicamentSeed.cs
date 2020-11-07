namespace API.Models.Seed
{
    public static class MedicamentSeed
    {
        public static void EnsureCreated(ApiContext context)
        {
            context.Medicaments.AddRange(
                new Medicament
                {
                    Id = 1,
                    Name = "TestDrug001",
                    ActiveSubstance = "Dopamine",
                    BarCode = "00001",
                    RecipeRequired = false,
                    IsReimbursed = false,
                    Country = "Lithuania",
                    Form = PharmaceuticalFormId.Spray
                },
                new Medicament
                {
                    Id = 2,
                    Name = "TestDrug002",
                    ActiveSubstance = "Methamphetamine",
                    BarCode = "00002",
                    RecipeRequired = true,
                    IsReimbursed = false,
                    Country = "Germany",
                    Form = PharmaceuticalFormId.Tablets
                },
                new Medicament
                {
                    Id = 3,
                    Name = "TestDrug003",
                    ActiveSubstance = "Adderall",
                    BarCode = "00003",
                    RecipeRequired = true,
                    IsReimbursed = true,
                    Country = "USA",
                    Form = PharmaceuticalFormId.Tablets
                }, new Medicament
                {
                    Id = 4,
                    Name = "TestDrug004",
                    ActiveSubstance = "<Redacted>",
                    BarCode = "00004",
                    RecipeRequired = false,
                    IsReimbursed = false,
                    Country = "France",
                    Form = PharmaceuticalFormId.Ointment
                }
            );
        }
    }
}
