using System.ComponentModel.DataAnnotations;
namespace _31241021966_BuiCongDanh.Validation;
public class CheckDateGreaterThanTodayAttribute : ValidationAttribute {
    protected override ValidationResult IsValid(object value, ValidationContext context) =>
      value is DateTime dt && dt.Date >= DateTime.Today ? ValidationResult.Success : new ValidationResult("Ngày phát hành không được trước ngày hôm nay.");
}
