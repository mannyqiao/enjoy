using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Enjoy.Core.EnjoyModels;
    using qcloudsms_csharp;
    public class QCloudSMSHelper : ISMSHelper
    {
        public void Send(ISMSEntity entity)
        {
            var sender = new SmsSingleSender(Constants.SMS_AppId, Constants.SMS_AppKey);
            sender.send(0, "86", entity.Mobile, entity.GetBody(), string.Empty, string.Empty);
        }
    }
}