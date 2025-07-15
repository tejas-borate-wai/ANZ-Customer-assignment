using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using MyTraining1101Demo.Common;

namespace MyTraining1101Demo.Authorization.Users.Dto
{
    public class GetLinkedUsersInput : IPagedResultRequest, ISortedResultRequest, IShouldNormalize
    {
        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }

        public string Sorting { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting) || Sorting == "userName ASC")
            {
                Sorting = "TenancyName, Username";
            }

            Sorting = DtoSortingHelper.ReplaceSorting(Sorting, s =>
            {
                if (s == "userName DESC")
                {
                    s = "TenancyName DESC, UserName DESC";
                }

                return s;
            });
        }
    }
}
