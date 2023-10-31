using IsteerPortal.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace IsteerPortal.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(IsteerPortalEntityFrameworkCoreModule),
    typeof(IsteerPortalApplicationContractsModule)
)]
public class IsteerPortalDbMigratorModule : AbpModule
{
}
