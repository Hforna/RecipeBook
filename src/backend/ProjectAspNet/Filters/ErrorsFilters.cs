using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Exceptions.Exceptions;
using System;

namespace ProjectAspNet_API.Filters
{
    public class FilterExceptions : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ProjectExceptionBase)
            {
                if (context.Exception is RegisterUserError)
                {
                    var exception = context.Exception as RegisterUserError;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new BadRequestObjectResult(new RegisterUserError(exception.Errors));
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new BadRequestObjectResult(new RegisterUserError("Unknown error"));
            }

        }
    }
}
