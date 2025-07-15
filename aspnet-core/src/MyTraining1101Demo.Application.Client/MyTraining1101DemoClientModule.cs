using Abp.Modules;
using Abp.Reflection.Extensions;

namespace MyTraining1101Demo
{
    public class MyTraining1101DemoClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyTraining1101DemoClientModule).GetAssembly());
        }
    }
}
