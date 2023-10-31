using IsteerPortal.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using IsteerPortal.Customers;

namespace IsteerPortal.Web.Pages.Customers
{
    public class EditModalModel : IsteerPortalPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerUpdateViewModel Customer { get; set; }

        private readonly ICustomersAppService _customersAppService;

        public EditModalModel(ICustomersAppService customersAppService)
        {
            _customersAppService = customersAppService;

            Customer = new();
        }

        public async Task OnGetAsync()
        {
            var customer = await _customersAppService.GetAsync(Id);
            Customer = ObjectMapper.Map<CustomerDto, CustomerUpdateViewModel>(customer);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customersAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerUpdateViewModel, CustomerUpdateDto>(Customer));
            return NoContent();
        }
    }

    public class CustomerUpdateViewModel : CustomerUpdateDto
    {
    }
}