using Crudify.Commons.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Crudify.App
{
    public static class GlobalExceptionHandler
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var problemDetails = SetProblemDetails(exception);

                        if (string.IsNullOrWhiteSpace(problemDetails.Detail))
                            problemDetails.Detail = "Ocorreu um erro inesperado, tente novamente mais tarde";

                        await CreateResponse(context, problemDetails);
                        LogError(loggerFactory, context, exception, problemDetails);
                    }
                });
            });
        }

        private static async Task CreateResponse(HttpContext context, ProblemDetails problemDetails)
        {
            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }

        private static void LogError(ILoggerFactory loggerFactory, HttpContext context, Exception exception, ProblemDetails problemDetails)
        {
            if (exception is GenericException)
                return; 

            if (!Convert.ToInt32(HttpStatusCode.OK).Equals(problemDetails.Status.Value))
            {
                var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                var uri = context.Response.HttpContext.Request.Path.ToUriComponent().ToString();
                var method = context.Response.HttpContext.Request.Method;

                if (Convert.ToInt32(HttpStatusCode.BadRequest).Equals(problemDetails.Status.Value) ||
                    Convert.ToInt32(HttpStatusCode.NotFound).Equals(problemDetails.Status.Value))
                {
                    logger.LogWarning("Warning " + problemDetails.Status.Value + " handling " + method + " " + uri + ". " + exception.Message);
                }
                else
                {
                    logger.LogCritical("Critical " + problemDetails.Status.Value + " handling " + method + " " + uri + ". " + exception);
                }
            }
        }

        private static ProblemDetails SetProblemDetails(Exception exception)
        {
            var problemDetails = new ProblemDetails();
            switch (exception)
            {
                case ValidationException validationException:
                    problemDetails.Detail = validationException.Message;
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;
                case FormatException formatException:
                    problemDetails.Detail = formatException.Message;
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;
                case InvalidOperationException invalidOperationException:
                    problemDetails.Detail = invalidOperationException.Message;
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;
                case FluentValidation.ValidationException fluentValidationException:
                    problemDetails.Detail = string.Join(" | ", fluentValidationException.Errors.Select(x => x.ErrorMessage));
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return problemDetails;
        }
    }
}