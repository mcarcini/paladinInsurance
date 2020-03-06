using Paladin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paladin.Infrastructure
{
    public class ExceptionLogginFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest") {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        Message = "An error has occured. Please try again later." 
                    }
                };
            }

            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.ExceptionHandled = true;

            var _context = DependencyResolver.Current.GetService<PaladinDbContext>();
            var error = new ErrorLog()
            {
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.Controller.GetType().Name,
                TargetedResult = filterContext.Result.GetType().Name,
                SessionID = (string)filterContext.HttpContext.Request["LoanId"],
                UserAgent = filterContext.HttpContext.Request.UserAgent,
                Timestamp = DateTime.Now
            };

            _context.Errors.Add(error);
            _context.SaveChanges();
            
        }
    }
}