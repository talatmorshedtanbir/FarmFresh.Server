using FarmFresh.Framework.Entities.Carts;

namespace FarmFresh.Framework.Services.Abstract
{
    public interface ICartService : IDisposable
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId);

        Task<IEnumerable<CartItem>> GetCustomerCartItemsAsync(string customerEmail);

        Task AddCartItemAsync(string customerEmail, int productId);

        Task AddCartAsync(string customerEmail);

        Task DeleteCartItemAsync(string customerEmail, int productId);

        Task EmptyCartAsync(string customerEmail);
    }
}
