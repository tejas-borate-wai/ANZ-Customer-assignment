using System.Collections.Generic;
using Abp;
using MyTraining1101Demo.Chat.Dto;
using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
