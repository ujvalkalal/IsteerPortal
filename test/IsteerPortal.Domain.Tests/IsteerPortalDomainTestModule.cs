using IsteerPortal.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace IsteerPortal;

[DependsOn(
    typeof(IsteerPortalEntityFrameworkCoreTestModule)
    )]
public class IsteerPortalDomainTestModule : AbpModule
{

}
