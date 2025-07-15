using System;
using Abp.Notifications;
using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}