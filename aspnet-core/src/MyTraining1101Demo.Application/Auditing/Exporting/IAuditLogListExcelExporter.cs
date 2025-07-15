using System.Collections.Generic;
using MyTraining1101Demo.Auditing.Dto;
using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
