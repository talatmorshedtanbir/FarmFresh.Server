using FarmFresh.Common.Exceptions;
using FarmFresh.Framework.Entities.Carts;
using FarmFresh.Framework.Entities.Products;
using FarmFresh.Framework.Entities.Users;
using FarmFresh.Framework.Services.Abstract;
using FarmFresh.Framework.UnitOfWorks.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Framework.Services.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartUnitOfWork _cartUnitOfWork;
        private readonly ICartItemUnitOfWork _cartItemUnitOfWork;
        private readonly ICustomerCartUnitOfWork _customerCartUnitOfWork;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public CartService(ICartUnitOfWork cartUnitOfWork,
            ICartItemUnitOfWork cartItemUnitOfWork,
            ICustomerCartUnitOfWork customerCartUnitOfWork,
            IUserService userService,
            IProductService productService)
        {
            _cartUnitOfWork = cartUnitOfWork;
            _cartItemUnitOfWork = cartItemUnitOfWork;
            _customerCartUnitOfWork = customerCartUnitOfWork;
            _userService = userService;
            _productService = productService;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId)
        {
            var cartItems = await _cartItemUnitOfWork.CartItemRepository.GetByCartIdAsync(cartId);

            return cartItems;
        }

        public async Task AddCartItemAsync(string customerEmail, int productId)
        {
            var customer = await _userService.GetAsync(customerEmail);

            if (customer is null)
            {
                throw new NotFoundException(nameof(User), nameof(customerEmail));
            }

            var product = await _productService.GetByIdAsync(productId);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), nameof(productId));
            }

            var customerCart = await _customerCartUnitOfWork.CustomerCartRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.CustomerId == customer.Id,
                x => x.Include(i => i.Cart));

            var existingCartItem = await _cartItemUnitOfWork.CartItemRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.ProductId == productId && x.CartId == customerCart.CartId,
                x => x.Include(i => i.Cart));

            if (existingCartItem is not null)
            {
                existingCartItem.Quantity++;
                existingCartItem.Cost += product.Price;
                existingCartItem.Cart.TotalCost += product.Price;

                await _cartItemUnitOfWork.CartItemRepository.UpdateAsync(existingCartItem);
                await _cartItemUnitOfWork.SaveChangesAsync();
            }
            else
            {
                customerCart.Cart.TotalCost += product.Price;

                var newCartItem = new CartItem
                {
                    CartId = customerCart.CartId,
                    ProductId = productId,
                    Quantity = 1,
                    Cost = product.Price
                };

                await _cartItemUnitOfWork.CartItemRepository.AddAsync(newCartItem);
                await _customerCartUnitOfWork.CustomerCartRepository.UpdateAsync(customerCart);
            }

            await _customerCartUnitOfWork.SaveChangesAsync();
            await _cartItemUnitOfWork.SaveChangesAsync();
        }

        public async Task AddCartAsync(string customerEmail)
        {
            var customer = await _userService.GetAsync(customerEmail);

            if (customer is null)
            {
                throw new NotFoundException(nameof(User), nameof(customerEmail));
            }

            var newCart = new Cart
            {
                TotalCost = 0
            };

            await _cartUnitOfWork.CartRepository.AddAsync(newCart);
            await _cartUnitOfWork.SaveChangesAsync();

            await _customerCartUnitOfWork.CustomerCartRepository.AddAsync(new CustomerCart
            {
                CartId = newCart.Id,
                CustomerId = customer.Id
            });

            await _customerCartUnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCustomerCartItemsAsync(string customerEmail)
        {
            IEnumerable<CartItem> cartItems = new List<CartItem>();

            var customer = await _userService.GetAsync(customerEmail);

            if (customer is null)
            {
                throw new NotFoundException(nameof(User), nameof(customerEmail));
            }

            var customerCart = await _customerCartUnitOfWork.CustomerCartRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.CustomerId == customer.Id,
                x => x.Include(i => i.Cart));

            if (customerCart is not null)
            {
                cartItems = await _cartItemUnitOfWork.CartItemRepository.GetByCartIdAsync(customerCart.CartId);
            }

            return cartItems;
        }

        public async Task DeleteCartItemAsync(string customerEmail, int productId)
        {
            var customer = await _userService.GetAsync(customerEmail);

            if (customer is null)
            {
                throw new NotFoundException(nameof(User), nameof(customerEmail));
            }

            var product = await _productService.GetByIdAsync(productId);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), nameof(productId));
            }

            var customerCart = await _customerCartUnitOfWork.CustomerCartRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.CustomerId == customer.Id,
                x => x.Include(i => i.Cart));

            var existingCartItem = await _cartItemUnitOfWork.CartItemRepository.GetFirstOrDefaultAsync(
                x => x,
                x => x.ProductId == productId && x.CartId == customerCart.CartId,
                x => x.Include(i => i.Cart));

            if (existingCartItem is not null && existingCartItem.Quantity > 1)
            {
                existingCartItem.Quantity--;
                existingCartItem.Cost -= product.Price;
                existingCartItem.Cart.TotalCost -= product.Price;

                await _cartItemUnitOfWork.CartItemRepository.UpdateAsync(existingCartItem);
            }
            else
            {
                // If one item, remove and decrease totals
                customerCart.Cart.TotalCost -= product.Price;

                if (existingCartItem is not null)
                {
                    await _cartItemUnitOfWork.CartItemRepository.DeleteAsync(existingCartItem);
                }

                await _customerCartUnitOfWork.SaveChangesAsync();
            }

            await _cartItemUnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _cartUnitOfWork?.Dispose();
            _cartItemUnitOfWork?.Dispose();
        }
    }
}
