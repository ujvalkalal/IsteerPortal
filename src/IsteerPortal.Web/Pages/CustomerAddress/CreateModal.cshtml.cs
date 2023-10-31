using IsteerPortal.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsteerPortal.CustomerAddress;

namespace IsteerPortal.Web.Pages.CustomerAddress
{
    public class CreateModalModel : IsteerPortalPageModel
    {
        [BindProperty]
        public CustomerAddresCreateViewModel CustomerAddres { get; set; }

        public List<SelectListItem> CustomerLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICustomerAddressAppService _customerAddressAppService;

        public CreateModalModel(ICustomerAddressAppService customerAddressAppService)
        {
            _customerAddressAppService = customerAddressAppService;

            CustomerAddres = new();
        }

        public async Task OnGetAsync()
        {
            CustomerAddres = new CustomerAddresCreateViewModel();
            CustomerLookupList.AddRange((
                                    await _customerAddressAppService.GetCustomerLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _customerAddressAppService.CreateAsync(ObjectMapper.Map<CustomerAddresCreateViewModel, CustomerAddresCreateDto>(CustomerAddres));
            return NoContent();
        }
    }

    public class CustomerAddresCreateViewModel : CustomerAddresCreateDto
    {
    }
}