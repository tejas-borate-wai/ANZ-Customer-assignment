using Abp.Domain.Services;

namespace MyTraining1101Demo
{
    public abstract class MyTraining1101DemoDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected MyTraining1101DemoDomainServiceBase()
        {
            LocalizationSourceName = MyTraining1101DemoConsts.LocalizationSourceName;
        }
    }
}
