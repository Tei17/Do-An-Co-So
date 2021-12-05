using OfficeOpenXml;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLSinhVien
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
