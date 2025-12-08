using CloudinaryDotNet;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.SharedKernel.exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNet_StoreManagement.SharedKernel.filters;

// public class ValidationFilter : IActionFilter
// {
//     private readonly ILogger<ValidationFilter> _logger;
//
//     public ValidationFilter(ILogger<ValidationFilter> logger)
//     {
//         _logger = logger;
//     }
//     
//     public void OnActionExecuting(ActionExecutingContext context)
//     {
//         if (!context.ModelState.IsValid)
//         {
//             var errors = context.ModelState
//                 .Where(x => x.Value?.Errors.Count > 0)
//                 .ToDictionary(
//                     kvp => kvp.Key,
//                     kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
//                 );
//
//             context.Result = new BadRequestObjectResult(new
//             {
//                 Message = "Validation failed",
//                 Errors = errors
//             });
//         }
//     }
//
//     public void OnActionExecuted(ActionExecutedContext context)
//     {
//     }
// }