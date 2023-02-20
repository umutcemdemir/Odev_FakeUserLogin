using FakeUserLogin.DbOperations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FakeUserLogin.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<BookStoreDbContext>(options
                => options.UseInMemoryDatabase(databaseName: "BookStoreDb"));


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
