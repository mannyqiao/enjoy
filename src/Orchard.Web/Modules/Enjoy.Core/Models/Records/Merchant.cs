using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    public class Merchant
    {
        public virtual int Id { get; set; }
        public virtual string MerchantName { get; set; }
        public virtual string License { get; set; }
        public virtual string LicenseImageUrl { get; set; }
        public virtual string Contact { get; set; }
        public virtual string LegalOwner { get; set; }
        public virtual string Category { get; set; }
        public virtual string AppId { get; set; }
        public virtual string AppSecret { get; set; }
        public virtual EnjoyUser EnjoyUser { get; set; }
        public virtual DateTime? LastUpdatedTime { get; set; }
        public virtual DateTime CreatedTime { get; set; }

    }
}