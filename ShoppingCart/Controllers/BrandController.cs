using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Models.Repository;

namespace ShoppingCart.Controllers
{
    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;

        public BrandController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index(string Slug = "")
        {
            BrandModel category = _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();
            if (category == null) {
                return RedirectToAction("Index");
            }
            var productsByCategory = _dataContext.Products.Where(c => c.CategoryId == category.Id);
            return View(await productsByCategory.OrderByDescending(c => c.Id).ToListAsync());
        }
    }
}
