using System.Collections.Generic;
using MyTraining1101Demo.Authorization.Users.Importing.Dto;
using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
