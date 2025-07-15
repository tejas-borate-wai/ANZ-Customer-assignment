using Microsoft.Extensions.Configuration;

namespace MyTraining1101Demo.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
