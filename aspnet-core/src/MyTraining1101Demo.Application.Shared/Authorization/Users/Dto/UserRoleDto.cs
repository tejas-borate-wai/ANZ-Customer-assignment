namespace MyTraining1101Demo.Authorization.Users.Dto
{
    public class UserRoleDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleDisplayName { get; set; }

        public bool IsAssigned { get; set; }

        public bool InheritedFromOrganizationUnit { get; set; }
    }
}