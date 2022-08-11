using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Repositories;
using Ordering.Domain.Repositories.Base;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection//startup.cs işlemini burada yapıyoruz.
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)//IConfiguration=program.cs ye ulaşmak için IServiceCollection=program.cs deki servilere ulaşmak için
        {
            //tüm configurasyonları ve servisleri bu metot altında toplayacaz. startup.cs için yazmıyoruz.

            //services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
            //                                    ServiceLifetime.Singleton,
            //                                    ServiceLifetime.Singleton);

            //MSSQL Server bilgisi tanımlanıyor
            services.AddDbContext<OrderContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("OrderConnection"),
                        //MigrationsAssembly=migration işlemi yapmak için kullanıyoruz
                        b => b.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)), ServiceLifetime.Singleton);

            //Add Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
