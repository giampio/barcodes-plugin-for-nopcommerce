using System;
using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Product.Barcodes.Data;
using Nop.Plugin.Product.Barcodes.Domain;
using Nop.Plugin.Product.Barcodes.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Product.Barcodes.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_barcode_product";

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<BarcodeProductService>().As<IBarcodeProducService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<BarcodeProductObjectContext>(builder, CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<BarcodeProduct>>()
                .As<IRepository<BarcodeProduct>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
