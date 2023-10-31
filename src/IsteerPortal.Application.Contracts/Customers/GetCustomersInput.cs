using Volo.Abp.Application.Dtos;
using System;

namespace IsteerPortal.Customers
{
    public class GetCustomersInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? EmailId { get; set; }
        public string? ContactNumber { get; set; }

        public GetCustomersInput()
        {

        }
    }
}