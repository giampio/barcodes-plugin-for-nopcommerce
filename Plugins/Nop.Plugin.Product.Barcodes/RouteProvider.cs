using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Product.Barcodes
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Nop.Plugin.Product.Barcodes.Product",
                "barcode-product/{productId}", 
                new { controller = "BarcodeProduct", action = "Edit" },
                new[] { "Nop.Plugin.Product.Barcodes.Controllers" }
            );
                        
            routes.MapRoute("Nop.Plugin.Product.Barcodes.Edit",
               "Admin/Plugins/BarcodeProduct/Edit/{productId}",
               new { controller = "BarcodeProduct", action = "Edit" },
               new[] { "Nop.Plugin.Product.Barcodes.Controllers" }
            );

            routes.MapRoute("Nop.Plugin.Product.Barcodes.GetBarcodes",
                "Admin/Plugins/BarcodeProduct/GetBarcodes/{productId}",
                new { controller = "BarcodeProduct", action = "GetBarcodes" },
                new[] { "Nop.Plugin.Product.Barcodes.Controllers" }
            );

            routes.MapRoute("Nop.Plugin.Product.Barcodes.Update",
                "Admin/Plugins/BarcodeProduct/Update",
                new { controller = "BarcodeProduct", action = "Update" },
                new[] { "Nop.Plugin.Product.Barcodes.Controllers" }
            );

            routes.MapRoute("Nop.Plugin.Product.Barcodes.Delete",
                "Admin/Plugins/BarcodeProduct/Delete",
                new { controller = "BarcodeProduct", action = "Delete" },
                new[] { "Nop.Plugin.Product.Barcodes.Controllers" }
            );           

        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
