using IsteerPortal.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using IsteerPortal.CustomerAddress;

namespace IsteerPortal.Web.Pages.CustomerAddress
{
    public class EditModalModel : IsteerPortalPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerAddresUpdateViewModel CustomerAddres { get; set; }

        public List<SelectListItem> CustomerLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICustomerAddressAppService _customerAddressAppService;

        public EditModalModel(ICustomerAddressAppService customerAddressAppService)
        {
            _customerAddressAppService = customerAddressAppService;

            CustomerAddres = new();
        }

        public async Task OnGetAsync()
        {
            var customerAddresWithNavigationPropertiesDto = await _customerAddressAppService.GetWithNavigationPropertiesAsync(Id);
            CustomerAddres = ObjectMapper.Map<CustomerAddresDto, CustomerAddresUpdateViewModel>(customerAddresWithNavigationPropertiesDto.CustomerAddres);

            CustomerLookupList.AddRange((
                                    await _customerAddressAppService.GetCustomerLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customerAddressAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerAddresUpdateViewModel, CustomerAddresUpdateDto>(CustomerAddres));
            return NoContent();
        }
    }

    public class CustomerAddresUpdateViewModel : CustomerAddresUpdateDto
    {
    }
}