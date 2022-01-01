using eTickets.Data.Cart;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Services;
using eTickets.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        public OrdersController(IMovieService movieService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //return a list of shopping cart item
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> ListOrders()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _orderService.GetOrderByUserIdAndRole(userId, userRole);
            return View(orders);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            // Get the movies

            var item = await _movieService.GetByIdAsync(id);
            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            //Return back to index
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            // Get the movies

            var item = await _movieService.GetByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.DeleteItemFromCart(item);
            }
            //Return back to index
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string email = User.FindFirst(ClaimTypes.Email).Value; ;

            await _orderService.StoreOrder(items, userId, email);
            await _shoppingCart.ClearShoppingCart();
            return View("OrderCompleted");
        }
    }
}
