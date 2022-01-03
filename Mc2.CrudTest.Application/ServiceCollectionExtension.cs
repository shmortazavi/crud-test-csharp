using Mc2.CrudTest.Domain.SeedWorks;
using Mc2.CrudTest.Infrastructure.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddStartupConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IReadUnitOfWork, ReadUnitOfWorks>();
            services.AddTransient<IWriteUnitOfWork, WriteUnitOfWork>();
            return services;
        }

    }
}
