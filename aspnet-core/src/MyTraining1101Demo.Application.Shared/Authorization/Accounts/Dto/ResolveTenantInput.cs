namespace MyTraining1101Demo.Authorization.Accounts.Dto
{
    public class ResolveTenantIdInput
    {
        // An encrypted text which contains tenantId={value} string
        public string c { get; set; }
    }
}