using System.Collections.Generic;
using MyTraining1101Demo.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace MyTraining1101Demo.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
