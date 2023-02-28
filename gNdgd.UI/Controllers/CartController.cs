using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gNdgd.UI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public IActionResult AddItem(int bookId,int quantity=1)
        {
            return View();
        }
        public IActionResult RemoveItem(int bookId)
        {
            return View();
        }
        public IActionResult GertUserCart()
        {
            return View();
        }
        public IActionResult GetTotalItemInCart()
        {
            return View();
        }
    }
}
