using Autofac;
using Autofac.Extensions.DependencyInjection;
using Mc2.CrudTest.Application;
using Mc2.CrudTest.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Mc2.CrudTest.Api
{
    public class Startup
    {
        readonly string Policy = "AllowAll";
        private const string DefaultConnection = "DefaultConnection";
        private const string DataAccessNameSpace = "Mc2.CrudTest.Infrastructure";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options
                           .AddPolicy(Policy, p => p.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mc2.CrudTest.Api", Version = "v1" });
            });

            services.AddDbContext<Mc2DbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString(DefaultConnection),
                m => m.MigrationsAssembly(DataAccessNameSpace))
                      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddStartupConfiguration(Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Mc2DbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mc2.CrudTest.Api v1"));
            }

            context.Database.Migrate();

            app.UseCors(Policy);

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
