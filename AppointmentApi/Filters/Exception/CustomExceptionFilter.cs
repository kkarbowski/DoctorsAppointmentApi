using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using AppointmentApi.Exceptions;
using System.Text;

namespace AppointmentApi.Filters.Exception
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is EntityNotFoundException)
            {
                Log.Error(context.Exception.Message);
            }
            else
            {
                string exceptionMessage = $"An exception has occurred, message: {context.Exception.Message}, stack trace:\n {context.Exception.StackTrace}";
                Log.Error(exceptionMessage);
            }

            context.ExceptionHandled = true;
        }
    }
}
