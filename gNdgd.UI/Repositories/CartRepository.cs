using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gNdgd.UI.Repositories
{
    public class CartRepository : ICartRepository
    {
        readonly ApplicationDbContext context;
        readonly UserManager<IdentityUser> userManager;
        readonly IHttpContextAccessor httpContextAccessor;
        public CartRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }



        public async Task<bool> AddCart(int bookId, int quantity)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId,
                    };
                    context.ShoppingCarts.Add(cart);
                }
                context.SaveChanges();
                var cartItem = await context.CartDetails.FirstOrDefaultAsync(x => x.ShoppingCartId == cart.Id && x.BookId == bookId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = quantity
                    };
                    context.CartDetails.Add(cartItem);
                }
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveCart(int bookId)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    return false;   
                }
                var cartItem = await context.CartDetails.FirstOrDefaultAsync(x => x.ShoppingCartId == cart.Id && x.BookId == bookId);
                if (cartItem is null)
                    return false;
                else if(cartItem.Quantity==1)
                {
                    context.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserCart()
        {
            var userId = GetUserId();
            if (userId is null)
                throw new Exception("User not found");
            var shoppingCart = await context.ShoppingCarts.Include(x => x.CartDetails).ThenInclude(x => x.Book).ThenInclude(x => x.Genre).Where(x => x.UserId == userId).ToListAsync();
            return shoppingCart;
        }

        private async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        private string GetUserId()
        {
            var user = httpContextAccessor.HttpContext.User;
            var userId = userManager.GetUserId(user);
            return userId;
        }
    }
}
