using System.Web.Mvc;
using Nop.Services.Catalog;
using Nop.Web.Factories;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Admin.Extensions;
using Nop.Admin.Helpers;
using Nop.Admin.Models.Catalog;
using Nop.Plugin.Product.Barcodes.Domain;
using Nop.Plugin.Product.Barcodes.Services;
using Nop.Services.Security;
using Nop.Core.Domain.Vendors;
using System.Collections.Generic;
using System;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System.Linq;
using Nop.Admin.Controllers;
using Nop.Services.Localization;
using Nop.Core.Caching;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Services.Shipping;
using Nop.Services;
using Nop.Web.Models.Catalog;
using Nop.Services.Media;
using Nop.Plugin.Product.Barcodes.Models;

namespace Nop.Plugin.Product.Barcodes.Controllers
{
    public class BarcodeProductController : BaseAdminController
    {
        private readonly ICatalogModelFactory _catalogModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IWorkContext _workContext;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IShippingService _shippingService;
        private readonly ICacheManager _cacheManager;
        private readonly CatalogSettings _catalogSettings;
        private readonly IBarcodeProducService _barcodeArticoleService;
        private readonly IPermissionService _permissionService;
        private readonly VendorSettings _vendorSettings;
        private readonly IVendorService _vendorService;
        private readonly IStoreMappingService _storeMappingService;
        
        public BarcodeProductController(ICatalogModelFactory catalogModelFactory,
        ICategoryService categoryService,
        IManufacturerService manufacturerService,
        IProductModelFactory productModelFactor,
        IProductService productService,
        IPictureService pictureService,       
        IProductAttributeService productAttributeService,
        IWorkContext workContext,
        ILanguageService languageService,
        ILocalizationService localizationService,
        ILocalizedEntityService localizedEntityService,
        IStoreContext storeContext,
        IStoreService storeService,
        IShippingService shippingService,
        ICacheManager cacheManager,
        CatalogSettings catalogSettings,
        IBarcodeProducService barcodeArticoleService,
        IPermissionService permissionService,
        VendorSettings vendorSettings,
        IVendorService vendorService,
        IStoreMappingService storeMappingService)
        {
            //model
            this._catalogModelFactory = catalogModelFactory;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._productModelFactory = productModelFactor;
            this._productService = productService;
            this._pictureService = pictureService;            
            this._productAttributeService = productAttributeService;
            this._workContext = workContext;           
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._shippingService = shippingService;
            this._cacheManager = cacheManager;
            this._catalogSettings = catalogSettings;
            this._barcodeArticoleService = barcodeArticoleService;
            this._permissionService = permissionService;            
            this._vendorSettings = vendorSettings;
            this._vendorService = vendorService;
            this._storeMappingService = storeMappingService;
        }
        public ActionResult Configure(int categoryId)
        {
            //if (categoryId == null)
            //    return Content("");

            // if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //     return AccessDeniedView();

            //var model = new ProductListModel();
            //a vendor should have access only to his products
            //model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            //model.AllowVendorsToImportProducts = _vendorSettings.AllowVendorsToImportProducts;

            //var productsInCategory = _categoryService.GetProductCategoriesByCategoryId((int)CategoryId, 0, int.MaxValue);
            //var productsInCategory = _categoryService.GetProductCategoriesByCategoryId(59, 0, 10);
            //var products = _productService.SearchProducts(0,10);

            //var products = new List<Product>();
            //foreach (var product in productsInCategory)
            //{
            //    var id = product.Product.Id;
            //    var productResult = _productService.GetProductById(id);
            //    products.Add(productResult);
            //} 

            //var category = _categoryService.GetCategoryById(categoryId);

            //var model = _productModelFactory.PrepareProductOverviewModels(products, true, true);                      

            //model
            //var model = _catalogModelFactory.PrepareCategoryModel(category);

            //var model = new List<ProductOverviewModel>();
            //model.AddRange(_productModelFactory.PrepareProductOverviewModels(products));

            //model = _catalogModelFactory.PrepareSearchModel(model, command);


            //var product = _productService.GetProductById(idProduct);
            //ProductDetailsModel model = null;

            ////If the product exists we will log it
            //if (product != null)
            //{
            //    model = _productModelFactory.PrepareProductDetailsModel(product);

            //    //Setup the product to save
            //    BarcodeArticole record = new BarcodeArticole();
            //    record.IDBARCODE = idProduct;
            //    record.BARCODE = product.Name;

            //    //Map the values we're interested in to our new entity
            //    _barcodeArticoleService.Log(record);

            ////Setup the product to save
            //ProductArticole record = new ProductArticole();
            //record.IDARTICOLO = idProduct;
            //record.CODICEARTICOLO = product.Name;

            ////record.CustomerId = _workContext.CurrentCustomer.Id;
            ////record.IpAddress = _workContext.CurrentCustomer.LastIpAddress;
            ////record.IsRegistered = _workContext.CurrentCustomer.IsRegistered();

            ////Map the values we're interested in to our new entity
            //_productArticoleService.Log(record);

            //}

            ////Return the view, it doesn't need a model
            //return Content("");

            var products = _productService.SearchProducts(
                       categoryIds: new List<int>() { Convert.ToInt32(categoryId) }
                       );

            var model = _productModelFactory.PrepareProductOverviewModels(products);

            return View("~/Plugins/Product.Barcodes/Views/Configure.cshtml", model);
        }

