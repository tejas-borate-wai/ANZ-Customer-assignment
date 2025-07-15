using Abp.Application.Services.Dto;

namespace MyTraining1101Demo.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}