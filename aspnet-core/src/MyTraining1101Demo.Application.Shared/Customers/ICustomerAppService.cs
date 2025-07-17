using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyTraining1101Demo.Authorization.Users.Dto;
using MyTraining1101Demo.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Customers
{
    public interface ICustomerAppService:IApplicationService
    {
        Task CreateAsync(CreateOrEditCustomerDto input);
        Task UpdateAsync(CreateOrEditCustomerDto input);
        Task<PagedResultDto<CustomerDto>> GetAllAsync(GetAllCustomersInput input);
        Task<CreateOrEditCustomerDto> GetCustomerForEditAsync(EntityDto input);
        Task DeleteAsync(EntityDto<int> input);
        Task<ListResultDto<UserListDto>> GetUsersByCustomerId(int customerId);
    }
}
