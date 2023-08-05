using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TochalTools
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product-Settings",
                url: "Product/Settings/{id}",
                defaults: new { controller = "Product", action = "Settings", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product-Rate",
                url: "Product/Rate/{productId}/{rate}",
                defaults: new { controller = "Product", action = "Rate", productId = UrlParameter.Optional, rate = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product-Delete",
                url: "Product/Delete/{id}",
                defaults: new { controller = "Product", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product-Edit",
                url: "Product/Edit/{id}",
                defaults: new { controller = "Product", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product-Create",
                url: "Product/Create/{id}",
                defaults: new { controller = "Product", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product-List",
                url: "Product/List/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product-Details",
                url: "Product/Details/{id}/{title}",
                defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional, title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product-Index",
                url: "Product/{mainCategory}/{subCategory}/{page}",
                defaults: new { controller = "Product", action = "Index", mainCategory = UrlParameter.Optional, subCategory = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tag-ReadAllByGroupForSelect",
                url: "Tag/ReadAllByGroupForSelect/{group}",
                defaults: new { controller = "Tag", action = "ReadAllByGroupForSelect", group = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tag-Delete",
                url: "Tag/Delete/{id}",
                defaults: new { controller = "Tag", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tag-Edit",
                url: "Tag/Edit/{id}",
                defaults: new { controller = "Tag", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tag-Create",
                url: "Tag/Create/{id}",
                defaults: new { controller = "Tag", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tag-List",
                url: "Tag/List/{id}",
                defaults: new { controller = "Tag", action = "List", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tag-Details",
                url: "Tag/Details/{id}/{title}",
                defaults: new { controller = "Tag", action = "Details", id = UrlParameter.Optional, title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog-Delete",
                url: "Blog/Delete/{id}",
                defaults: new { controller = "Blog", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog-Edit",
                url: "Blog/Edit/{id}",
                defaults: new { controller = "Blog", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog-Create",
                url: "Blog/Create/{id}",
                defaults: new { controller = "Blog", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog-List",
                url: "Blog/List/{id}",
                defaults: new { controller = "Blog", action = "List", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog-Details",
                url: "Blog/Details/{id}/{title}",
                defaults: new { controller = "Blog", action = "Details", id = UrlParameter.Optional, title = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog-Index",
                url: "Blog/{category}/{page}",
                defaults: new { controller = "Blog", action = "Index", category = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Comment-List",
                url: "Comment/List/{group}/{confirmed}",
                defaults: new { controller = "Comment", action = "List", group = UrlParameter.Optional, confirmed = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Address-EditCity",
                url: "Address/EditCity/{id}/{countryId}",
                defaults: new { controller = "Address", action = "EditCity", id = UrlParameter.Optional, countryId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Address-CreateCity",
                url: "Address/CreateCity/{id}/{countryId}",
                defaults: new { controller = "Address", action = "CreateCity", id = UrlParameter.Optional, countryId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Address-CityList",
                url: "Address/CityList/{id}/{countryId}",
                defaults: new { controller = "Address", action = "CityList", id = UrlParameter.Optional, countryId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Dashboard-User",
                url: "User",
                defaults: new { controller = "Dashboard", action = "User" }
            );

            routes.MapRoute(
                name: "Dashboard-Admin",
                url: "Admin",
                defaults: new { controller = "Dashboard", action = "Admin" }
            );

            routes.MapRoute(
                name: "Base-ImageResizer",
                url: "Base/ImageResizer/{width}/{height}/{fileType}/{moduleName}/{fileName}/{fileExtension}",
                defaults: new { controller = "Base", action = "ImageResizer", width = UrlParameter.Optional, height = UrlParameter.Optional, fileType = UrlParameter.Optional, moduleName = UrlParameter.Optional, fileName = UrlParameter.Optional, fileExtension = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AboutUs",
                url: "AboutUs",
                defaults: new { controller = "Home", action = "AboutUs" }
            );

            routes.MapRoute(
                name: "ContactUs",
                url: "ContactUs",
                defaults: new { controller = "Home", action = "ContactUs" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "Cart",
                defaults: new { controller = "Home", action = "Cart" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
