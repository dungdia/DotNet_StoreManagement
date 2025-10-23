using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.PromotionAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNet_StoreManagement.Features.PromotionAPI
{
    [ApiController]
    [Route("api/v1/promotions")]
    public class PromotionController : ControllerBase
    {
        private readonly PromotionService _service;

        public PromotionController(PromotionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> getAllPromotions()
        {
            var promotions = await _service.getAllPromotions();

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Get promotions successfully",
                promotions
            );

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getPromotionById(int id)
        {
            var promotion = await _service.getPromotionById(id);

            if (promotion == null)
            {
                return NotFound(new APIResponse<Object>(
                    HttpStatusCode.NotFound.value(),
                    "Promotion not found",
                    promotion
                ));
            }

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Get promotion successfully",
                promotion
            );

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> addPromotion([FromBody] PromotionDTO addPromotionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.addPromotion(addPromotionDto);

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Add promotion successfully",
                result
            );

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> editPromotion(int id, [FromBody] PromotionDTO promotion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.updatePromotion(id, promotion);
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Edit promotion successfully",
                result
            );

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deletePromotion(int id)
        {
            var result = await _service.deletePromotion(id);

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Delete promotion successfully",
                result
            );

            return Ok(response);
        }
    }
}
