using Miachyna.Domain.Models;
using Miachyna.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Miachyna.UI.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<Cart>("cart");
            return View(cart);
        }
    }
}
