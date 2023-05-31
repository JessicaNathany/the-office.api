using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using the_office.api.application.Common.Validations;
using the_office.domain.Enums;
using the_office.domain.Errors;
using the_office.domain.Shared;

namespace the_office.api.Filters;

public class AfterHandlerActionFilterAttribute : ActionFilterAttribute
{
    private readonly IDictionary<ErrorType, Action<ActionExecutedContext, Result>> _failureHandlers;

    public AfterHandlerActionFilterAttribute()
    {
        _failureHandlers = new Dictionary<ErrorType, Action<ActionExecutedContext, Result>>()
        {
            {ErrorType.ValidationError, HandlerValidationError},
            {ErrorType.ResourceNotFound, HandlerNotFound}
        };
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        HandleResult(context);

        base.OnActionExecuted(context);
    }

    private void HandleResult(ActionExecutedContext context)
    {
        if (context.Result is not ObjectResult objectResult)
            return;

        if (objectResult.Value is not Result result)
            return;

        if (result.IsFailure)
        {
            HandleFailure(context, result);
        }
        else
        {
            var isGenericType = objectResult.Value.GetType().IsGenericType &&
                                objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(Result<>);

            if (isGenericType)
            {
                dynamic dynamicResult = objectResult.Value;
                HandleGenericResult(context, dynamicResult);
            }
            else
            {
                context.Result = new OkResult();
            }
        }
    }

    private static void HandleGenericResult<TResult>(ActionExecutedContext context, Result<TResult> result)
    {
        context.Result = context.HttpContext.Request.Method == HttpMethod.Post.Method
            ? new CreatedResult(string.Empty, result.Value)
            : new OkObjectResult(result.Value);
    }

    private void HandleFailure(ActionExecutedContext context, Result result)
    {
        if (Enum.TryParse(result.Error.Code, out ErrorType errorType))
        {
            if (_failureHandlers.TryGetValue(errorType, out var handler))
            {
                handler.Invoke(context, result);
                return;
            }
        }

        context.Result = new BadRequestObjectResult(result.Error);
    }

    private static void HandlerValidationError(ActionExecutedContext context, Result result)
    {
        var details = result switch
        {
            IValidationResult validationResult => CreateValidationProblemDetails(result.Error, validationResult.Errors),
            _ => CreateValidationProblemDetails(result.Error)
        };

        context.Result = new BadRequestObjectResult(details);
    }

    private static void HandlerNotFound(ActionExecutedContext context, Result result)
    {
        var details = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = result.Error.Message
        };

        context.Result = new NotFoundObjectResult(details);
    }

    private static ProblemDetails CreateValidationProblemDetails(Error error, Error[]? errors = null)
    {
        return new ProblemDetails
        {
            Title = "Validation Error",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = error.Message,
            Status = StatusCodes.Status400BadRequest,
            Extensions = {{nameof(errors), errors}}
        };
    }
}