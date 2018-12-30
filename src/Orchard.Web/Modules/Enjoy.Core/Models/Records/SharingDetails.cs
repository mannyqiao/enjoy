

namespace Enjoy.Core.Records
{
    /// <summary>
    /// 用户分享记录
    /// </summary>
    public class SharingDetails
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 关联商户
        /// </summary>
        public virtual Merchant Merchant { get; set; }
        /// <summary>
        /// 当前用户OpenId
        /// </summary>
        public virtual string SharedBy { get; set; }
        /// <summary>
        /// 分享的小程序Id
        /// </summary>
        public virtual string AppId { get; set; }
        /// <summary>
        /// 分享的卡券 Id
        /// </summary>
        public virtual string CardId { get; set; }
        /// <summary>
        /// 分享时间
        /// </summary>
        public virtual long CreatedTime { get; set; }
    }
}