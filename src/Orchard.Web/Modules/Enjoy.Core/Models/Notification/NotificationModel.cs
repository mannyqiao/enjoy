

namespace Enjoy.Core.EnjoyModels
{
    using Enjoy.Core.Records;
    using System;
    
    public class NotificationModel:IModelKey<long>
    {
        public NotificationModel(XNotification notification)
        {
            this.Id = notification.Id;
            this.Read = notification.Read;
            this.SendBySMS = notification.SendBySMS;
            this.Title = notification.Title;
            this.Body = notification.Body;
            this.CreatedTime = notification.CreatedTime;
        }
        public NotificationModel()
        {
            this.CreatedTime = DateTime.Now.ToUnixStampDateTime();
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public long CreatedTime { get; set; }
        public bool SendBySMS { get; set; }
        public EnjoyUserModel EnjoyUser { get; set; }
        public bool Read { get; set; }
    }
}