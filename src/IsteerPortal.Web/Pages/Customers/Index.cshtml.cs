using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using IsteerPortal.Customers;
using IsteerPortal.Shared;

namespace IsteerPortal.Web.Pages.Customers
{
    public class IndexModel : AbpPageModel
    {
        public string? NameFilter { get; set; }
        public string? WebsiteFilter { get; set; }
        public string? EmailIdFilter { get; set; }
        public string? ContactNumberFilter { get; set; }

        private readonly ICustomersAppService _customersAppService;

        public IndexModel(ICustomersAppService customersAppService)
        {
            _customersAppService = customersAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}