using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyTraining1101Demo.Sessions.Dto;

namespace MyTraining1101Demo.Models.Common
{
    [AutoMapFrom(typeof(TenantLoginInfoDto)),
     AutoMapTo(typeof(TenantLoginInfoDto))]
    public class TenantLoginInfoPersistanceModel : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}