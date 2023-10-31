using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using IsteerPortal.CustomerAddress;
using IsteerPortal.Shared;

namespace IsteerPortal.Web.Pages.CustomerAddress
{
    public class IndexModel : AbpPageModel
    {
        public string? Address1Filter { get; set; }
        public string? Address2Filter { get; set; }
        public string? ZIPCODEFilter { get; set; }
        [SelectItems(nameof(CustomerLookupList))]
        public Guid? CustomerIdFilter { get; set; }
        public List<SelectListItem> CustomerLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly ICustomerAddressAppService _customerAddressAppService;

        public IndexModel(ICustomerAddressAppService customerAddressAppService)
        {
            _customerAddressAppService = customerAddressAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerLookupList.AddRange((
                    await _customerAddressAppService.GetCustomerLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}