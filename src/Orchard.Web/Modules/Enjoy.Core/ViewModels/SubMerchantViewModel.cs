using Enjoy.Core.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.Models;
    public class SubMerchantViewModel
    {
        public SubMerchantViewModel()
        {

        }
        public SubMerchantViewModel(Merchant record)
        {
            this.Id = record.Id;
            this.Country = "中国";
            this.BrandName = record.BrandName;
            var idx = 0;
            record.Address.Split('/').Select((name) =>
            {
                switch (idx)
                {
                    case 0:
                        this.Province = name;
                        break;
                    case 1:
                        this.City = name;
                        break;
                    case 2:
                        this.Area = name;
                        break;
                }
                idx++;
                return name;
            }).ToList();
            this.Contact = record.Contact;
            this.LogoUrl = record.LogoUrl;
            this.Mobile = record.Mobile;
            this.AgreementMediaId = record.AgreementMediaId;
            this.OperatorMediaId = record.OperatorMediaId;

            this.Protocol = record.Protocol;
            this.PrimaryCategoryId = record.PrimaryCategoryId;
            this.SecondaryCategoryId = record.SecondaryCategoryId;
            Enum.TryParse<MerchantStatus>(record.Status, out MerchantStatus recult);
            this.Status = recult;
            this.Address = record.Address;
            this.EnjoyUser = record.EnjoyUser.Id;
        }
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string LogoUrl { get; set; }
        public string Protocol { get; set; }
        public int PrimaryCategoryId { get; set; }
        public int SecondaryCategoryId { get; set; }
        public int EnjoyUser { get; set; }
        /// <summary>
        /// 营业执照或个体工商户营业执照彩照或扫描件
        /// </summary>
        public string AgreementMediaId { get; set; }
        /// <summary>
        /// 营业执照内登记的经营者身份证彩照或扫描件
        /// </summary>
        public string OperatorMediaId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        //['province', 'city', 'area']
        public ApplyProtocolWxResponse ApplyProtocol { get; set; }

        public MerchantStatus Status { get; set; }
        //agreement_media_id  否 string (36)	2343343424	营业执照或个体工商户营业执照彩照或扫描件
        //operator_media_id   否 string (36)	2343343424	营业执照内登记的经营者身份证彩照或扫描件
    }
}