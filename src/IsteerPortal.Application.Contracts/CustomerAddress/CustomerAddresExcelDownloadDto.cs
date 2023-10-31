using Volo.Abp.Application.Dtos;
using System;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddresExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? ZIPCODE { get; set; }
        public Guid? CustomerId { get; set; }

        public CustomerAddresExcelDownloadDto()
        {

        }
    }
}