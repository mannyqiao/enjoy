


namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.Records;
    using Enjoy.Core.WeChatModels;
    public class MerchantViewModel
    {
        public MerchantViewModel()
        {

        }

        public MerchantModel Merchant { get; set; }

        public long OwnerId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public string Area { get; set; }
        //['province', 'city', 'area']
        public ApplyProtocolWxResponse ApplyProtocol { get; set; }
        public AuditStatus Status { get; set; }
        //agreement_media_id  否 string (36)	2343343424	营业执照或个体工商户营业执照彩照或扫描件
        //operator_media_id   否 string (36)	2343343424	营业执照内登记的经营者身份证彩照或扫描件
    }
}