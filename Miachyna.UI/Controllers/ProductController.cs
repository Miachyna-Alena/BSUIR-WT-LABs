﻿using Miachyna.UI.Services.CategoryServices;
using Miachyna.UI.Services.CosmeticServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Miachyna.UI.Controllers
{
    [Authorize]
    public class ProductController(ICategoryService categoryService, ICosmeticService cosmeticService) : Controller
    {
        public async Task<IActionResult> Index(string? category)
        {
            var categoriesResponse = await categoryService.GetCategoryListAsync();

            if (!categoriesResponse.Success)
                return NotFound(categoriesResponse.ErrorMessage);

            ViewData["categories"] = categoriesResponse.Data;

            var currentCategory = category == null ? "All" : categoriesResponse.Data.FirstOrDefault(c => c.NormalizedName == category)?.Name;

            ViewData["currentCategory"] = currentCategory;

            var productResponse = await cosmeticService.GetCosmeticListAsync(category);

            if (!productResponse.Success)
                ViewData["Error"] = productResponse.ErrorMessage;
            return View(productResponse.Data.Items);
        }
    }
}