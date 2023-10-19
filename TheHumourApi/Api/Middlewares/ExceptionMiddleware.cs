
using System.Text.Json;
using Application.Models;
using Infrastructure.Exceptions;

namespace Api.Middlewares;
public class ExceptionMiddleware : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            string errorId = Guid.NewGuid().ToString();
            var errorResult = new ErrorResult()
            {
                Source = ex.TargetSite.DeclaringType.FullName,
                Exception = ex.Message,
                ErrorId = errorId
            };
            errorResult.ErrorMessages.Add(ex.Message);

            if (ex is CustomException customException)
            {
                errorResult.StatusCode = (int)customException.StatusCode;
            }

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = errorResult.StatusCode;
            await response.WriteAsync(JsonSerializer.Serialize(errorResult));
        }
    }
}