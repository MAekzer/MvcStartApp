using Microsoft.EntityFrameworkCore;
using MvcStartApp.Middleware;
using MvcStartApp.Middleware.Repositories;
using MvcStartApp.Models.db;

namespace MvcStartApp
{
    public class Program
    {
        private static readonly IConfiguration configuration = BuildConfiguration();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionstring = configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionstring));
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<ILogRepository, LogRepository>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseMiddleware<LoggingMiddleware>();

            app.Run();
        }

        public static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}