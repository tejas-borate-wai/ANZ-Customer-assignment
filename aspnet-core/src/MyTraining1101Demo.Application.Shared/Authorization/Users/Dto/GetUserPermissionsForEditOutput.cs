using System.Collections.Generic;
using MyTraining1101Demo.Authorization.Permissions.Dto;

namespace MyTraining1101Demo.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}