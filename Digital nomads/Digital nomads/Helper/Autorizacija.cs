using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Digital_nomads.Data;
using Digital_nomads.ViewModels;

namespace Digital_nomads.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool radnik, bool admin,bool dostavljac,bool kupac)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { radnik, admin,dostavljac,kupac };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool radnik, bool administrator)
        {
            _radnik = radnik; _administrator = administrator;
        }
        private readonly bool _radnik;
        private readonly bool _administrator;

        

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            MojDbContext db = filterContext.HttpContext.RequestServices.GetService<MojDbContext>();
            LogiraniKorisnikSesija l = filterContext.HttpContext.GetLogiraniKorisnik();
            if (l==null)
            {
                filterContext.Result = new RedirectToActionResult("Nonauthorized", "Home",new { area = "" });
                return;
            }
            bool isAdmin;
            if (l.LoginRola.OpisRole == "Administrator")
                isAdmin = true;
            else isAdmin = false;
            if(_radnik && isAdmin==false)
            {
                await next();
                return;
            }
            if (_administrator && isAdmin==true)
            {
                await next();
                return;
            }
            filterContext.Result = new RedirectToActionResult("Nonauthorized", "Home", new { area = "" });
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}
