using AccountingNotebook.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AccountingNotebook.API
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
		public void OnException(ExceptionContext context)
		{
			if (context == null)
			{
				return;
			}

			var statusCode = HttpStatusCode.InternalServerError;

			var ex = context.Exception;
			var message = ex.Message;

			if (ex is ResourceBusyException)
			{
				statusCode = HttpStatusCode.ServiceUnavailable;
				message = "Service is currently busy, please try again later";
			}

			var response = context.HttpContext.Response;
			response.StatusCode = (int)statusCode;
			response.ContentType = "application/json";

			response.WriteAsync(message);
		}
	}
}
