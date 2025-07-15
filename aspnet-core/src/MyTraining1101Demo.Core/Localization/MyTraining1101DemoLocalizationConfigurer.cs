using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace MyTraining1101Demo.Localization
{
    public static class MyTraining1101DemoLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    MyTraining1101DemoConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(MyTraining1101DemoLocalizationConfigurer).GetAssembly(),
                        "MyTraining1101Demo.Localization.MyTraining1101Demo"
                    )
                )
            );
        }
    }
}