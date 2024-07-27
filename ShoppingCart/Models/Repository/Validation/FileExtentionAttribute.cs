using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace ShoppingCart.Models.Repository.Validation
{
    public class FileExtentionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string[] extensions = { ".jpg", ".png", ".jpeg" };
                bool result = extensions.Any(x => x == extension);

                if (!result)
                {
                    return new ValidationResult("Allowed extensions are .jpg, .png, .jpeg");
                }
            }
            return ValidationResult.Success;
        }
    }
}
