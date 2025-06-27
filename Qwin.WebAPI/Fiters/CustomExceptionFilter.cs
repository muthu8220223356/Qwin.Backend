using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Qwin.WebAPI.Fiters
{
    public class CustomExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //Log====>
            context.Result = new JsonResult(new { StatusCode = 500, Message = "Global exception error" });
            base.OnException(context);
        }
    }
}
