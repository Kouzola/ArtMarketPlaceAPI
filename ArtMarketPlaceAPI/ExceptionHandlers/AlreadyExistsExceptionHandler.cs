using Business_Layer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ArtMarketPlaceAPI.ExceptionHandlers
{
    public class AlreadyExistsExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not AlreadyExistException) return false;

            var response = new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ExceptionMessage = exception.Message,
                Title = "Bad request."
            };


            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
