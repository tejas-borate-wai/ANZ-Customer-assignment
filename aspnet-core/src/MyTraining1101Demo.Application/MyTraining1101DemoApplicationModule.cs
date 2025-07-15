using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyTraining1101Demo.Authorization;

namespace MyTraining1101Demo
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(MyTraining1101DemoApplicationSharedModule),
        typeof(MyTraining1101DemoCoreModule)
        )]
    public class MyTraining1101DemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyTraining1101DemoApplicationModule).GetAssembly());
        }
    }
}