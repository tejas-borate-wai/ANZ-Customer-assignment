using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;

namespace MyTraining1101Demo.Friendships
{
    public interface IFriendshipManager : IDomainService
    {
        Task CreateFriendshipAsync(Friendship friendship);

        Task UpdateFriendshipAsync(Friendship friendship);

        Task<Friendship> GetFriendshipOrNullAsync(UserIdentifier user, UserIdentifier probableFriend);

        Task BanFriendAsync(UserIdentifier userIdentifier, UserIdentifier probableFriend);

        Task AcceptFriendshipRequestAsync(UserIdentifier userIdentifier, UserIdentifier probableFriend);
    }
}
