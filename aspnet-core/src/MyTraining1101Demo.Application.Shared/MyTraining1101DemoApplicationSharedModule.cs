using Abp.Modules;
using Abp.Reflection.Extensions;

namespace MyTraining1101Demo
{
    [DependsOn(typeof(MyTraining1101DemoCoreSharedModule))]
    public class MyTraining1101DemoApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyTraining1101DemoApplicationSharedModule).GetAssembly());
        }
    }
}