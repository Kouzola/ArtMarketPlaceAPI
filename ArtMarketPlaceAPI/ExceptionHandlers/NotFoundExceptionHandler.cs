using Business_Layer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.ExceptionHandlers
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            if (exception is not NotFoundException) return false;

            var response = new
            {
                StatusCode = StatusCodes.Status404NotFound,
                ExceptionMessage = exception.Message,
                Title = "Ressource not found."
            };


            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
