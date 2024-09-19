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
                    context.Result = new BadRequestObjectResult(new RegisterUserError(exception!.Errors));
                }

                if (context.Exception is LoginUserException)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Result = new UnauthorizedObjectResult(new RegisterUserError(context.Exception.Message));
                }
                if (context.Exception is  RegisterProductError)
                {
                    var exception = context.Exception as RegisterProductError;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new BadRequestObjectResult(new RegisterProductError(exception!.Errors));
                }
                if(context.Exception is FilterRecipeException)
                {
                    var exception = context.Exception as FilterRecipeException;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new BadRequestObjectResult(new FilterRecipeException(exception!.Errors));
                }
                if(context.Exception is CreateRecipeException)
                {
                    var exception = context.Exception as CreateRecipeException;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new BadRequestObjectResult(new CreateRecipeException(exception!.Errors));
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
