using Enjoy.Core.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.Models;
    public class CreatingSubMerchantViewModel
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string LogoUrl { get; set; }
        public string Protocol { get; set; }
        public int PrimaryCategoryId { get; set; }
        public int SecondaryCategoryId { get; set; }
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

        public ApplyProtocolWxResponse ApplyProtocol { get; set; }
        //agreement_media_id  否 string (36)	2343343424	营业执照或个体工商户营业执照彩照或扫描件
        //operator_media_id   否 string (36)	2343343424	营业执照内登记的经营者身份证彩照或扫描件
    }
}