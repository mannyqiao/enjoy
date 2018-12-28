

namespace Enjoy.Core.Services
{
    using Record = Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using System;
    using Enjoy.Core.WeChatModels;
    using Orchard;
    using NHibernate.Criterion;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    public class CardCouponService : QueryBaseService<Record::CardCoupon, CardCounponModel>, ICardCouponService
    {
        private readonly IWeChatApi WeChat;
        public CardCouponService(IOrchardServices os, IWeChatApi wechat)
            : base(os)
        {
            this.WeChat = wechat;
        }
        public override Type ModelType
        {
            get
            {
                return typeof(Core.ICardCoupon);
            }
        }

        public QRCodeWxResponse CreateQRCode(string cardid)
        {
            var request = WeChatApiRequestBuilder.GenerateWxQRCodeUrl(this.WeChat.GetToken());

            return request.GetResponseForJson<QRCodeWxResponse>((http) =>
            {
                http.Method = "POST";
                http.ContentType = "application/json; encoding=utf-8";
                var data = new
                {
                    action_name = "QR_CARD",
                    expire_seconds = 1800,
                    action_info = new
                    {
                        card = new
                        {
                            card_id = cardid,
                            code = CodeTypes.CODE_TYPE_QRCODE.ToString(),
                            openid = "",
                            is_unique_code = false,
                            outer_str = "13b",
                        }
                    }
                };
                using (var stream = http.GetRequestStream())
                {
                    var buffers = UTF8Encoding.UTF8.GetBytes(data.SerializeToJson());
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            });

        }

        public CardCounponModel GetCardCounpon(long id)
        {
            return this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));

            }, o => new CardCounponModel(o));
        }



        public PagingData<CardCounponModel> QueryCardCoupon(PagingCondition condition, CardTypes type)
        {
            return this.Query(condition, (builder) =>
             {
                 if (type != CardTypes.None)
                     builder.Add(Expression.Eq("CardType", type));
             },
            (record) =>
            {
                return new CardCounponModel(record);
            });
        }
        public IList<CardCounponModel> QueryCardCoupon(long merchantId, CardTypes[] types, CardCouponStates[] states)
        {
            var session = this.OS.TransactionManager.GetSession();
            var criteria = session.CreateCriteria<Record::CardCoupon>();
            criteria.Add(Expression.Eq("Merchant.Id", merchantId));
            criteria.Add(Expression.In("Type", types));
            if(states!=null&& states.Count() > 0) {
                criteria.Add(Expression.In("Status", states));
            }
            return criteria.List<Record::CardCoupon>()
                .Select(record => new CardCounponModel(record))
                .ToList();

        }
        public ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model)
        {
            //发布到微信
            model.LastUpdateTime = DateTime.UtcNow.ToUnixStampDateTime();

            var result = this.SaveOrUpdate(model, Validate, RecordSetter);
            //var qrcode = CreateQRCode(model.WxNo);
            return result;
        }
        public CreateCouponWxResponse Publish(long id)
        {
            var model = this.GetCardCounpon(id);
            var request = string.IsNullOrEmpty(model.WxNo)
                ? WeChatApiRequestBuilder.GenerateWxCreateCardUrl(this.WeChat.GetToken())
                : WeChatApiRequestBuilder.GenerateWxUpdateCardUrl(this.WeChat.GetToken());
            ////TODO : update has some error 
            var result = request.GetResponseForJson<CreateCouponWxResponse>((http) =>
            {
                http.Method = "POST";
                http.ContentType = "application/json;encoding=utf-8";
                using (var stream = http.GetRequestStream())
                {
                    var json = string.IsNullOrEmpty(model.WxNo)
                        ? model.CardCoupon.GenreateCreatingWapper().SerializeToJson()
                        : model.CardCoupon.GenreateUpgradeWpper().SerializeToJson();
                    var buffers = UTF8Encoding.UTF8.GetBytes(json);
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            });
            if (result.HasError == false)
            {
                model.State = CardCouponStates.Approved;
                model.WxNo = result.CardId;
                model.CardCoupon.CardId = result.CardId;
                if (model.Type == CardTypes.MEMBER_CARD)
                {
                    this.WeChat.SetMemberCardFieldIfActiveByWx(model.WxNo);
                }
                this.SaveOrUpdate(model);
            }
            else
            {
                model.State = CardCouponStates.PublishedError;
                this.SaveOrUpdate(model);
            }
            return result;
        }
        public NormalWxResponse TestwhiteList(string[] wechatids)
        {
            var request = WeChatApiRequestBuilder.GenerateWxtestwhitelist(this.WeChat.GetToken());
            return request.GetResponseForJson<NormalWxResponse>((http) =>
            {
                http.Method = "POST";
                http.ContentType = "application/json; encoding=utf-8";
                var data = new
                {
                    openid = new string[] { },
                    username = wechatids
                };
                return http;
            });
        }

        protected override void RecordSetter(Record::CardCoupon record, CardCounponModel model)
        {
            record.CreatedTime = model.CreatedTime;
            record.BrandName = model.BrandName;
            record.Merchant = this.OS.TransactionManager.GetSession().Get<Record::Merchant>(model.Merchant.Id);
            record.Quantity = model.Quantity;
            record.WxNo = model.WxNo;
            record.Type = model.Type;
            record.Status = model.State;
            record.JsonMetadata = model.CardCoupon.SerializeToJson();
            record.ErrMsg = model.ErrMsg;
        }



        private IResponse Validate(CardCounponModel model)
        {
            return new BaseResponse(Constants.Success);
        }

        public PagingData<CardCounponModel> QueryCardCoupon(WebQueryFilter filter, PagingCondition condition)
        {
            return base.Query(filter, condition, null, record => new CardCounponModel(record));
        }


        public void UpdateStatus(string wxno, CardCouponStates status, string reson)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("WxNo", wxno));
            }, o => new CardCounponModel(o));
            model.State = status;
            model.ErrMsg = reson;
            this.SaveOrUpdate(model);
        }

        public CardCounponModel GetCardCounpon(string cardid)
        {
            return QueryFirstOrDefault((builder) =>
            {
                builder.Add(Restrictions.Eq("WxNo", cardid));
            },
            record => new CardCounponModel(record));
        }

        public Record::WxUserCardCoupon QueryWxUserCardCoupon(string userCardCode)
        {
            return base.QueryFirstOrDefault<Record::WxUserCardCoupon>
            ((builder) =>
            {
                builder.Add(Restrictions.Eq("UserCardCode", userCardCode));
            });
        }

        public BaseResponse DeleteById(long id)
        {
            var model = this.GetCardCounpon(id);
            if (string.IsNullOrEmpty(model.WxNo) == false)
            {
                this.WeChat.DeleteCardCoupon(model.WxNo);
            }
            return base.Delete(id);
        }

        public void SaveWxUserCardCoupon(WxUserCardCouponModel model)
        {
            var record = this.QueryWxUserCardCoupon(model.UserCardCode);
            if (record == null)
            {
                record = new Record::WxUserCardCoupon();
            };
            record.UserCardCode = model.UserCardCode;
            record.Merchant = new Records.Merchant() { Id = model.Merchant.Id };
            record.FriendUserName = model.FriendUserName;
            record.CardCoupon = new Records.CardCoupon() { Id = model.CardCounpon.Id };
            record.IsGiveByFriend = model.IsGiveByFriend;
            record.Gotfrom = model.IsGiveByFriend ? new Records.WxUser() { Id = model.Gotfrom.Id } : null;
            record.Owner = new Records.WxUser() { Id = model.Owner.Id };
            record.LastActivityTime = model.LastActivityTime;
            record.OldUserCardCode = model.OldUserCardCode;
            record.UserCardCode = model.UserCardCode;
            record.ExtraInfo = model.ExtraInfo;
            record.State = model.State;
            record.Type = model.Type;
            this.OS.TransactionManager.GetSession().SaveOrUpdate(record);
        }
        readonly string QueryString_CardCouponNeryBy = @"SELECT 
    [card].[Id],
    [card].[WxNo],
    [merchant].[BrandName] AS [Merchant],
    [card].[BrandName] AS [BrandName],
    '' AS Privilege,
    [merchant].[LogoUrl] AS [LogoUrl],
    [shop].[Latitude],
    [shop].[Longitude]
    FROM [Enjoy_Core_Shop] [shop]
    LEFT JOIN [Enjoy_Core_Merchant] [merchant]
        ON [shop].[Merchant_Id] = [merchant].[Id]
    LEFT JOIN [Enjoy_Core_CardCoupon] [card]
        ON [shop].[Merchant_Id] = [card].[Merchant_Id]
