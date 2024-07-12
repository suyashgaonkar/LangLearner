using LangLearner.Models.Entities;

namespace LangLearner.Database.Seeders
{
    public static class DatabaseInitializer
    {

        public static async Task SeedDataAsync(AppDbContext context)
        {
            await SeedDefaultLanguagesAsync(context);
        }

        private static async Task SeedDefaultLanguagesAsync(AppDbContext context)
        {
            if(!context.Languages.Any())
            {
                var languages = new List<Language>
                {
                    new Language {Code="en", Name="English", NativeName="English"},
                    new Language {Code="es", Name="Spanish", NativeName="Espańol"},
                    new Language {Code="pl", Name="Polish", NativeName="Polski"},
                    new Language {Code="de", Name="German", NativeName="Deutsch"},
                    new Language {Code="ua", Name="Ukrainian", NativeName="Українська"},
                };

                context.Languages.AddRange(languages);
                if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
            }
        }


    }
}
