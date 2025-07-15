using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using MyTraining1101Demo.Authorization.Users;
using MyTraining1101Demo.MultiTenancy;

namespace MyTraining1101Demo.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}