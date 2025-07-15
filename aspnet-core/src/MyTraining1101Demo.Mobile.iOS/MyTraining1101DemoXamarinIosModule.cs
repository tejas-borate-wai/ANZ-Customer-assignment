using Abp.Modules;
using Abp.Reflection.Extensions;

namespace MyTraining1101Demo
{
    [DependsOn(typeof(MyTraining1101DemoXamarinSharedModule))]
    public class MyTraining1101DemoXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyTraining1101DemoXamarinIosModule).GetAssembly());
        }
    }
}