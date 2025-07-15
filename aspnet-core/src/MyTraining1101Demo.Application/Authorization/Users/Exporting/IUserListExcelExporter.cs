using System.Collections.Generic;
using MyTraining1101Demo.Authorization.Users.Dto;
using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}