

namespace Enjoy.Core.Services
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.Records;
    using Enjoy.Core.WeChatModels;
    using Orchard;

    public class UserGetCardWeChatMsgBehavior : WeChatMsgBehavior<GetCardCouponWeChatEventArgs>
    {
        private readonly IWxUserService WxUserService;
        private readonly IWeChatApi WeChat;
        private readonly ICardCouponService CCService;
        public UserGetCardWeChatMsgBehavior(
            IOrchardServices os,
            IWxUserService userService,
            IWeChatApi weChat,
            ICardCouponService ccService)
            : base(os)
        {
            this.WxUserService = userService;
            this.WeChat = weChat;
            this.CCService = ccService;
        }

        public override EventTypes Type
        {
            get
            {
                return EventTypes.user_get_card;
            }
        }

        protected override void Execute(GetCardCouponWeChatEventArgs model)
        {
            //保存微信用户 ，如果没有保存的话
            var wxUserId = this.WxUserService.Register(
                   new WxUserModel(this.WeChat.GetWxUser(model.FromUserName)) { RegistryType = RegistryTypes.WxServicePush }
              );
            var gotfrom = (long?)null;
            if (model.IsGiveByFriend)
            {
                var wx = this.WxUserService.GetWxUser(model.UnionId);
                gotfrom = wx == null ? (long?)null : wx.Id;
            }
            //获取卡券模板
            var cc = this.CCService.GetCardCounpon(model.CardId);
            switch (cc.Type)
            {
                case CardTypes.CASH:
                case CardTypes.DISCOUNT:
                case CardTypes.GENERAL_COUPON:
                case CardTypes.GIFT:
                case CardTypes.GROUPON://生成用户卡券
                    var coupon = model.Generate(cc, wxUserId, gotfrom);
                    this.CCService.SaveWxUserCardCoupon(coupon);
                    break;
                case CardTypes.MEMBER_CARD://生成会员卡

                    break;
            }
        }
    }
}