﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMSweb.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務            
            // Web API 路由            
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",    
                defaults: new { id = RouteParameter.Optional }
            );
            //允許CORS           
            //var cors = new EnableCorsAttribute("*", "*", "*");            
            //config.EnableCors(cors);            
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(
            t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
    
}