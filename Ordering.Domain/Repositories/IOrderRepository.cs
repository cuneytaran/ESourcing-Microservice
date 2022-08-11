using Ordering.Domain.Entities;
using Ordering.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>// Order ile ilgili işlem yapacak anlamında. Yani sadece Ordar a özgü olacak
    {
        Task<IEnumerable<Order>> GetOrdersBySellerUserName(string userName);
    }
}
