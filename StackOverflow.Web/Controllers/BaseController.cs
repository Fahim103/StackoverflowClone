using Autofac;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Web.Controllers
{
    public class BaseController : Controller
    {

        protected IUnitOfWork UnitOfWork { get; set; }

        public BaseController()
        {
            UnitOfWork = Startup.AutofacContainer.Resolve<IUnitOfWork>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
                UnitOfWork.BeginTransaction();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction)
                UnitOfWork.Commit();
        }
    }
}