using Business_Layer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ArtMarketPlaceAPI.ExceptionHandlers
{
    public class BusinessExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BusinessException) return false;

            var response = new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ExceptionMessage = exception.Message,
                Title = "Invalid operation."
            };


            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
