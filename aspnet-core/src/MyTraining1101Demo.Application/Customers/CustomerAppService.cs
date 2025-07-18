using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MyTraining1101Demo.Authorization.Users;
using MyTraining1101Demo.Authorization.Users.Dto;
using MyTraining1101Demo.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;


namespace MyTraining1101Demo.Customers
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerUser> _customerUserRepository;
        private readonly IRepository<User, long> _userRepository;


        public CustomerAppService(
            IRepository<Customer> CustomerRepository,
            IRepository<CustomerUser> CustomerUserRepository,
            IRepository<User, long> UserRepository
        )
        {
            _customerRepository = CustomerRepository;
            _customerUserRepository = CustomerUserRepository;
            _userRepository = UserRepository;
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

        public async Task<ListResultDto<UserListDto>> GetUsersByCustomerId(int customerId, string filter = null)
        {
            var userIds = await _customerUserRepository
                .GetAll()
                .Where(cu => cu.CustomerId == customerId)
                .Select(cu => cu.UserId)
                .ToListAsync();

            var query = _userRepository
                .GetAll()
                .Where(u => userIds.Contains(u.Id));

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var lowerFilter = filter.ToLower();

                query = query.Where(u =>
                    (u.Name != null && u.Name.ToLower().Contains(lowerFilter)) ||
                    (u.Surname != null && u.Surname.ToLower().Contains(lowerFilter)) ||
                    (u.EmailAddress != null && u.EmailAddress.ToLower().Contains(lowerFilter)));
            }

            var users = await query.ToListAsync();

            return new ListResultDto<UserListDto>(
                ObjectMapper.Map<List<UserListDto>>(users)
            );
        }



        public async Task<ListResultDto<NameValueDto>> GetUnassignedUsersAsync()
        {
            var assignedUserIds = await _customerUserRepository.GetAll()
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();

            var unassignedUsers = await _userRepository.GetAll()
                .Where(u => !assignedUserIds.Contains(u.Id))
                .Select(u => new NameValueDto
                {
                    Name = $"{u.FullName}",
                    Value = u.Id.ToString()
                })
                .ToListAsync();

            return new ListResultDto<NameValueDto>(unassignedUsers);
        }



    }
}
