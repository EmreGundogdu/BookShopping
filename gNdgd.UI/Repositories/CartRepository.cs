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



        public async Task<int> AddCart(int bookId, int quantity)
        {
            string userId = GetUserId();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User not found");
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
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> RemoveCart(int bookId)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User not found");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Cart is empty");
                var cartItem = await context.CartDetails.FirstOrDefaultAsync(x => x.ShoppingCartId == cart.Id && x.BookId == bookId);
                if (cartItem is null)
                    throw new Exception("Cart Items are not exists");
                else if (cartItem.Quantity==1)
                    context.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId is null)
                throw new Exception("User not found");
            var shoppingCart = await context.ShoppingCarts.Include(x => x.CartDetails).ThenInclude(x => x.Book).ThenInclude(x => x.Genre).Where(x => x.UserId == userId).FirstOrDefaultAsync();
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

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await(from cart in context.ShoppingCarts join cartDetail in context.CartDetails on cart.Id equals cartDetail.ShoppingCartId select new { cartDetail.Id }).ToListAsync();
            return data.Count;
        }
    }
}
