using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)//DbContextOptions in base ine veri yolluyoruz
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
