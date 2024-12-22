using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Miachyna.Domain.Entities;
using Miachyna.UI.Services.CosmeticServices;
using Miachyna.UI.Services.CategoryServices;

namespace Miachyna.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICosmeticService _cosmeticService;
        private readonly ICategoryService _categoryService;

        public EditModel(ICosmeticService cosmeticService, ICategoryService categoryService)
        {
            _cosmeticService = cosmeticService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Cosmetic Cosmetic { get; set; } = default!;
        public SelectList CategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic =  await _cosmeticService.GetCosmeticByIdAsync(id.Value);
            
            if (cosmetic == null)
            {
                return NotFound();
            }

            Cosmetic = cosmetic.Data;

            var categoryListData = await _categoryService.GetCategoryListAsync();
            ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id", "Name");
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _cosmeticService.UpdateCosmeticAsync(Cosmetic.Id, Cosmetic, null);
            }
            catch (Exception)
            {
                if (!CosmeticExists(Cosmetic.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CosmeticExists(int id)
        {
            return true;
        }
    }
}
