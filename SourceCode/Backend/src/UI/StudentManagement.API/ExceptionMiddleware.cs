namespace StudentManagement.UI.API
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

                //NOTE: Log inner exception if any
                if (ex.InnerException != null)
                {
                    logger.LogError(ex.InnerException, "Inner Exception: {Message}", ex.InnerException.Message);
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                DetailedMessage = exception.Message,
                StackTrace = exception.StackTrace //TODO: Consider removing in production
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}