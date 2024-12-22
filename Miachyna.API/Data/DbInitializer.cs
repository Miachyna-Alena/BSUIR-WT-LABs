using Miachyna.Domain.Entities;

namespace Miachyna.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Заполнение данными
            if (!context.Categories.Any() && !context.Cosmetics.Any())
            {
                var categories = new Category[]
                {
                    new Category {Id=1, Name="Face", NormalizedName="face"},
                    new Category {Id=2, Name="Eyes", NormalizedName="eyes"},
                    new Category {Id=3, Name="Lips", NormalizedName="lips"},
                    new Category {Id=4, Name="Eyebrows", NormalizedName="eyebrows"}
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();

                var cosmetics = new List<Cosmetic>
                {
                    new Cosmetic
                {
                    Id = 1,
                    Name = "Skin Tint Blurring Elixir",
                    Description = "1 fl oz | longwearing",
                    Price = 32,
                    Image = "Images/skin-tint.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("face"))
                },
                new Cosmetic
                {
                    Id = 2,
                    Name = "Setting Powder",
                    Description = "0.17 oz | mattify + lock in",
                    Price = 26,
                    Image = "Images/powder.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("face"))
                },
                new Cosmetic
                {
                    Id = 3,
                    Name = "Kylash Volume Mascara",
                    Description = "0.41 fl oz | VOLUME + LIFT",
                    Price = 24,
                    Image = "Images/mascara.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("eyes"))
                },
                new Cosmetic
                {
                    Id = 4,
                    Name = "Classic Matte Palette Duo",
                    Description = "Highly pigmented + easy-to-blend",
                    Price = 57,
                    Image = "Images/palette.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("eyes"))
                },
                new Cosmetic
                {
                    Id = 5,
                    Name = "Plumping Gloss",
                    Description = "0.10 oz | plump + shine",
                    Price = 19,
                    Image = "Images/plumping-gloss.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("lips"))
                },
                new Cosmetic
                {
                    Id = 6,
                    Name = "Kylie's Lipstick",
                    Description = "A shade & finish",
                    Price = 102,
                    Image = "Images/lipsticks.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("lips"))
                },
                new Cosmetic
                {
                    Id = 7,
                    Name = "Kybrow Gel",
                    Description = "0.17 fl oz | COMB + SET",
                    Price = 12,
                    Image = "Images/gel.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("eyebrows"))
                },
                new Cosmetic
                {
                    Id = 8,
                    Name = "Kybrow Highlighter",
                    Description = "0.02 oz | brighten + redefine",
                    Price = 17,
                    Image = "Images/highlighter.jpg",
                    Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("eyebrows"))
                }
                };
                await context.AddRangeAsync(cosmetics);
                await context.SaveChangesAsync();
            }
        }
    }
}
