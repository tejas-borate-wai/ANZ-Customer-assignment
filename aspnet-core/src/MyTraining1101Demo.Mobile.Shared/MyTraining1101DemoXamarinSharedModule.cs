using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace MyTraining1101Demo
{
    [DependsOn(typeof(MyTraining1101DemoClientModule), typeof(AbpAutoMapperModule))]
    public class MyTraining1101DemoXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyTraining1101DemoXamarinSharedModule).GetAssembly());
        }
    }
}