using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;
using Miachyna.UI.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Miachyna.UI.Services.CosmeticServices
{
    public class MemoryCosmeticService : ICosmeticService
    {
        IConfiguration _config;
        List<Cosmetic> _cosmetic;
        List<Category> _categories;


        public MemoryCosmeticService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _config = config;
            _categories = categoryService.GetCategoryListAsync()
                .Result
                .Data;
            SetupData();
        }

        private void SetupData()
        {
            _cosmetic = new List<Cosmetic>
            {
                new Cosmetic
                {
                    Id = 1,
                    Name = "Skin Tint Blurring Elixir",
                    Description = "1 fl oz | longwearing",
                    Price = 32,
                    Image = "Images/skin-tint.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("face")).Id
                },
                new Cosmetic
                {
                    Id = 2,
                    Name = "Setting Powder",
                    Description = "0.17 oz | mattify + lock in",
                    Price = 26,
                    Image = "Images/powder.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("face")).Id
                },
                new Cosmetic
                {
                    Id = 3,
                    Name = "Kylash Volume Mascara",
                    Description = "0.41 fl oz | VOLUME + LIFT",
                    Price = 24,
                    Image = "Images/mascara.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("eyes")).Id
                },
                new Cosmetic
                {
                    Id = 4,
                    Name = "Classic Matte Palette Duo",
                    Description = "Highly pigmented + easy-to-blend",
                    Price = 57,
                    Image = "Images/palette.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("eyes")).Id
                },
                new Cosmetic
                {
                    Id = 5,
                    Name = "Plumping Gloss",
                    Description = "0.10 oz | plump + shine",
                    Price = 19,
                    Image = "Images/plumping-gloss.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("lips")).Id
                },
                new Cosmetic
                {
                    Id = 6,
                    Name = "Kylie's Lipstick",
                    Description = "A shade & finish",
                    Price = 102,
                    Image = "Images/lipsticks.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("lips")).Id
                },
                new Cosmetic
                {
                    Id = 7,
                    Name = "Kybrow Gel",
                    Description = "0.17 fl oz | COMB + SET",
                    Price = 12,
                    Image = "Images/gel.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("eyebrows")).Id
                },
                new Cosmetic
                {
                    Id = 8,
                    Name = "Kybrow Highlighter",
                    Description = "0.02 oz | brighten + redefine",
                    Price = 17,
                    Image = "Images/highlighter.jpg",
                    CategoryId = _categories.Find(c=>c.NormalizedName.Equals("eyebrows")).Id
                }
            };
        }
        public Task<ResponseData<ListModel<Cosmetic>>> GetCosmeticListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var result = new ResponseData<ListModel<Cosmetic>>();

            int? categoryId = null;

            if (categoryNormalizedName != null)
                categoryId = _categories.Find(c => c.NormalizedName.Equals(categoryNormalizedName))?.Id;

            var data = _cosmetic.Where(d => categoryId == null || d.CategoryId.Equals(categoryId))?.ToList();

            int pageSize = _config.GetSection("Pagination:ItemsPerPage").Get<int>();

            int totalPages;
            if (pageSize == 0)
            {
                pageSize = -1;
                totalPages = 1;
            }
            else
            {
                totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            }

            var listData = new ListModel<Cosmetic>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            result.Data = listData;

            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "There are no objects in the selected category :(";
            }

            return Task.FromResult(result);
        }
        public Task<ResponseData<Cosmetic>> GetCosmeticByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task UpdateCosmeticAsync(int id, Cosmetic flowers, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
        public Task DeleteCosmeticAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseData<Cosmetic>> CreateCosmeticAsync(Cosmetic cosmetic, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
