using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Models.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				BrandModel apple = new BrandModel { Name = "Apple", Slug = "apple", Decription = "Aple is Large Brand in the world", Status =  1};
				BrandModel samsung = new BrandModel { Name = "SamSung", Slug = "samsung", Decription = "SamSung is Large Brand in the world", Status =  1};
				
				CategoryModel macbook = new CategoryModel { Name = "Macbook", Slug = "Macbook", Decription = "Macbook is Large Brand in the world", Status = 1 };
				CategoryModel pc = new CategoryModel { Name = "PC", Slug = "PC", Decription = "PC is Large Brand in the world", Status = 1 };
				_context.Products.AddRange(
					new ProductModel { Name = "Macbook", Slug = "macbook", Decription = "Macbook is Best", Image = "1.img", category = macbook, brand = apple, price = 1300 },
					new ProductModel { Name = "Pc", Slug = "pc", Decription = "Pc is Best", Image = "1.img", category = pc, brand = samsung, price = 1300 }
				);
				_context.SaveChanges();
			}
		}
	}
}