        //list products
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var model = new ProductListModel();
            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            model.AllowVendorsToImportProducts = _vendorSettings.AllowVendorsToImportProducts;

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);
            foreach (var c in categories)
                model.AvailableCategories.Add(c);

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, _cacheManager, true);
            foreach (var m in manufacturers)
                model.AvailableManufacturers.Add(m);

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //warehouses
            model.AvailableWarehouses.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var wh in _shippingService.GetAllWarehouses())
                model.AvailableWarehouses.Add(new SelectListItem { Text = wh.Name, Value = wh.Id.ToString() });

            //vendors
            model.AvailableVendors.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var vendors = SelectListHelper.GetVendorList(_vendorService, _cacheManager, true);
            foreach (var v in vendors)
                model.AvailableVendors.Add(v);

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            model.AvailableProductTypes.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.All"), Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.PublishedOnly"), Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.UnpublishedOnly"), Value = "2" });

            return View("~/Plugins/Product.Barcodes/Views/List.cshtml", model);
        }               

        [HttpPost, ActionName("List")]
        [FormValueRequired("go-to-product-by-sku")]
        public virtual ActionResult GoToSku(ProductListModel model)
        {
            string sku = model.GoDirectlyToSku;

            //try to load a product entity
            var product = _productService.GetProductBySku(sku);

            //if not found, then try to load a product attribute combination
            if (product == null)
            {
                var combination = _productAttributeService.GetProductAttributeCombinationBySku(sku);
                if (combination != null)
                {
                    product = combination.Product;
                }
            }

            if (product != null)
                return RedirectToAction("ProductEdit", "BarcodeProductController", new { id = product.Id });

            //not found
            return List();
        }

        public ActionResult GetBarcodes(int productId)
        {
            var barcodes = _barcodeArticoleService.GetBarcodesProduct(productId);
            var model = barcodes.Select(x =>
            {                
                return new BarcodeProduct
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Barcode = x.Barcode
                };
            }).ToList();

            return Json(new DataSourceResult
            {
                Data = model,
                Total = barcodes.TotalCount
            });
        }

        public ActionResult Edit(int productId)
        {
            var product = _productService.GetProductById(productId);

            //var model = _productModelFactory.PrepareProductDetailsModel(product);
                      
            //ViewBag.Name = product.Name;

            var model = new BarcodeProductModel { Id = product.Id, Barcode = null, ProductName = product.Name };              

            return View("~/Plugins/Product.Barcodes/Views/Edit.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult Edit(BarcodeProductModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");

            BarcodeProduct record = new BarcodeProduct();
            record.ProductId = model.Id;
            record.Barcode = model.Barcode;
            _barcodeArticoleService.Insert(record);

            return View("~/Plugins/Product.Barcodes/Views/Edit.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult Update(BarcodeProductModel model)
        {
           // if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
          //      return Content("Access denied");

            BarcodeProduct record = _barcodeArticoleService.GetById(model.Id);

            if (record != null)
            {
                record.ProductId = model.ProductId;
                record.Barcode = model.Barcode;
                _barcodeArticoleService.Update(record);
            }

            return new NullJsonResult();
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult Delete(BarcodeProductModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");

            BarcodeProduct record = _barcodeArticoleService.GetById(model.Id);

            if (record == null)
                throw new ArgumentException("No record found with the specified id");
            
            _barcodeArticoleService.Delete(record);

            return new NullJsonResult();
        }       
    }
}
