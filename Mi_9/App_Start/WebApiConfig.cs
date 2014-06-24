﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Mi_9
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "rootApi",
                routeTemplate: "{controller}",
                defaults: new { id = RouteParameter.Optional, controller = "Values" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );
        }
    }
}
