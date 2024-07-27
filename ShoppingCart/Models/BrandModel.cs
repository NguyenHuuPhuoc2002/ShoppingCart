using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Tên Thương Hiệu")]
		public string Name { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Mô tả Danh Mục")]
		public string Decription { get; set; }
		[Required]
		public string Slug { get; set; }

		public int Status { get; set; }
	}
}
