using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models.ViewModel
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = " Làm ơn nhập UserName")]
		public string UserName { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Làm ơn nhập mật khẩu")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
