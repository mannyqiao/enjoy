

namespace Enjoy.Core.EnjoyModels
{
    using Records = Enjoy.Core.Records;
    using Newtonsoft.Json;
    using System;
    using Enjoy.Core.WeChatModels;
    using System.Collections.Generic;

    public class MerchantModel : IModelKey<long>
    {
        public MerchantModel()
        {
            this.BeginTime = DateTime.UtcNow.ToUnixStampDateTime();
            this.EndTime = DateTime.UtcNow.AddYears(1).ToUnixStampDateTime();
        }

        public MerchantModel(Records.Merchant record)
        {
            if (record != null)
            {
                this.Address = record.Address;
                ///  this.AgreementMediaId = record.AgreementMediaId;
                // this.AppId = record.AppId;
                // this.BeginTime = record.BeginTime;
                this.BrandName = record.BrandName;
                this.Contact = record.Contact;
                this.CreateTime = record.CreateTime;
                //  this.EndTime = record.EndTime;
                this.EnjoyUser = new EnjoyUserModel(record.EnjoyUser);
                this.Id = record.Id;
                //this.LogoUrl = record.LogoUrl;
                // this.MerchantId = record.MerchantId;
                this.Mobile = record.Mobile;
                // this.OperatorMediaId = record.OperatorMediaId;
                // this.PrimaryCategoryId = record.PrimaryCategoryId;
                //this.Protocol = record.Protocol;
                //this.SecondaryCategoryId = record.SecondaryCategoryId;
                this.UpdateTime = record.LastActivityTime;
                this.Official = record.Official.DeserializeToObject<WeChatConfig>();
                this.Miniprogram = record.Miniprogram.DeserializeToObject<WeChatConfig>();
                this.Payment = record.Miniprogram.DeserializeToObject<Dictionary<string, string>>();
                //  this.Status = record.Status;
                // this.ErrMsg = record.ErrMsg;
                // this.Secrect = record.Secrect;
            }


        }
        public long Id { get; set; }
        /// <summary>
        /// 由微信公众平台返回， 子商户id，对于一个母商户公众号下唯一
        /// </summary>
        public long? MerchantId { get; set; }
        /// <summary>
        /// wxxxxxxxxxxx 子商户的公众号app_id，配置后子商户卡券券面上的app_id为该app_id。注意：该app_id须经过认证
        /// </summary>        

        public string AppId { get; set; }
        /// <summary>
        /// 兰州拉面 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上
        /// </summary>
        public string BrandName { get; set; }
        public string Secrect { get; set; }
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

        public long BeginTime { get; set; }

        /// <summary>
        /// 子商户创建时间
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// 子商户更新时间
        /// </summary>
        public long UpdateTime { get; set; }


        public AuditStatus Status { get; set; }
        public string StatusName
        {
            get
            {
                return Status.WithDisplayName(this.ErrMsg);
            }
        }
        public string ActionLinks
        {
            get
            {
                return Status.GenerateActionLink(this.Id);
            }
        }

        /// <summary>
        /// 商户创建者
        /// </summary>
        [JsonIgnore]
        public EnjoyUserModel EnjoyUser { get; set; }

        public string Contact { get; set; }

        public string Mobile { get; set; }
        public string Address { get; set; }
        public string CategoryName { get; set; }
        public string ErrMsg { get; set; }

        public IWeChatConfig Miniprogram { get; set; }
        public IWeChatConfig Official { get; set; }
        public Dictionary<string, string> Payment { get; set; }
    }
}