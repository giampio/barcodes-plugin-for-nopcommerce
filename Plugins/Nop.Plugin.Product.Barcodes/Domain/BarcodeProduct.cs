using Nop.Core;

namespace Nop.Plugin.Product.Barcodes.Domain
{
    public partial class BarcodeProduct : BaseEntity
    {

        public virtual int ProductId { get; set; }

        public virtual string Barcode { get; set; }
    }
}