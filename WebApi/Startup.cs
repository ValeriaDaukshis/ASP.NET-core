using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiCommon.DataModel;
using WebApiCommon.Implementations.Repositories;
using WebApiCommon.Implementations.Services;
using WebApiCommon.Interfaces;
using WebApiCommon.Interfaces.Repositories;
using WebApiCommon.Interfaces.Services;
using WebApiCommon.Middleware;
using WebApiCommonn.DataModel;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IScreenRepository, ScreenRepository>();
            services.AddScoped<IScreenService, ScreenService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IHiveRepository, HiveRepository>();
            services.AddScoped<IHiveService, HiveService>();

            services.AddScoped<ICaching<Screen>, Caching<Screen>>();
            services.AddScoped<ICaching<Category>, Caching<Category>>();
            services.AddScoped<ICaching<Hive>, Caching<Hive>>();

            services.AddMemoryCache();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<DbRequestMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
