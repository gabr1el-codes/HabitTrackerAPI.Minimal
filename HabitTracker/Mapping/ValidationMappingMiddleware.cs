using FluentValidation;
using Habits.Contracts.Responses;

namespace Movies.Api.Mapping;

public class ValidationMappingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationMappingMiddleware> _logger;

    public ValidationMappingMiddleware(RequestDelegate next, ILogger<ValidationMappingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Call the next middleware in the pipeline
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Validation failed for the request.");

            context.Response.StatusCode = 400;
            var validationFailureResponse = new ValidationFailureResponse
            {
                Errors = ex.Errors.Select(x => new ValidationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                })
            };

            await context.Response.WriteAsJsonAsync(validationFailureResponse); // Write the response as JSON
        }
    }
}
