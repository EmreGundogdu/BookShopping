namespace gNdgd.UI
{
    public interface ICartRepository
    {
        Task<bool> AddCart(int bookId, int quantity);
        Task<bool> RemoveCart(int bookId);
        Task<IEnumerable<ShoppingCart>> GetUserCart();
    }
}