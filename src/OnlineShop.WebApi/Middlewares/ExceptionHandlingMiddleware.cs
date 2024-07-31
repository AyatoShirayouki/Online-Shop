using System.Net;

namespace OnlineShop.WebApi.Middlewares
{
	/// <summary>
	/// Middleware for handling exceptions and returning a custom error response.
	/// </summary>
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;


		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next middleware in the pipeline.</param>
		/// <param name="logger">The logger instance.</param>
		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next ?? throw new ArgumentNullException(nameof(next));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}


		/// <summary>
		/// Invokes the middleware to handle the request and catch any exceptions.
		/// </summary>
		/// <param name="httpContext">The HTTP context.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		/// <summary>
		/// Handles the exception by returning a custom error response.
		/// </summary>
		/// <param name="context">The HTTP context.</param>
		/// <param name="exception">The exception that occurred.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			return context.Response.WriteAsync(new ErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				Message = "Internal Server Error from the custom middleware: " + exception.Message
			}.ToString());
		}
	}
}
