using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using MyTraining1101Demo.Friendships.Dto;

namespace MyTraining1101Demo.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }
        
        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}