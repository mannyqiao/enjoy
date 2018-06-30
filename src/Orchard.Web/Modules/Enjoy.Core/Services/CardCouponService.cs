

namespace Enjoy.Core.Services
{
    using Records = Enjoy.Core.Models.Records;
    using Models = Enjoy.Core.Models;
    using System;
    using WeChat.Models;
    using Orchard;
    using NHibernate.Criterion;
    using System.Text;
    using Enjoy.Core.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

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
            return this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));

            }, o => new Models.CardCounponModel(o));
        }



        public Models.PagingData<Models::CardCounponModel> QueryCardCoupon(PagingCondition condition, CardTypes type)
        {
            return this.QueryByPaging(condition, (builder) =>
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

            model.LastUpdateTime = DateTime.Now.ToUnixStampDateTime();
            model.Status = CCStatus.Editing;
            var result = this.SaveOrUpdate(model, Validate, Convert);
            var r = TestwhiteList(new string[] { "s66822351", "ebying" });
            //var qrcode = CreateQRCode(model.WxNo);
            return result;
        }
        public Models::CreateCouponWxResponse Publish(int id)
        {
            var model = this.GetCardCounpon(id);
            var request = string.IsNullOrEmpty(model.WxNo)
                ? WeChatApiRequestBuilder.GenerateWxCreateCardUrl(this.WeChat.GetToken())
                : WeChatApiRequestBuilder.GenerateWxUpdateCardUrl(this.WeChat.GetToken());
            ////TODO : update has some error 

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
                model.Status = CCStatus.Checking;
                model.WxNo = result.CardId;
                model.CardCouponWapper.Card.SetCardId(model.WxNo);
                this.SaveOrUpdate(model);
            }
            else
            {
                model.Status = CCStatus.PublishedError;
                this.SaveOrUpdate(model);
            }
            return result;
        }
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
                r.Status = m.Status;
                r.JsonMetadata = m.CardCouponWapper.ToJson();
                r.ErrMsg = m.ErrMsg;
                return r;
            });
        }

        private IResponse Validate(Models::CardCounponModel model)
        {
            return new Models::BaseResponse(EnjoyConstant.Success);
        }

        public Models.PagingData<Models.CardCounponModel> QueryCardCoupon(QueryFilter filter, PagingCondition condition)
        {
            return base.Query(condition, builder =>
            {
                foreach (var criteria in this.Criterias(filter))
                {
                    builder.Add(criteria);
                }
                foreach (var order in this.Orders(filter))
                {
                    builder.AddOrder(order);
                }
            },
            record => new Models.CardCounponModel(record));
        }
        public override IEnumerable<ICriterion> Criterias(QueryFilter filter)
        {
            var names = filter.Search.Value as string[];
            if (names != null && names.Count(o => !string.IsNullOrWhiteSpace(o)) > 0)
            {
                foreach (var name in names)
                {
                    yield return Expression.Like("BrandName", name) as ICriterion;
                }
            }

            foreach (var criteria in base.Criterias(filter))
            {
                yield return criteria;
            }
        }

        public void UpdateStatus(string wxno, CCStatus status, string reson)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("WxNo", wxno));
            }, o => new Models.CardCounponModel(o));
            model.Status = status;
            model.ErrMsg = reson;
            this.SaveOrUpdate(model);
        }
    }
}