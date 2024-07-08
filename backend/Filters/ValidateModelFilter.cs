using LangLearner.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace LangLearner.Filters
{
#pragma warning disable CS8602
    public class ValidateModelFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context) 
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                throw new APIValidationException("Some fields are missing or invalid!", errors);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }

    }
#pragma warning restore CS8602
}

