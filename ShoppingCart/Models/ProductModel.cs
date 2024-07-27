using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ShoppingCart.Models.Repository.Validation;

namespace ShoppingCart.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập Tên Sản phẩm")]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập mô tả Sản phẩm")]
        public string Decription { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập giá Sản phẩm")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal price { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một thương hiệu")]
        public int BrandId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một danh mục")]
        public int CategoryId { get; set; }

        public CategoryModel category { get; set; }
        public BrandModel brand { get; set; }

        public string Image { get; set; } 

        [NotMapped]
        [FileExtention(ErrorMessage = "Allowed extensions are .jpg, .png, .jpeg")]
        public IFormFile? ImageUpload { get; set; }
    }
}
