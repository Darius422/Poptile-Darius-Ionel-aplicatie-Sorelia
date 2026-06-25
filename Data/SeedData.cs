using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BeautyHealthStore.Data
{
    public static class SeedData
    {
        public static async Task Initialize(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            string adminEmail = "test@test.ro";
            string adminPassword = "Test123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                await userManager.CreateAsync(adminUser, adminPassword);
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                await userManager.AddToRoleAsync(adminUser, "Admin");

            if (context.Categories.Any())
                return;

            var categories = new Category[]
            {
                new Category { Name = "Produse pentru fata" },
                new Category { Name = "Produse pentru corp" },
                new Category { Name = "Produse pentru par" },
                new Category { Name = "Produse naturiste" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var products = new Product[]
            {
                new Product
                {
                    Name = "Crema hidratanta Sorelia",
                    Description = "Crema pentru hidratarea zilnica a tenului.",
                    Price = 49.99m,
                    ImageUrl = "/images/products/crema-hidratanta-sorelia.png",
                    Stock = 25,
                    Section = "Infrumusetare",
                    IsNew = true,
                    IsBestSeller = false,
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Serum cu acid hialuronic",
                    Description = "Serum hidratant pentru un ten mai luminos si mai ferm.",
                    Price = 59.99m,
                    ImageUrl = "/images/products/serum-cu-acid-hialuronic.png",
                    Stock = 18,
                    Section = "Infrumusetare",
                    IsNew = true,
                    IsBestSeller = true,
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Masca faciala cu argila vulcanica",
                    Description = "Masca pentru curatarea si revitalizarea tenului.",
                    Price = 44.99m,
                    ImageUrl = "/images/products/masca-faciala-cu-argila-vulcanica.png",
                    Stock = 22,
                    Section = "Infrumusetare",
                    IsNew = false,
                    IsBestSeller = false,
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Lotiune de corp Aloe Vera",
                    Description = "Lotiune delicata pentru hidratarea si ingrijirea pielii.",
                    Price = 39.99m,
                    ImageUrl = "/images/products/lotiune-de-corp-aloe-vera.png",
                    Stock = 30,
                    Section = "Infrumusetare",
                    IsNew = false,
                    IsBestSeller = true,
                    CategoryId = categories[1].Id
                },
                new Product
                {
                    Name = "Sampon revitalizant",
                    Description = "Sampon pentru par fragil si lipsit de vitalitate.",
                    Price = 34.50m,
                    ImageUrl = "/images/products/sampon-revitalizant.png",
                    Stock = 20,
                    Section = "Infrumusetare",
                    IsNew = true,
                    IsBestSeller = false,
                    CategoryId = categories[2].Id
                },
                new Product
                {
                    Name = "Ulei pentru par cu argan",
                    Description = "Ulei hranitor pentru par uscat, deteriorat si lipsit de stralucire.",
                    Price = 54.99m,
                    ImageUrl = "/images/products/ulei-pentru-par-cu-argan.png",
                    Stock = 16,
                    Section = "Infrumusetare",
                    IsNew = true,
                    IsBestSeller = false,
                    CategoryId = categories[2].Id
                },
                new Product
                {
                    Name = "Ceai naturist relaxant",
                    Description = "Ceai naturist recomandat pentru relaxare si stare de bine.",
                    Price = 24.99m,
                    ImageUrl = "/images/products/ceai-naturist-relaxant.png",
                    Stock = 40,
                    Section = "Sanatate",
                    IsNew = false,
                    IsBestSeller = true,
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Crema pentru articulatii si oase",
                    Description = "Crema pentru confort, mobilitate si ingrijirea articulatiilor.",
                    Price = 69.99m,
                    ImageUrl = "/images/products/crema-pentru-articulatii-si-oase.png",
                    Stock = 14,
                    Section = "Sanatate",
                    IsNew = true,
                    IsBestSeller = false,
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Crema cu venin de albine pentru durere",
                    Description = "Crema naturista pentru masaj si calmarea disconfortului muscular.",
                    Price = 74.99m,
                    ImageUrl = "/images/products/crema-cu-venin-de-albine-pentru-durere.png",
                    Stock = 12,
                    Section = "Sanatate",
                    IsNew = true,
                    IsBestSeller = true,
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Gel exfoliant cu extract de portocala",
                    Description = "Gel exfoliant pentru curatarea si revigorarea pielii.",
                    Price = 42.99m,
                    ImageUrl = "/images/products/gel-exfoliant-cu-extract-de-portocala.png",
                    Stock = 26,
                    Section = "Infrumusetare",
                    IsNew = true,
                    IsBestSeller = false,
                    CategoryId = categories[0].Id
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}