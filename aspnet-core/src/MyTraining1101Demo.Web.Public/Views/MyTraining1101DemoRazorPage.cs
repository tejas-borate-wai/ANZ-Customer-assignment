using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace MyTraining1101Demo.Web.Public.Views
{
    public abstract class MyTraining1101DemoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected MyTraining1101DemoRazorPage()
        {
            LocalizationSourceName = MyTraining1101DemoConsts.LocalizationSourceName;
        }
    }
}
