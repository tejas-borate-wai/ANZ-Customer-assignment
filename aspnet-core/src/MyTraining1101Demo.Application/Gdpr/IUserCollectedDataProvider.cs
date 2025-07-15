using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
