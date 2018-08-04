

namespace Enjoy.Core.Services
{
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using System;
    using Enjoy.Core.WeChatModels;
    using Orchard;
    using NHibernate.Criterion;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    public class CardCouponService : QueryBaseService<CardCoupon, CardCounponModel>, ICardCouponService
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
                return typeof(ICardCoupon);
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
                    var buffers = UTF8Encoding.UTF8.GetBytes(data.ToJson());
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

        public ActionResponse<CardCounponModel> SaveOrUpdate(CardCounponModel model)
        {
            //发布到微信
            model.LastUpdateTime = DateTime.Now.ToUnixStampDateTime();
            model.State = CardCouponStates.Editing;
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
                http.ContentType = "application/json; encoding=utf-8";
                using (var stream = http.GetRequestStream())
                {
                    var buffers = UTF8Encoding.UTF8.GetBytes(model.CardCouponWapper.ToJson());
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            });
            if (result.HasError == false)
            {
                model.State = CardCouponStates.Checking;
                model.WxNo = result.CardId;
                model.CardCouponWapper.Card.SetCardId(model.WxNo);
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

        protected override void RecordSetter(CardCoupon record, CardCounponModel model)
        {
            record.CreatedTime = model.CreatedTime;
            record.BrandName = model.BrandName;
            record.Merchant = this.OS.TransactionManager.GetSession().Get<Merchant>(model.Merchant.Id);
            record.Quantity = model.Quantity;
            record.WxNo = model.WxNo;
            record.Type = model.Type;
            record.Status = model.State;
            record.JsonMetadata = model.CardCouponWapper.ToJson();
            record.ErrMsg = model.ErrMsg;
        }



        private IResponse Validate(CardCounponModel model)
        {
            return new BaseResponse(EnjoyConstant.Success);
        }

        public PagingData<CardCounponModel> QueryCardCoupon(QueryFilter filter, PagingCondition condition)
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

        public WxUserCardCoupon QueryWxUserCardCoupon(string userCardCode)
        {
            return base.QueryFirstOrDefault<WxUserCardCoupon>
            ((builder) =>
            {
                builder.Add(Restrictions.Eq("UserCardCode", userCardCode));
            });
        }

        public BaseResponse DeleteById(long id)
        {
            return base.Delete(id);
        }

        public void SaveWxUserCardCoupon(WxUserCardCouponModel model)
        {
            var record = this.QueryWxUserCardCoupon(model.UserCardCode);
            if (record == null)
            {
                record = new WxUserCardCoupon();
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


    }
}