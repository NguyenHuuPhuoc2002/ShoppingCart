using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models.Repository;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
		private readonly DataContext _dataContext;

		public ProductController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public IActionResult Index()
        {
            return View();
        }
		public async Task<IActionResult> Detail(int Id)
		{
			if (Id == null)
			{
				return RedirectToAction("Index");
			}
			var productsById = _dataContext.Products.Where(c => c.Id == Id).FirstOrDefault();

			return View(productsById);
		}
	}
}
