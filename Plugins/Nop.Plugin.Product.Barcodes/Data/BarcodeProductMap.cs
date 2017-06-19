using Nop.Data.Mapping;
using Nop.Plugin.Product.Barcodes.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Product.Barcodes.Data
{
    public partial class BarcodeProductMap : EntityTypeConfiguration<BarcodeProduct>
    {
        public BarcodeProductMap()
        {
            ToTable("Wp_Barcode");

            HasKey(T => T.Id);
            Property(T => T.ProductId);
            Property(T => T.Barcode);
        }
    }
}