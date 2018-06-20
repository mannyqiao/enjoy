

namespace Enjoy.Core.Models
{
    using Records = Enjoy.Core.Models.Records;
    using Newtonsoft.Json;
    public class MerchantModel : IEntityKey<int>
    {
        public MerchantModel() { }
        public MerchantModel(Records::Merchant record)
        {
            if (record != null)
            {
                this.Address = record.Address;
                this.AgreementMediaId = record.AgreementMediaId;
                this.AppId = record.AppId;
                this.BenginTime = record.BenginTime;
                this.BrandName = record.BrandName;
                this.Contact = record.Contact;
                this.CreateTime = record.CreateTime;
                this.EndTime = record.EndTime;
                this.EnjoyUser = record.EnjoyUser;
                this.Id = record.Id;
                this.LogoUrl = record.LogoUrl;
                this.MerchantId = record.MerchantId;
                this.Mobile = record.Mobile;
                this.OperatorMediaId = record.OperatorMediaId;
                this.PrimaryCategoryId = record.PrimaryCategoryId;
                this.Protocol = record.Protocol;
                this.SecondaryCategoryId = record.SecondaryCategoryId;
                this.UpdateTime = record.UpdateTime;
                this.Status = record.Status;
                
            }

            
        }
        public int Id { get; set; }
        /// <summary>
        /// 由微信公众平台返回， 子商户id，对于一个母商户公众号下唯一
        /// </summary>
        public int MerchantId { get; set; }
        /// <summary>
        /// wxxxxxxxxxxx 子商户的公众号app_id，配置后子商户卡券券面上的app_id为该app_id。注意：该app_id须经过认证
        /// </summary>        

        public string AppId { get; set; }
        /// <summary>
        /// 兰州拉面 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// http://mmbiz.xxxx	子商户logo，可通过 上传图片接口 获取。该logo将在制券时填入并显示在卡券页面上
        /// </summary>
        public virtual string LogoUrl { get; set; }
        /// <summary>
        /// String(36)  mdasdfkl ：	授权函ID，即通过 上传临时素材接口 上传授权函后获得的meida_id
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// 是 unsigned int	15300000	授权函有效期截止时间（东八区时间，单位为秒），需要与提交的扫描件一致
        /// </summary>
        public long EndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PrimaryCategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SecondaryCategoryId { get; set; }

        /// <summary>
        /// 否 string (36)	2343343424	营业执照或个体工商户营业执照彩照或扫描件
        /// </summary>
        public string AgreementMediaId { get; set; }

        /// <summary>
        /// 否 string (36)	2343343424	营业执照内登记的经营者身份证彩照或扫描件
        /// </summary>
        public string OperatorMediaId { get; set; }

        public long BenginTime { get; set; }

        /// <summary>
        /// 子商户创建时间
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// 子商户更新时间
        /// </summary>
        public long UpdateTime { get; set; }


        public AuditStatus Status { get; set; }
        /// <summary>
        /// 商户创建者
        /// </summary>
        [JsonIgnore]
        public Records::EnjoyUser EnjoyUser { get; set; }

        public string Contact { get; set; }

        public string Mobile { get; set; }
        public string Address { get; set; }
        public string CategoryName { get; set; }
    }
}