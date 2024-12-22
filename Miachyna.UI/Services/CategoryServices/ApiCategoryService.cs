using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;

namespace Miachyna.UI.Services.CategoryServices
{
    public class ApiCategoryService(HttpClient httpClient) : ICategoryService
    {
        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<List<Category>>>();
            };
            var response = new ResponseData<List<Category>>
            { Success = false, ErrorMessage = "API reading error!" };
            return response;
        }
    }
}
