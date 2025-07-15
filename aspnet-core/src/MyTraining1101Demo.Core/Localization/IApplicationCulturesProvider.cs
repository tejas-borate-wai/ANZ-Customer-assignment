using System.Globalization;

namespace MyTraining1101Demo.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}