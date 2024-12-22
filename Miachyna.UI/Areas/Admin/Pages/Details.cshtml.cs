using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Miachyna.Domain.Entities;
using Miachyna.UI.Services.CosmeticServices;

namespace Miachyna.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICosmeticService _cosmeticService;

        public DetailsModel(ICosmeticService cosmeticService)
        {
            _cosmeticService = cosmeticService;
        }

        public Cosmetic Cosmetic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic = await _cosmeticService.GetCosmeticByIdAsync(id.Value);

            if (cosmetic == null)
            {
                return NotFound();
            }
            else
            {
                Cosmetic = cosmetic.Data;
            }
            return Page();
        }
    }
}
