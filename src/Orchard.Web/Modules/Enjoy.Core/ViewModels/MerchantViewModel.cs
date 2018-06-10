using Enjoy.Core.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.Models;
    public class MerchantViewModel
    {
        public MerchantViewModel()
        {
            
        }
     
        public MerchantModel Merchant { get; set; }

        public int OwnerId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        //['province', 'city', 'area']
        public ApplyProtocolWxResponse ApplyProtocol { get; set; }
        public AuditStatus Status { get; set; }
        //agreement_media_id  否 string (36)	2343343424	营业执照或个体工商户营业执照彩照或扫描件
        //operator_media_id   否 string (36)	2343343424	营业执照内登记的经营者身份证彩照或扫描件
    }
}