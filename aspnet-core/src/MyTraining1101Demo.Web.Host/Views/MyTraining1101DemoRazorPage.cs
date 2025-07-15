using Abp.AspNetCore.Mvc.Views;

namespace MyTraining1101Demo.Web.Views
{
    public abstract class MyTraining1101DemoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected MyTraining1101DemoRazorPage()
        {
            LocalizationSourceName = MyTraining1101DemoConsts.LocalizationSourceName;
        }
    }
}
