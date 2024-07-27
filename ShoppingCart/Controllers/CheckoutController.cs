using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Models.Repository;
using System.Security.Claims;

namespace ShoppingCart.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly DataContext _dataContext;

		public CheckoutController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email); // tim ra email
			if (userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				//Tao ma don hang ngau nhien
				var orderCode = Guid.NewGuid().ToString();

				//gan gia tri cho don hang
				var orderItem = new OrderModel();
				orderItem.OrderCode = orderCode;
				orderItem.UserName = userEmail;
				orderItem.Status = 1;
				orderItem.CreatedDate = DateTime.Now;

				_dataContext.Add(orderItem);
				_dataContext.SaveChanges();

				//Them chi tiet don hang
				var carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach (var cart in carts)
				{
					var orderDetails = new OrderDetail();
					orderDetails.UserName = userEmail;
					orderDetails.OrderCode = orderCode;
					orderDetails.ProductId = (int)cart.ProductId;
					orderDetails.Price = cart.Price;
					orderDetails.Quantity = cart.Quantity;


					_dataContext.Add(orderDetails);
					_dataContext.SaveChanges();
				}
				TempData["Success"] = "Checkout thành công!";
				HttpContext.Session.Remove("Cart");
				return RedirectToAction("Index", "Cart");

			}
		}
	}
}
