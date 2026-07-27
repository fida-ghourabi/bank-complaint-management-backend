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


            if (!context.ModelState.IsValid)
            {

                var errors =
                    context.ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value!.Errors
                            .Select(e => e.ErrorMessage)
                            .ToArray()
                    );



                var response =
                    new ErrorResponse
                    {
                        Success = false,

                        StatusCode = 400,

                        Message = "Validation failed.",

                        Errors = errors,

                        Timestamp = DateTime.UtcNow
                    };



                context.Result =
                    new BadRequestObjectResult(response);


                return;
            }



            await next();

        }

    }
}