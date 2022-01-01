using eTickets.Migrations;
using System.Collections.Generic;
using System.Threading.Tasks;
using eTickets.Models;
namespace eTickets.Services.IServices
{
    public interface IOrderService
    {

        Task StoreOrder(List<ShoppingCartItem> items, string UserId, string userEmail);

        Task <List<Order>> GetOrderByUserIdAndRole(string userId, string userRole);
    }
}
