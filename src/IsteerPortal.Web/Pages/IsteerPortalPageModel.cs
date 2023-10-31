using IsteerPortal.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace IsteerPortal.Web.Pages;

public abstract class IsteerPortalPageModel : AbpPageModel
{
    protected IsteerPortalPageModel()
    {
        LocalizationResourceType = typeof(IsteerPortalResource);
    }
}
