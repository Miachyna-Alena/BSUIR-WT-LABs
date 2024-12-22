using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Miachyna.Domain.Entities;
using Miachyna.UI.Services.CategoryServices;
using Miachyna.UI.Services.CosmeticServices;

namespace Miachyna.UI.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, ICosmeticService cosmeticService) : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var categoryListData = await categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Cosmetic Cosmetic { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await cosmeticService.CreateCosmeticAsync(Cosmetic, Image);
            return RedirectToPage("./Index");
        }
    }
}
