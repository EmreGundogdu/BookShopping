namespace gNdgd.UI
{
    public interface ICartRepository
    {
        Task<int> AddCart(int bookId, int quantity);
        Task<int> RemoveCart(int bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<bool> DoCheckout();
    }
}