using DotNet_StoreManagement.Features.PromotionAPI.utils;
using System.ComponentModel.DataAnnotations;

namespace DotNet_StoreManagement.Features.PromotionAPI.dtos
{
    public class PromotionDTO
    {
        [Required(ErrorMessage = "PromoCode is required")]
        [StringLength(50, ErrorMessage = "PromoCode cannot exceed 50 characters")]
        public string PromoCode { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "DiscountType is required")]
        [RegularExpression("^(percent|fixed)$", ErrorMessage = "DiscountType must be 'percent' or 'fixed'")]
        public string DiscountType { get; set; } = null!;

        [DiscountValueValidation]
        public decimal DiscountValue { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        [DateRangeValidation(nameof(StartDate))]
        public DateOnly EndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MinOrderAmount { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int UsageLimit { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int UsedCount { get; set; } = 0;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required")]
        [RegularExpression("^(active|inactive)$", ErrorMessage = "Status must be 'active' or 'inactive'")]
        public string Status { get; set; } = "active";
    }
}
