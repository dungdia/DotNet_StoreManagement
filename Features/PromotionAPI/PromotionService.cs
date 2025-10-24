using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.PromotionAPI.dtos;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Net;

namespace DotNet_StoreManagement.Features.PromotionAPI
{
    [Service]
    public class PromotionService
    {
        private readonly PromotionRepository promotionRepo;

        public PromotionService(PromotionRepository repo)
        {
            promotionRepo = repo;
        }

        public async Task<ICollection<Promotion>> getAllPromotions()
        {
            var promotions = await promotionRepo.GetAllAsync();
            return promotions;
        }

        public async Task<Promotion?> getPromotionById(int id)
        {
            var promotion = await promotionRepo.GetByIdAsync(id);
            return promotion;
        }

        public async Task<int> addPromotion(PromotionDTO addPromotionDto)
        {
            var promotion = new Promotion
            {
                PromoCode = addPromotionDto.PromoCode,
                Description = addPromotionDto.Description,
                DiscountType = addPromotionDto.DiscountType,
                DiscountValue = addPromotionDto.DiscountValue,
                StartDate = addPromotionDto.StartDate,
                EndDate = addPromotionDto.EndDate,
                MinOrderAmount = addPromotionDto.MinOrderAmount,
                UsageLimit = addPromotionDto.UsageLimit,
                UsedCount = addPromotionDto.UsedCount,
                Status = addPromotionDto.Status
            };

            try
            {
                var result = await promotionRepo.AddAndSaveAsync(promotion);
                return result;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062) // 1062 = duplicate entry
                {
                    throw new APIException(HttpStatusCode.BadRequest.value(), "PromoCode already exists");
                }

                throw; // nếu không phải duplicate thì ném lỗi bình thường
            }
        }

        public async Task<int> updatePromotion(int id, PromotionDTO updatePromotionDto)
        {
            var existingPromotion = await promotionRepo.GetByIdAsync(id);

            if (existingPromotion == null)
            {
                throw new APIException(HttpStatusCode.NotFound.value(), "Promotion not found");
            }

            // Check duplicate PromoCode except current promotion
            var isDuplicate = await promotionRepo.ExistsAsync(p =>
                    p.PromoCode == updatePromotionDto.PromoCode && p.PromoId != id);

            if (isDuplicate)
            {
                throw new APIException(HttpStatusCode.BadRequest.value(), "PromoCode already exists");
            }

            existingPromotion.PromoCode = updatePromotionDto.PromoCode;
            existingPromotion.Description = updatePromotionDto.Description;
            existingPromotion.DiscountType = updatePromotionDto.DiscountType;
            existingPromotion.DiscountValue = updatePromotionDto.DiscountValue;
            existingPromotion.StartDate = updatePromotionDto.StartDate;
            existingPromotion.EndDate = updatePromotionDto.EndDate;
            existingPromotion.MinOrderAmount = updatePromotionDto.MinOrderAmount;
            existingPromotion.UsageLimit = updatePromotionDto.UsageLimit;
            existingPromotion.UsedCount = updatePromotionDto.UsedCount;
            existingPromotion.Status = updatePromotionDto.Status;

            var result = await promotionRepo.UpdateAndSaveAsync(existingPromotion);
            return result;
        }

        public async Task<int> deletePromotion(int id)
        {
            var existingPromotion = await promotionRepo.GetByIdAsync(id);

            if (existingPromotion == null)
            {
                throw new APIException(HttpStatusCode.NotFound.value(), "Promotion not found");
            }



            var result = await promotionRepo.DeleteAndSaveAsync(existingPromotion);
            return result;
        }
    }
}
