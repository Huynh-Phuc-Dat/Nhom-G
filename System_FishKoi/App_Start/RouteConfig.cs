﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace System_FishKoi
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Home_Index", "trang-chu", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            //Định nghĩa đường dẫn cho trang đăng nhập
            routes.MapRoute("Login_Login", "dang-nhap", new { controller = "Login", action = "Login", id = UrlParameter.Optional });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        }
    }
}
