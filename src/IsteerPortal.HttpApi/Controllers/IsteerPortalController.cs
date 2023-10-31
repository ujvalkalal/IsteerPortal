using IsteerPortal.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IsteerPortal.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class IsteerPortalController : AbpControllerBase
{
    protected IsteerPortalController()
    {
        LocalizationResource = typeof(IsteerPortalResource);
    }
}
