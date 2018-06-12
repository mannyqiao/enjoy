

namespace Enjoy.Core.Services
{
    using Records = Enjoy.Core.Models.Records;
    using Models = Enjoy.Core.Models;
    using System;
    using WeChat.Models;
    using Orchard;
    using NHibernate.Criterion;
    using System.Text;

    public class CardCouponService : QueryBaseService<Records::CardCoupon, Models::CardCounponModel>, ICardCouponService
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

        public Models.QRCodeWxResponse CreateQRCode(string cardid)
        {
            var request = WeChatApiRequestBuilder.GenerateWxQRCodeUrl(this.WeChat.GetToken());

            return request.GetResponseForJson<Models::QRCodeWxResponse>((http) =>
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


            //发布到微信

            //model.WxNo = wxresponse.CardId;
            var result = this.SaveOrUpdate(model, Validate, Convert);
            var r = TestwhiteList(new string[] { "s66822351", "ebying" });
            //var qrcode = CreateQRCode(model.WxNo);
            return result;
        }
        public Models::CreateCouponWxResponse Publish(int id)
        {
            var request = WeChatApiRequestBuilder.GenerateWxCreateCardUrl(this.WeChat.GetToken());
            var model = this.GetCardCounpon(id);
            var result = request.GetResponseForJson<Models::CreateCouponWxResponse>((http) =>
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
                model.WxNo = result.CardId;
                this.SaveOrUpdate(model);
            }
            return result;
        }
        //public static QRCodeWxResponse CreateWxQRCode(string token, string cardid)
        //{
        //    var request = WxUtil.GenerateWxQRCodeUrl(token);

        //    var qrcode = request.GetResponseForJson<QRCodeWxResponse>((http) =>
        //    {
        //        http.Method = "POST";
        //        http.ContentType = "application/json; encoding=utf-8";
        //        var data = new
        //        {
        //            action_name = "QR_CARD",
        //            expire_seconds = 1800,
        //            action_info = new
        //            {
        //                card = new
        //                {
        //                    card_id = cardid,
        //                    code = "",
        //                    openid = "",
        //                    is_unique_code = false,
        //                    outer_str = "13b",
        //                }
        //            }
        //        };
        //        using (var stream = http.GetRequestStream())
        //        {
        //            var buffers = UTF8Encoding.UTF8.GetBytes(data.ToJson());
        //            stream.Write(buffers, 0, buffers.Length);
        //            stream.Flush();
        //        }
        //        return http;
        //    });
        //    return qrcode;
        //}
        public Models.NormalWxResponse TestwhiteList(string[] wechatids)
        {
            var request = WeChatApiRequestBuilder.GenerateWxtestwhitelist(this.WeChat.GetToken());
            return request.GetResponseForJson<Models::NormalWxResponse>((http) =>
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


        //public static NormalWxResponse TestwhiteList(string token,
        //    string[] usernams,
        //    string[] openids = null)
        //{
        //    var request = WxUtil.GenerateWxtestwhitelist(token);
        //    return request.GetResponseForJson<NormalWxResponse>((http) =>
        //    {
        //        http.Method = "POST";
        //        http.ContentType = "application/json; encoding=utf-8";
        //        var data = new
        //        {
        //            openid = openids ?? new string[] { },
        //            username = new string[] { "s66822351" ""}
        //        };
        //        using (var stream = http.GetRequestStream())
        //        {
        //            var buffers = UTF8Encoding.UTF8.GetBytes(data.ToJson());
        //            stream.Write(buffers, 0, buffers.Length);
        //            stream.Flush();
        //        }
        //        return http;
        //    });
        //}
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