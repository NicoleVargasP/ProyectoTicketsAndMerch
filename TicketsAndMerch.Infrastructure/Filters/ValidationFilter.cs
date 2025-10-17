using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicketsAndMerch.Infrastructure.Validators; 

namespace TicketsAndMerch.Infrastructure.Filters  
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IValidationService _validationService;
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IValidationService validationService, IServiceProvider serviceProvider)
        {
            _validationService = validationService;
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue;

                var argumentType = argument.GetType();

                var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
                var validator = _serviceProvider.GetService(validatorType);

                if (validator == null) continue;

                try
                {
                    var method = typeof(IValidationService).GetMethod("ValidateAsync");
                    var genericMethod = method.MakeGenericMethod(argumentType);
                    var validationTask = (Task<ValidationResult>)genericMethod.Invoke(_validationService, new[] { argument });

                    var validationResult = await validationTask;

                    if (!validationResult.IsValid)
                    {
                        context.Result = new BadRequestObjectResult(new { Errors = validationResult.Errors });
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error durante la validación: {ex.Message}");
                }
            }

            await next();
        }
    }
}
