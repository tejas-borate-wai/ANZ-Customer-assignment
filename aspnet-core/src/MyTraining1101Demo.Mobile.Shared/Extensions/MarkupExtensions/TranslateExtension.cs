using System;
using MyTraining1101Demo.Core;
using MyTraining1101Demo.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTraining1101Demo.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return Text;
            }

            return L.Localize(Text);
        }
    }
}