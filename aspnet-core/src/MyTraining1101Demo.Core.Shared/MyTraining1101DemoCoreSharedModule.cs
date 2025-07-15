using Abp.Modules;
using Abp.Reflection.Extensions;

namespace MyTraining1101Demo
{
    public class MyTraining1101DemoCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyTraining1101DemoCoreSharedModule).GetAssembly());
        }
    }
}