using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyTraining1101Demo.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.UI;


namespace MyTraining1101Demo.Customers
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerUser> _customerUserRepository;

        public CustomerAppService(
            IRepository<Customer> CustomerRepository,
            IRepository<CustomerUser> CustomerUserRepository
        )
        {
            _customerRepository = CustomerRepository;
            _customerUserRepository = CustomerUserRepository;
        }

        public async Task CreateAsync(CreateOrEditCustomerDto input)
        {
            try
            {
                var customer = ObjectMapper.Map<Customer>(input);
                var newCustomerId = await _customerRepository.InsertAndGetIdAsync(customer);

                if (input.AssignedUserIds != null)
                {
                    foreach (var userId in input.AssignedUserIds)
                    {
                        await _customerUserRepository.InsertAsync(new CustomerUser
                        {
                            CustomerId = newCustomerId,
                            UserId = userId
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw new UserFriendlyException("something go wrong ",ex);
            }
        }


        public async Task UpdateAsync(CreateOrEditCustomerDto input)
        {
            // Get the existing customer
            var customer = await _customerRepository.GetAsync(input.Id.Value);

            ObjectMapper.Map(input, customer);

            // Update entity in DB
            await _customerRepository.UpdateAsync(customer);

            // Remove old mappings
            await _customerUserRepository.DeleteAsync(x => x.CustomerId == customer.Id);

            // Add new user mappings
            if (input.AssignedUserIds != null && input.AssignedUserIds.Any())
            {
                foreach (var userId in input.AssignedUserIds)
                {
                    await _customerUserRepository.InsertAsync(new CustomerUser
                    {
                        CustomerId = customer.Id,
                        UserId = userId
                    });
                }
            }
        }


        public async Task<PagedResultDto<CustomerDto>> GetAllAsync(GetAllCustomersInput input)
        {
            var query = _customerRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(c =>
                    c.Name.Contains(input.Filter) ||
                    c.EmailId.Contains(input.Filter) ||
                    c.Address.Contains(input.Filter)
                );
            }

            var totalCount = await query.CountAsync();

            var customers = await query
                .OrderBy(input.Sorting ?? "Name ASC") 
                .PageBy(input)                        
                .ToListAsync();

            var customerDtos = ObjectMapper.Map<List<CustomerDto>>(customers);

            return new PagedResultDto<CustomerDto>(totalCount, customerDtos);
        }


        public async Task<CreateOrEditCustomerDto> GetCustomerForEditAsync(EntityDto input)
        {
            var customer = await _customerRepository.GetAsync(input.Id);

            var customerDto = ObjectMapper.Map<CreateOrEditCustomerDto>(customer);

            customerDto.AssignedUserIds = await _customerUserRepository
                .GetAll()
                .Where(x => x.CustomerId == input.Id)
                .Select(x => x.UserId)
                .ToListAsync();

            return customerDto;
        }

        public async Task DeleteAsync(EntityDto<int> input)
        {
            try
            {
                await _customerUserRepository.DeleteAsync(x => x.CustomerId == input.Id);
                await _customerRepository.DeleteAsync(input.Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error deleting customer and assigned users", ex);
                throw;
            }
        }


    }
}
