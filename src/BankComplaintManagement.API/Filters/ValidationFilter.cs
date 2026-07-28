using BankComplaintManagement.API.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankComplaintManagement.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
                return;
            }

            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value!.Errors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                );

            var response = new ErrorResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation failed.",
                Errors = errors,
                Timestamp = DateTime.UtcNow
            };

            context.Result = new BadRequestObjectResult(response);
        }
    }
}