using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;

namespace Miachyna.UI.Services.CategoryServices
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
