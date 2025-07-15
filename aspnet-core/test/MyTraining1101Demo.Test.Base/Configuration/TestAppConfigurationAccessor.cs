using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;

namespace MyTraining1101Demo.Test.Base.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(MyTraining1101DemoTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
