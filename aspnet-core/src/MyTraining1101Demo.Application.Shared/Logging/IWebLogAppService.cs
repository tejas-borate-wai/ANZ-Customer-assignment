using Abp.Application.Services;
using MyTraining1101Demo.Dto;
using MyTraining1101Demo.Logging.Dto;

namespace MyTraining1101Demo.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
