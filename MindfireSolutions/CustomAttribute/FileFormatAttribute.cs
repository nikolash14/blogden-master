using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace MindfireSolutions.CustomAttribute
{
    /// <summary>
    /// File-Format Attribute to check the Format of the Image By Data Annotations.
    /// </summary>
    public class FileFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (CheckImageType(value as HttpPostedFileBase))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Image Must Be In .jpg, .jpeg or .png Format");
            }
            return ValidationResult.Success;
        }
        protected bool CheckImageType(HttpPostedFileBase file)
        {
            return (file.ContentType.Equals("image/jpg") || file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/png"));
        }
    }
}