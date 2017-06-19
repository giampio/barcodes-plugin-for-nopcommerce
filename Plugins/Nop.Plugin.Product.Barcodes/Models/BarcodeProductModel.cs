using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Product.Barcodes.Models
{
    public class BarcodeProductModel : BaseNopEntityModel
    {
        public BarcodeProductModel() { }
       
        public virtual int ProductId { get; set; }

        public virtual string Barcode { get; set; }

        public virtual string ProductName { get; set; } 
    }
}