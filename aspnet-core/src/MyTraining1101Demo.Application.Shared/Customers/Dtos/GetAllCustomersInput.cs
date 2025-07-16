using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Customers.Dtos
{
    public class GetAllCustomersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; } // optional search string
    }

    public class CustomerDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Address { get; set; }
    }

}
