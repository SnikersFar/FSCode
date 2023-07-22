using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Extensions;
using Infrastructure.Contexts.Repositories;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<StuffContext>
            (
            db => db.UseSqlServer(builder.Configuration["Db:Connection"],
            b => b.MigrationsAssembly("Infrastructure"))
            );
            builder.Services.RegisterAutoRepositories(typeof(BaseRepository<>));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "registration",
                pattern: "Registration",
                defaults: new { controller = "Auth", action = "Registration" });

            app.MapControllerRoute(
                name: "authorization",
                pattern: "Authorization",
                defaults: new { controller = "Auth", action = "Authorization" });

            app.Run();
        }
    }
}