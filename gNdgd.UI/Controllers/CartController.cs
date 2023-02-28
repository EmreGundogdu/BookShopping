using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gNdgd.UI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        readonly ICartRepository cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<IActionResult> AddItem(int bookId,int quantity=1,int redirect=0)
        {
            var cartCount =await cartRepository.AddCart(bookId, quantity);
            if (redirect == 0)
                return Ok();
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await cartRepository.RemoveCart(bookId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GertUserCart()
        {
            var cart = await cartRepository.GetUserCart();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            var cartItem =await cartRepository.GetCartItemCount();
            return Ok(cartItem);
        }
    }
}
