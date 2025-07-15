using System;
using MyTraining1101Demo.Core;
using MyTraining1101Demo.Core.Dependency;
using MyTraining1101Demo.Services.Permission;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTraining1101Demo.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class HasPermissionExtension : IMarkupExtension
    {
        public string Text { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return false;
            }

            var permissionService = DependencyResolver.Resolve<IPermissionService>();
            return permissionService.HasPermission(Text);
        }
    }
}