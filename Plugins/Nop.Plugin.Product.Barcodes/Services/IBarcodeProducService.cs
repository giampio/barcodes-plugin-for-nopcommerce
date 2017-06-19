using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Product.Barcodes.Domain;
using Nop.Core.Data;
using Nop.Core;

namespace Nop.Plugin.Product.Barcodes.Services
{ 
    public interface IBarcodeProducService
    {
        /// <summary>
        /// Get record by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Barcode Product</returns>
        BarcodeProduct GetById(int Id);

        /// <summary>
        /// Gets all Barcodes for a Product
        /// </summary>
        /// <param name="productId">id Product</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Barcodes Product</returns>
        IPagedList<BarcodeProduct> GetBarcodesProduct(int productId = 0, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Insert the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Insert(BarcodeProduct record);

        /// <summary>
        /// Update the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Update(BarcodeProduct record);

        /// <summary>
        /// Delete the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Delete(BarcodeProduct record);
    }
}

