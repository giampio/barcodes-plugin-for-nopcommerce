using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Product.Barcodes.Domain;
using Nop.Core.Data;
using Nop.Core;
using Nop.Admin.Infrastructure.Cache;
using Nop.Core.Caching;

namespace Nop.Plugin.Product.Barcodes.Services
{
    public class BarcodeProductService : IBarcodeProducService
    {
        private const string BARCODE_PRODUCT_ALL_KEY = "Nop.plugins.barcodeProduct.all-{0}-{1}";

        private readonly ICacheManager _cacheManager;
        private readonly IRepository<BarcodeProduct> _barcodeProductRepository;

        public BarcodeProductService(ICacheManager cacheManager,
            IRepository<BarcodeProduct> barcodeArticoleRepository)
        {
            _cacheManager = cacheManager;
            _barcodeProductRepository = barcodeArticoleRepository;
        }

        /// <summary>
        /// Get record by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Barcode Product</returns>
        public virtual BarcodeProduct GetById(int Id)
        {
            if (Id == 0)
                return null;

            return _barcodeProductRepository.GetById(Id);
        }

        /// <summary>
        /// Gets all Barcodes for a Product
        /// </summary>
        /// <param name="productId">id Product</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Barcodes Product</returns>
        public virtual IPagedList<BarcodeProduct> GetBarcodesProduct(int productId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var key = string.Format(BARCODE_PRODUCT_ALL_KEY, pageIndex, pageSize);
            return _cacheManager.Get(key, () =>
            {
                var query = from tr in _barcodeProductRepository.Table
                            where tr.ProductId == productId
                            orderby tr.Id
                            select tr;
                var records = new PagedList<BarcodeProduct>(query, pageIndex, pageSize);
                return records;
            });
        }

        /// <summary>
        /// Insert the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public virtual void Insert(BarcodeProduct record)
        {
            if (record == null)
                throw new ArgumentNullException("productBarcode");

            _barcodeProductRepository.Insert(record);
        }

        /// <summary>
        /// Update the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public virtual void Update(BarcodeProduct record)
        {
            if (record == null)
                throw new ArgumentNullException("productBarcode");

            _barcodeProductRepository.Update(record);
        }

        /// <summary>
        /// Delete the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public virtual void Delete(BarcodeProduct record)
        {
            if (record == null)
                throw new ArgumentNullException("productBarcode");

            _barcodeProductRepository.Delete(record);
        }
    }
}
