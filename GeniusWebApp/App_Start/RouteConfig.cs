using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GeniusWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "AcceptFriendRequest",
            //    url: "FriendRequest/Accept/{object}",
            //    template:
            //    //defaults: new {controller = "FriendRequest/"}
            //);

            routes.MapRoute(
                name: "User",
                url: "User/{id}",
                defaults: new { controller = "GeniusUser", action = "Index" }
            );
            /// Changed path from "Group/{groupName}" to "Group/Show/{groupName}"
            routes.MapRoute(
                name: "Group",
                url: "Group/Show/{groupName}",
                defaults: new { controller = "Group", action = "Index", groupName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

