using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace IsteerPortal.Web;

[Dependency(ReplaceServices = true)]
public class IsteerPortalBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IsteerPortal";
}
