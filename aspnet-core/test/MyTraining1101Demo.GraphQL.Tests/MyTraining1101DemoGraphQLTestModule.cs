using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MyTraining1101Demo.Configure;
using MyTraining1101Demo.Startup;
using MyTraining1101Demo.Test.Base;

namespace MyTraining1101Demo.GraphQL.Tests
{
    [DependsOn(
        typeof(MyTraining1101DemoGraphQLModule),
        typeof(MyTraining1101DemoTestBaseModule))]
    public class MyTraining1101DemoGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyTraining1101DemoGraphQLTestModule).GetAssembly());
        }
    }
}