using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository// iki tane repository implemantasyonu
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)//Repository de constructor dbContex bekliyor. bu yüzden burdan tanımlama yapıyoruz. base ise Repository e git anlamında
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersBySellerUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                      .Where(o => o.SellerUserName == userName)
                      .ToListAsync();

            return orderList;
        }
    }
}
