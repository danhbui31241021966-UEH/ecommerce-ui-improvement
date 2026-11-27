using System.ComponentModel.DataAnnotations;
using _31241021966_BuiCongDanh.Data;
using _31241021966_BuiCongDanh.Models;
namespace _31241021966_BuiCongDanh.Validation;
public class GenreAttribute : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext context) {
        var db = (ApplicationDbContext)context.GetService(typeof(ApplicationDbContext));
        if (!int.TryParse(value?.ToString(),out int id) || !db.Genres.Any(x=>x.ID==id)) return new ValidationResult("Thể loại không tồn tại.");
        if (context.ObjectInstance is Movie m && m.GenreID != id) return new ValidationResult("Mã và thể loại phải trùng nhau.");
        return ValidationResult.Success;
    }
}
