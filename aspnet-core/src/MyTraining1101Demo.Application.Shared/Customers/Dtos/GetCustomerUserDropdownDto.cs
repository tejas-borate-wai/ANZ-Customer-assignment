using MyTraining1101Demo.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Customers.Dtos
{
    public class GetCustomerUserDropdownDto
    {
        public List<UserListDto> AssignedUsers { get; set; }
        public List<UserListDto> UnassignedUsers { get; set; }
    }
}
