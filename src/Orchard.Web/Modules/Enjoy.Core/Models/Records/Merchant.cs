using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    /// <summary>
    /// 
    /// </summary>
    public class Merchant : IEntityKey<long>
    {
        /// <summary>
        /// 商户流水号
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 商户代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 兰州拉面 子商户名称（12个汉字内），该名称将在制券时填入并显示在卡券页面上
        /// </summary>
        public virtual string BrandName { get; set; }
        /// <summary>
        /// 商户创建者
        /// </summary>
        public virtual EnjoyUser EnjoyUser { get; set; }
        /// <summary>
        /// 小程序相关配置
        /// </summary>
        public virtual string Miniprogarm { get; set; }
        /// <summary>
        /// 公众号相关配置
        /// </summary>
        public virtual string Official { get; set; }
        /// <summary>
        /// 支付商户号相关配置
        /// </summary>
        public virtual string Payment { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Contact { get; set; }
        /// <summary>
        /// 商户地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 商户创建时间
        /// </summary>
        public virtual long CreateTime { get; set; }
        /// <summary>
        /// 商户更新时间
        /// </summary>
        public virtual long LastActivityTime { get; set; }
   
    }
}