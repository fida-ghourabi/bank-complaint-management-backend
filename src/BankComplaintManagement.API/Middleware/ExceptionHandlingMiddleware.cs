using BankComplaintManagement.API.Responses;
using BankComplaintManagement.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace BankComplaintManagement.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;



        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }





        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(
                    context,
                    exception);
            }
        }








        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {


            // Si la réponse HTTP a déjà commencé,
            // on ne peut plus la modifier

            if (context.Response.HasStarted)
            {
                _logger.LogWarning(
                    "La réponse HTTP a déjà commencé.");

                throw exception;
            }





            int statusCode;

            string message;




            // ================================
            // Exceptions métiers
            // ================================

            if (exception is BaseException baseException)
            {

                statusCode =
                    baseException.StatusCode;


                message =
                    baseException.Message;

            }

            else
            {

                // ================================
                // Erreur inconnue
                // ================================


                statusCode =
                    (int)HttpStatusCode.InternalServerError;


                message =
                    "Une erreur interne est survenue.";



                _logger.LogError(
                    exception,
                    "Erreur non gérée");

            }





            context.Response.StatusCode =
                statusCode;


            context.Response.ContentType =
                "application/json";





            var response =
                new ErrorResponse
                {
                    Success = false,

                    StatusCode = statusCode,

                    Message = message,

                    Timestamp = DateTime.UtcNow
                };







            var options =
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy =
                        JsonNamingPolicy.CamelCase
                };






            var json =
                JsonSerializer.Serialize(
                    response,
                    options);






            await context.Response
                .WriteAsync(json);

        }

    }
}