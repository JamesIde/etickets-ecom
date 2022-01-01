using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {

        private readonly ApplicationDbContext _db;

        public ShoppingCart(ApplicationDbContext db)
        {
            _db = db;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var context = service.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }


        public void AddItemToCart(Movie movie)
        {
            //First check if movie is already in the account
            //Else add to the cart and set amount to 1
            //Check if the cart has something in it

            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                //Then create a new shopping cart item
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _db.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _db.SaveChanges();
        }

        public void DeleteItemFromCart(Movie movie)
        {
            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _db.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _db.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _db.ShoppingCartItems.Where(n=>n.ShoppingCartId == ShoppingCartId).Include(n=>n.Movie).ToList());    
        }

        public double GetShoppingCartTotal()
        {
            var total = _db.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n=>n.Movie.Price * n.Amount).Sum();
            return total;
        }

        public async Task ClearShoppingCart()
        {
            var items = await  _db.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToListAsync();
            _db.ShoppingCartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }
    }
}
