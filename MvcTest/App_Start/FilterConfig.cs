using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());//ExceptionFilter
            //filters.Add(new AuthorizeAttribute());
            //filters.Add(new HandleErrorAttribute()
            //{
            //    ExceptionType = typeof(DivideByZeroException),
            //    View = "DivideError"
            //});
            //filters.Add(new HandleErrorAttribute()
            //{
            //    ExceptionType = typeof(NotFiniteNumberException),
            //    View = "NotFiniteError"
            //});


        }
        
    }
}