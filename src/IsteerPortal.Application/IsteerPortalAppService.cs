using IsteerPortal.Localization;
using Volo.Abp.Application.Services;

namespace IsteerPortal;

/* Inherit your application services from this class.
 */
public abstract class IsteerPortalAppService : ApplicationService
{
    protected IsteerPortalAppService()
    {
        LocalizationResource = typeof(IsteerPortalResource);
    }
}
