using Microsoft.AspNetCore.Mvc;

namespace Miachyna.UI.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
