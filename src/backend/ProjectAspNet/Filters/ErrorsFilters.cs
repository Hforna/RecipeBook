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
            if (context.Exception is ProjectExceptionBase projectExceptionBase)
            {
                context.HttpContext.Response.StatusCode = (int)projectExceptionBase.GetStatusCode();
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new BadRequestObjectResult(new RegisterUserError("Unknown error"));
            }

        }
    }
}
