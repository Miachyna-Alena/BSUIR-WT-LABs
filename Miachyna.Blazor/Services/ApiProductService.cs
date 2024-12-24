using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;

namespace Miachyna.Blazor.Services
{
    public class ApiProductService(HttpClient Http) : IProductService<Cosmetic>
    {
        List<Cosmetic> _cosmetics;
        int _currentPage = 1;
        int _totalPages = 1;
        public IEnumerable<Cosmetic> Products => _cosmetics;
        public int CurrentPage => _currentPage;
        public int TotalPages => _totalPages;
        public event Action ListChanged;
        public async Task GetProducts(int pageNo, int pageSize)
        {
            var uri = Http.BaseAddress.AbsoluteUri;
            var queryData = new Dictionary<string, string>
            {
                { "pageNo", pageNo.ToString() },
                { "pageSize", pageSize.ToString() }
            };
            var query = QueryString.Create(queryData);
            var result = await Http.GetAsync(uri + query.Value);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content
                    .ReadFromJsonAsync<ResponseData<ListModel<Cosmetic>>>();
                _currentPage = responseData.Data.CurrentPage;
                _totalPages = responseData.Data.TotalPages;
                _cosmetics = responseData.Data.Items;
                ListChanged?.Invoke();
            }
            else
            {
                _cosmetics = null;
                _currentPage = 1;
                _totalPages = 1;
            }
        }
    }
}
