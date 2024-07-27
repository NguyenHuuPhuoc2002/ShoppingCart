using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Models.Repository;

namespace ShoppingCart
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// Session
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.IsEssential = true;
			});

			
			builder.Services.AddDbContext<DataContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectedDb")));

            // Configure Identity
            builder.Services.AddIdentity<AppUserModel, IdentityRole>()
				.AddEntityFrameworkStores<DataContext>()
				.AddDefaultTokenProviders();

			builder.Services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 4;

				options.User.RequireUniqueEmail = true;
			});

			var app = builder.Build();

			// Error page
			app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");

			app.UseSession();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "Areas",
				pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

			// Set up route URL
			app.MapControllerRoute(
				name: "category",
				pattern: "/category/{Slug?}",
				defaults: new { controller = "Category", action = "Index" });

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			// Seed Data
			var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
			SeedData.SeedingData(context);

			app.Run();
		}
	}
}
