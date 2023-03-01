using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gNdgd.UI.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        readonly IUserOrderRepository userOrderRepository;

        public UserOrderController(IUserOrderRepository userOrderRepository)
        {
            this.userOrderRepository = userOrderRepository;
        }

        public async Task<ActionResult> UserOrders()
        {
            var orders = await userOrderRepository.UserOrders();
            return View(orders);
        }
    }
}
