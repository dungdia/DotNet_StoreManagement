using System.ComponentModel.DataAnnotations;

namespace DotNet_StoreManagement.Features.PromotionAPI.utils
{
    public class DiscountValueValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dto = (DotNet_StoreManagement.Features.PromotionAPI.dtos.PromotionDTO)validationContext.ObjectInstance;

            var discountType = dto.DiscountType?.ToLower();
            var discountValue = Convert.ToDecimal(value ?? 0);

            if (discountType == "fixed")
            {
                if (discountValue < 0)
                {
                    return new ValidationResult("DiscountValue must be >= 0 for fixed type.");
                }
            }
            else if (discountType == "percent")
            {
                // percent must be 0–100 and integer
                if (discountValue < 0 || discountValue > 100)
                {
                    return new ValidationResult("DiscountValue must be between 0 and 100 for percent type.");
                }
                if (discountValue % 1 != 0)
                {
                    return new ValidationResult("DiscountValue must be an integer when DiscountType is percent.");
                }
            }
            else
            {
                return new ValidationResult("Invalid DiscountType. Must be 'fixed' or 'percent'.");
            }

            return ValidationResult.Success;
        }
    }

    public class DateRangeValidationAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateRangeValidationAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var endDate = value as DateOnly?;
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);

            if (startDateProperty == null)
                return new ValidationResult($"Unknown property: {_startDatePropertyName}");

            var startDate = startDateProperty.GetValue(validationContext.ObjectInstance) as DateOnly?;

            if (startDate == null || endDate == null)
                return ValidationResult.Success; // [Required] sẽ xử lý null riêng

            if (endDate <= startDate)
                return new ValidationResult("EndDate must be after StartDate.");

            return ValidationResult.Success;
        }
    }
}
