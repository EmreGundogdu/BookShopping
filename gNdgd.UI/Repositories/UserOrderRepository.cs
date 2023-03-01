using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gNdgd.UI.Repositories
{
    public class UserOrderRepository: IUserOrderRepository
    {
        readonly ApplicationDbContext context;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly UserManager<IdentityUser> userManager;
        public UserOrderRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not found");
            var orders =await context.Orders.Include(x => x.OrderDetails).ThenInclude(x=>x.Book).ThenInclude(x=>x.Genre).Where(x => x.UserId == userId).ToListAsync();
            return orders;
        }
        private string GetUserId()
        {
            var user = httpContextAccessor.HttpContext.User;
            var userId = userManager.GetUserId(user);
            return userId;
        }
    }
}
