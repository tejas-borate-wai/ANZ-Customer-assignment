using Abp;

namespace MyTraining1101Demo
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="MyTraining1101DemoDomainServiceBase"/>.
    /// For application services inherit MyTraining1101DemoAppServiceBase.
    /// </summary>
    public abstract class MyTraining1101DemoServiceBase : AbpServiceBase
    {
        protected MyTraining1101DemoServiceBase()
        {
            LocalizationSourceName = MyTraining1101DemoConsts.LocalizationSourceName;
        }
    }
}