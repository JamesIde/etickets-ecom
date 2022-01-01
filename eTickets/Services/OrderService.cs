using eTickets.Data;
using eTickets.Models;
using eTickets.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;


        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<List<Order>> GetOrderByUserIdAndRole(string userId, string userRole)
        {
            var orders = await _db.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n=>n.User).ToListAsync(); 
         
            if(userRole != "Admin")
            {
                orders = orders.Where(n=>n.UserId == userId).ToList();
            }
            
            return orders;
        }

        public async Task StoreOrder(List<ShoppingCartItem> items, string UserId, string userEmail)
        {

            var order = new Order()
            {
                UserId = UserId,
                Email = userEmail
            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();


            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _db.OrderItems.AddAsync(orderItem); 
            }
            await _db.SaveChangesAsync();
        }
    }
}
