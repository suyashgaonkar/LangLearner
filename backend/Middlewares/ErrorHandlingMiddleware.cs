


using LangLearner.Exceptions;
using LangLearner.Models.Dtos.Responses;
using System.Text.Json;

namespace LangLearner.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public ErrorHandlingMiddleware()
        {
            
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (APIValidationException e)
            {
                Console.WriteLine("aaaaaaaaaaaa");
                context.Response.StatusCode = e.StatusCode;
                context.Response.ContentType = "application/json";
                var errorResponse = new ApiValidationError { ErrorMessage = e.Message, StatusCode = e.StatusCode, Errors=e.Errors };
                var errorJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(errorJson);
            }
            catch (GeneralAPIException e)
            {
                context.Response.StatusCode = e.StatusCode;
                context.Response.ContentType = "application/json";
                var errorResponse = new ApiError { ErrorMessage = e.Message, StatusCode = e.StatusCode };
                var errorJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(errorJson);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message ?? "Something went wrong. Please try again later");
            }
        }
    }
}
