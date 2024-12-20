using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;

namespace Miachyna.UI.Services.CategoryServices
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>>
        GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Face", NormalizedName="face"},
                new Category {Id=2, Name="Eyes", NormalizedName="eyes"},
                new Category {Id=3, Name="Lips", NormalizedName="lips"},
                new Category {Id=4, Name="Eyebrows", NormalizedName="eyebrows"}
            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }
}
