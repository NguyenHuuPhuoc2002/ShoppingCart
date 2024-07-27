using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Models.Repository;
using ShoppingCart.Models.ViewModel;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;

        public CartController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartItemViewModel = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };
            return View(cartItemViewModel);
        }
		public IActionResult Checkout()
		{
			return View("Index");
		}

		public async Task<IActionResult> Add(int Id)
		{
            var product = await _dataContext.Products.FindAsync(Id);
			var carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            var cartItem = carts.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem == null)
            {
                carts.Add(new CartItemModel(product));
            }
            else
            {
				cartItem.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", carts);
            TempData["Success"] = "Add Item to Cart Successfully";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Increase(int Id)
		{
            var carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            var cartItem = carts.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem != null) { 
                ++cartItem.Quantity;
            }
            HttpContext.Session.SetJson("Cart", carts);
            TempData["Success"] = "Increase Item Quantity to Cart Successfully";
            return RedirectToAction("Index");
		}
		public async Task<IActionResult> Decrease(int Id)
		{
			var carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ;
            var cartItem =  carts.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                carts.Remove(cartItem);
            }
            if (carts.Count == 0) {
				HttpContext.Session.Remove("Cart");
			}
            else
            {
				HttpContext.Session.SetJson("Cart", carts);
			}
            TempData["Success"] = "Decrease Item Quantity to Cart Successfully";
            return RedirectToAction("Index");
		}
        public async Task<IActionResult> Remove(int Id)
        {
            var carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            var cartItem = carts.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem != null) { 
                carts.Remove(cartItem) ;
            }
            if (carts.Count == 0) HttpContext.Session.Remove("Cart");
            else HttpContext.Session.SetJson("Cart", carts) ;
            TempData["Success"] = "Remove Item of Cart Successfully";
            return RedirectToAction("Index");
        }
	}
}
