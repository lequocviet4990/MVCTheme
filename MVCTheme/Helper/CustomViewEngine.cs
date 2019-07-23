using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTheme.Helper
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            var themeName = ConfigurationManager.AppSettings["ThemeName"];

            ViewLocationFormats = new string[] { "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml", "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml" };
            PartialViewLocationFormats = new string[] { "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml", "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml" };
        }
    }
}