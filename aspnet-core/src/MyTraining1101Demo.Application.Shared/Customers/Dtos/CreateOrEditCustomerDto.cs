using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Customers.Dtos
{
    public class CreateOrEditCustomerDto
    {
        public int? Id { get; set; }               // null for create, value for edit
        public string Name { get; set; }
        public string EmailId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Address { get; set; }
        public List<long> AssignedUserIds { get; set; } // from the dropdown
    }

}
