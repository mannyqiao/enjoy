using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class VirtualAccount : IWeChatCardKey
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 所属appid
        /// </summary>
        public virtual string AppId { get; set; }
        /// <summary>
        /// 用户opnid
        /// </summary>
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 会员卡id
        /// </summary>
        public virtual string CardId { get; set; }

        /// <summary>
        /// 账户状态
        /// </summary>
        public virtual VAStates State { get; set; }
        /// <summary>
        /// 会员卡用户code
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public virtual int Money { get; set; }
        /// <summary>
        /// 最后一次交易记录
        /// </summary>
        public virtual TradeDetails LastTrading { get; set; }

        public virtual long LastUpdatedTime { get; set; }

    }
}