WHERE SQRT(
    (
        ((:Longitude - [shop].[Longitude]) * PI() * 12656 * COS(((:Latitude + [shop].[Latitude]) / 2) * PI() / 180) /180) * ((:Longitude - [shop].[Longitude]) *
        PI() * 12656 * COS(((:Latitude + [shop].[Latitude]) /
            2) * PI() / 180) /
        180)) + (((:Latitude - [shop].[Latitude]) *
        PI() * 12656 / 180) * (
        (:Latitude - [shop].[Latitude]) * PI() *
        12656 / 180)
        )
    )  < :Distance
";
        //public PagingData<CardCouponNearby> QueryCardCoupon(Location location, PagingCondition condition, float distance)
        //{
        //    var session = this.OS.TransactionManager.GetSession();
        //    var results = session.CreateSQLQuery(QueryString_CardCouponNeryBy)
        //        .AddEntity(typeof(CardCouponNearby))
        //         .SetSingle("Latitude", location.Latitude)
        //         .SetSingle("Longitude", location.Longitude)
        //         .SetSingle("Distance", distance)
        //         .SetMaxResults(condition.Size)
        //         .List<CardCouponNearby>();
        //    return new PagingData<CardCouponNearby>(results)
        //    {
        //        Paging = new Paging(condition.Page, condition.Size)
        //    };
        //}
    }
}