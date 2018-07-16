using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Models.Records
{
    /// <summary>
    /// 
    /// </summary>
    public class Merchant : IEntityKey<int>
    {
        public virtual int Id { get; set; }
        /// <summary>
        /// 由微信公众平台返回， 子商户id，对于一个母商户公众号下唯一
        /// </summary>
        public virtual int? MerchantId { get; set; }
        /// <summary>
        /// wxxxxxxxxxxx 子商户的公众号app_id，配置后子商户卡券券面上的app_id为该app_id。注意：该app_id须经过认证
        /// </summary>        

        public virtual string AppId { get; set; }
        /// <summary>
        /// 兰州拉面 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上
        /// </summary>
        public virtual string BrandName { get; set; }
        /// <summary>
        /// http://mmbiz.xxxx	子商户logo，可通过 上传图片接口 获取。该logo将在制券时填入并显示在卡券页面上
        /// </summary>
        public virtual string LogoUrl { get; set; }
        /// <summary>
        /// String(36)  mdasdfkl ：	授权函ID，即通过 上传临时素材接口 上传授权函后获得的meida_id
        /// </summary>
        public virtual string Protocol { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual long BeginTime { get; set; }
        /// <summary>
        /// 是 unsigned int	15300000	授权函有效期截止时间（东八区时间，单位为秒），需要与提交的扫描件一致
        /// </summary>
        public virtual long EndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int PrimaryCategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int SecondaryCategoryId { get; set; }

        /// <summary>
        /// 否 string (36)	2343343424	营业执照或个体工商户营业执照彩照或扫描件
        /// </summary>
        public virtual string AgreementMediaId { get; set; }

        /// <summary>
        /// 否 string (36)	2343343424	营业执照内登记的经营者身份证彩照或扫描件
        /// </summary>
        public virtual string OperatorMediaId { get; set; }

      

        /// <summary>
        /// 子商户创建时间
        /// </summary>
        public virtual long CreateTime { get; set; }
        /// <summary>
        /// 子商户更新时间
        /// </summary>
        public virtual long UpdateTime { get; set; }


        public virtual AuditStatus Status { get; set; }
        /// <summary>
        /// 商户创建者
        /// </summary>
        public virtual EnjoyUser EnjoyUser { get; set; }

        public virtual string Contact { get; set; }

        public virtual string Mobile { get; set; }
        public virtual string Address { get; set; }
        public virtual string ErrMsg { get; set; }

        //商户创建传入参数

        //参数名       必填    类型 示例  说明
        //info        是      json结构
        //app_id      否      String(36)  wxxxxxxxxxxx 子商户的公众号app_id，配置后子商户卡券券面上的app_id为该app_id。注意：该app_id须经过认证
        //brand_name  是      String(36)  兰州拉面 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上
        //logo_url    是      string (128)	http://mmbiz.xxxx	子商户logo，可通过 上传图片接口 获取。该logo将在制券时填入并显示在卡券页面上
        //protocol    是      String(36)  mdasdfkl ：	授权函ID，即通过 上传临时素材接口 上传授权函后获得的meida_id
        //end_time    是      unsigned int	15300000	授权函有效期截止时间（东八区时间，单位为秒），需要与提交的扫描件一致
        //primary_category_id 是 int	2	一级类目id,可以通过本文档中接口查询
        //secondary_category_id   是 int	2	二级类目id，可以通过本文档中接口查询
        //agreement_media_id  否 string (36)	2343343424	营业执照或个体工商户营业执照彩照或扫描件
        //operator_media_id   否 string (36)	2343343424	营业执照内登记的经营者身份证彩照或扫描件

        //商户创建 成功 api返回参数
        //{
        //"info": {
        // "merchant_id": 12,
        // "app_id":"xxxxxxxxxxxxx",
        // "create_time": 1438790559,
        // "update_time": 1438790559,
        // "brand_name": "aaaaaa",
        // "logo_url": "http://mmbiz.xxxx",
        // "status": "CHECKING",
        // "begin_time": 1438790559,
        // "end_time": 1438990559,
        // "primary_category_id": 1,
        // "secondary_category_id": 101
        //}
        //}
    }
}