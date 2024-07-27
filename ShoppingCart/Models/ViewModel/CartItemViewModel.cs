namespace ShoppingCart.Models.ViewModel
{
	public class CartItemViewModel
	{
		public List<CartItemModel> CartItems;
		public decimal? GrandTotal { get; set; }
	}
}
