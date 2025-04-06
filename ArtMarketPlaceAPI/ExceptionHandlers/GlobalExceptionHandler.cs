using Microsoft.AspNetCore.Diagnostics;

namespace ArtMarketPlaceAPI.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ExceptionMessage = exception.Message,
                Title = "The server was unable to process your request."
            };

            await httpContext.Response.WriteAsJsonAsync(response,cancellationToken);
            return true;
        }
    }
}
