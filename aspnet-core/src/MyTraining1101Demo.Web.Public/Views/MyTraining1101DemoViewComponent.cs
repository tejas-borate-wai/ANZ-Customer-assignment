using Abp.AspNetCore.Mvc.ViewComponents;

namespace MyTraining1101Demo.Web.Public.Views
{
    public abstract class MyTraining1101DemoViewComponent : AbpViewComponent
    {
        protected MyTraining1101DemoViewComponent()
        {
            LocalizationSourceName = MyTraining1101DemoConsts.LocalizationSourceName;
        }
    }
}