using Microsoft.AspNetCore.Mvc.RazorPages;
using Miachyna.Domain.Entities;
using Miachyna.UI.Services.CosmeticServices;
using Microsoft.AspNetCore.Authorization;

namespace Miachyna.UI.Areas.Admin.Pages
{
    [Authorize(Policy ="admin")]
    public class IndexModel : PageModel
    {
        private readonly ICosmeticService _cosmeticService;
        public IndexModel(ICosmeticService cosmeticService)
        {
            _cosmeticService = cosmeticService;
        }
        public List<Cosmetic> Cosmetic { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _cosmeticService.GetCosmeticListAsync(null, pageNo.Value);
            if (response.Success)
            {
                Cosmetic = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}
