using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Web.Framework.Menu;
using Nop.Plugin.Product.Barcodes.Data;

namespace Nop.Plugin.Product.Barcodes
{
    /// <summary>
    /// Plugin
    /// </summary>
    public class BarcodeProductProvider : BasePlugin, IMiscPlugin
    {

        private readonly BarcodeProductObjectContext _context;

        public BarcodeProductProvider(BarcodeProductObjectContext context)
        { 
            _context = context;
        }

        public override void Install()
        {
            _context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            _context.Uninstall();
            base.Uninstall();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "List";
            controllerName = "BarcodeProduct";

            routeValues = new RouteValueDictionary();
            routeValues["NameSpaces"] = "Nop.Plugin.Product.Barcodes.Controllers";
            routeValues["area"] = null;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            //var menuItem = new SiteMapNode()
            //{
            //    SystemName = "BarcodeProduct",
            //    Title = "BarcodeProduct",
            //    ControllerName = "Configure",
            //    ActionName = "Index",
            //    Visible = true,
            //    RouteValues = new RouteValueDictionary() { { "area", null } },
            //};

            //var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            //if (pluginNode != null)
            //    pluginNode.ChildNodes.Add(menuItem);
            //else
            //    rootNode.ChildNodes.Add(menuItem);
        }

    }
}
