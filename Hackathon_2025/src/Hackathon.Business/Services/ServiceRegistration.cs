using Hackathon.Business.Services.Implementations;
using Hackathon.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.Business.Services;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }
}
