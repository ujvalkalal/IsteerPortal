using IsteerPortal.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsteerPortal.Customers;

namespace IsteerPortal.Web.Pages.Customers
{
    public class CreateModalModel : IsteerPortalPageModel
    {
        [BindProperty]
        public CustomerCreateViewModel Customer { get; set; }

        private readonly ICustomersAppService _customersAppService;

        public CreateModalModel(ICustomersAppService customersAppService)
        {
            _customersAppService = customersAppService;

            Customer = new();
        }

        public async Task OnGetAsync()
        {
            Customer = new CustomerCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _customersAppService.CreateAsync(ObjectMapper.Map<CustomerCreateViewModel, CustomerCreateDto>(Customer));
            return NoContent();
        }
    }

    public class CustomerCreateViewModel : CustomerCreateDto
    {
    }
}