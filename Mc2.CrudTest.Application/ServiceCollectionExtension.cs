using Mc2.CrudTest.Domain.SeedWorks;
using Mc2.CrudTest.Infrastructure.UnitOfWorks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddStartupConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IReadUnitOfWork, ReadUnitOfWorks>();
            services.AddScoped<IWriteUnitOfWork, WriteUnitOfWork>();
            services.AddMediatR(typeof(ServiceCollectionExtension).Assembly);

            return services;
        }

    }
}
