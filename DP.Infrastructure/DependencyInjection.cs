using DP.Application.Interfaces;
using DP.Infrastructure.Persistence;
using DP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DP.Application.Interfaces;
using DP.Infrastructure.Services;
using StackExchange.Redis;

namespace DP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        var redisConnection = configuration["Redis:Connection"];
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection!));

        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}