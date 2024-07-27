using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models.Repository;

namespace ShoppingCart.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly DataContext _dataContext;

		public OrderController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IActionResult> Index()
		{

			return View(await _dataContext.Orders.OrderByDescending(p => p.Id).ToListAsync());

		}
        public async Task<IActionResult> ViewOrder(string orderCode)
        {
			var detailOrder = await _dataContext.OrderDetails.Include(o => o.Product).Where(o => o.OrderCode == orderCode).ToListAsync();
            return View(detailOrder);

        }
    }
}
