using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required (ErrorMessage = " Làm ơn nhập UserName")]
		public string UserName { get; set; }
		[Required(ErrorMessage = " Làm ơn nhập Email"), EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password), Required (ErrorMessage = "Làm ơn nhập mật khẩu")]
		public string Password { get; set; }
	}
}
