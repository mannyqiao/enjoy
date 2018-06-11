

namespace Enjoy.Core.Services
{
    using Records = Enjoy.Core.Models.Records;
    using Models = Enjoy.Core.Models;
    using System;
    using WeChat.Models;
    using Orchard;
    using NHibernate.Criterion;

    public class CardCouponService : QueryBaseService<Records::CardCoupon, Models::CardCounponModel>, ICardCouponService
    {

        public CardCouponService(IOrchardServices os)
            : base(os)
        {

        }
        public override Type ModelType
        {
            get
            {
                return typeof(ICardCoupon);
            }
        }

        public Models::WxCardCouponWapper<ICardCoupon> CreateCardCouponInstance(CardTypes type)
        {
            switch (type)
            {
                case CardTypes.CASH:
                    return new Models::WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new CashCoupon()
                    };
                case CardTypes.DISCOUNT:
                    return new Models::WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new DiscountCoupon()
                    };
                case CardTypes.GENERAL_COUPON:
                    return new Models::WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new GeneralCoupon()
                    };
                case CardTypes.GIFT:
                    return new Models::WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new GiftCoupon()
                    };
                case CardTypes.GROUPON:
                    return new Models::WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new Groupon()
                    };

                case CardTypes.MEMBER_CARD:
                    break;
            }
            throw new NotSupportedException("No supported with type:" + type.ToString());
        }

        public Models.CardCounponModel GetCardCounpon(int id)
        {
            return this.QueryFirstOrDefaut((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));

            }, o => new Models.CardCounponModel(o));
        }

        public Models.PagingData<Models::CardCounponModel> QueryCardCounpon(int page, CardTypes type)
        {
            return this.QueryByPaging(page, (builder) =>
             {
                 if (type != CardTypes.None)
                     builder.Add(Expression.Eq("CardType", type));
             },
            (record) =>
            {
                return new Models::CardCounponModel(record);
            });
        }

        public Models::ActionResponse<Models::CardCounponModel> SaveOrUpdate(Models.CardCounponModel model)
        {
            return this.SaveOrUpdate(model, Validate, Convert);
        }
        private Records::CardCoupon Convert(Models::CardCounponModel model)
        {
            return this.ConvertToRecord<int>(model, (r, m) =>
            {
                if (r == null) r = new Records.CardCoupon();
                r.CreatedTime = m.CreatedTime;
                r.BrandName = m.BrandName;
                r.Merchant = this.OS.TransactionManager.GetSession().Get<Records::Merchant>(model.Merchant.Id);
                r.Quantity = m.Quantity;
                r.WxNo = m.WxNo;
                r.Type = m.Type;
                r.JsonMetadata = m.CardCouponWapper.ToJson();
                return r;
            });
        }

        private IResponse Validate(Models::CardCounponModel model)
        {
            return new Models::BaseResponse(EnjoyConstant.Success);
        }

    }
}