using Enjoy.Core.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class MerchantProfileViewModel
    {
        public int Id { get; set; }
        public string MerchantName { get; set; }
        public string License { get; set; }
        public string LogoUrl { get; set; }
        public string LicenseImageUrl { get; set; }
        public string Contact { get; set; }
        public string LegalOwner { get; set; }
        public string Category { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public EnjoyUser EnjoyUser { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}