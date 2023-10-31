using Volo.Abp.Modularity;

namespace IsteerPortal;

[DependsOn(
    typeof(IsteerPortalApplicationModule),
    typeof(IsteerPortalDomainTestModule)
    )]
public class IsteerPortalApplicationTestModule : AbpModule
{

}
