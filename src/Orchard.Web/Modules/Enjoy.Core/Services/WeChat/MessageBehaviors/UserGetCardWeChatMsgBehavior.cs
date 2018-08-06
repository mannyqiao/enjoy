

namespace Enjoy.Core.Services
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.Records;
    using Enjoy.Core.WeChatModels;
    using Orchard;
    using Orchard.Logging;

    public class UserGetCardWeChatMsgBehavior : WeChatMsgBehavior<GetCardCouponWeChatEventArgs>
    {
        private readonly IWxUserService WxUserService;
        private readonly IWeChatApi WeChat;
        private readonly ICardCouponService CardCoupon;
        public UserGetCardWeChatMsgBehavior(
            IOrchardServices os,
            IWxUserService userService,
            IWeChatApi weChat,
            ICardCouponService ccService)
            : base(os)
        {
            this.WxUserService = userService;
            this.WeChat = weChat;
            this.CardCoupon = ccService;
            Logger = NullLogger.Instance;
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.user_get_card;
            }
        }
        public ILogger Logger { get; set; }
        protected override void Execute(GetCardCouponWeChatEventArgs model)
        {
            //保存微信用户 ，如果没有保存的话            
            var enjoyWxUserId = this.WxUserService.Register(
                new WxUserModel(this.WeChat.GetWxUser(model.FromUserName)) { RegistryType = RegistryTypes.WxServicePush }
                );
            long? gotfromEnjoyWxUserId = null;
            if (model.IsGiveByFriend)
            {
                gotfromEnjoyWxUserId = this.WxUserService.Register(
                    new WxUserModel(this.WeChat.GetWxUser(model.FriendUserName)) { RegistryType = RegistryTypes.WxServicePush }
                    );
            }
            var cc = this.CardCoupon.GetCardCounpon(model.CardId);
            if (cc == null)
            {
                Logger.Error("Can't find coupon templete by cardid {0}", model.CardId);
                return;
            }
            switch (cc.Type)
            {
                case CardTypes.CASH:
                case CardTypes.DISCOUNT:
                case CardTypes.GENERAL_COUPON:
                case CardTypes.GIFT:
                case CardTypes.GROUPON://生成用户卡券
                    var coupon = model.Generate(cc, enjoyWxUserId, gotfromEnjoyWxUserId, false);
                    this.CardCoupon.SaveWxUserCardCoupon(coupon);
                    break;
                case CardTypes.MEMBER_CARD://生成会员卡
                    break;
            }
        }
    }
}