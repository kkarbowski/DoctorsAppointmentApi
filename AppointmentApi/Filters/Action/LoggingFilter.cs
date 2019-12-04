using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.Text;
using Newtonsoft.Json;

namespace AppointmentApi.Filters.Action
{
    public class LoggingFilter : IActionFilter
    {
        public LoggingFilter() { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            StringBuilder logMessage = new StringBuilder($"Executing " +
                $"{context.HttpContext.Request.Method} with Path " +
                $"{context.HttpContext.Request.Path}");
            if (context.ActionArguments.Count > 0)
            {
                logMessage.Append(", content: ");
                foreach (var argument in context.ActionArguments)
                {
                    logMessage.Append($"{argument.Key}={JsonConvert.SerializeObject(argument.Value)}, ");
                }
                logMessage.Length -= 2;
            }
            logMessage.Append(".");
            Log.Debug(logMessage.ToString());
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            var responseStatusCode = context.HttpContext.Response.StatusCode;
            String executedMessage = $"Finished execution of {context.HttpContext.Request.Method} " +
                $"{context.HttpContext.Request.Path}, " +
                $"response status code = {responseStatusCode}";

            if (responseStatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK ||
                responseStatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status201Created)
            {
                Log.Debug(executedMessage);
            }
            else
            {
                Log.Error(executedMessage);
            }
        }
    }
}
