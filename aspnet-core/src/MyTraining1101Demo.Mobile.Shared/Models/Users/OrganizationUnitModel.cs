using Abp.AutoMapper;
using MyTraining1101Demo.Organizations.Dto;

namespace MyTraining1101Demo.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}