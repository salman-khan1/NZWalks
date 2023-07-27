﻿using System.Net;

namespace NZWalksAPi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log this exception
                logger.LogError(ex,$"{errorId} : ", ex.Message);

                //Return A Custom Error Response
                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id=errorId,
                    Message="Something went wrong! we are looking into resolving it"
                };
                await httpContext.Response.WriteAsJsonAsync(error);

            }
        }
    }
}
