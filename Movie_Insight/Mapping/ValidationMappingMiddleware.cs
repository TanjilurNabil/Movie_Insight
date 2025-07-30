using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Movies.Contracts.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Mapping
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidationMappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.StatusCode = 400; // Bad Request
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = ex.Errors.Select(x => new ValidationResponse
                    {
                        PropertyName = x.PropertyName,
                        Message = x.ErrorMessage
                    })
                };
                await httpContext.Response.WriteAsJsonAsync(validationFailureResponse);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class ValidationMappingMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseValidationMappingMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ValidationMappingMiddleware>();
    //    }
    //}
}
