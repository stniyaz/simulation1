using Hackathon.Core.Interfaces;
using Hackathon.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.Data;

public static class RepositoryRegistration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        //services.AddScoped<IOrderRepository, OrderRepository>();
        //services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
