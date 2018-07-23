


namespace Enjoy.Core.Services
{
    using System;
    using NHibernate;
    using Orchard;
    using Records = Enjoy.Core.Models.Records;
    using Models = Enjoy.Core.Models;
    using System.Linq;
    using NHibernate.Criterion;
    using System.Collections.Generic;
    using Enjoy.Core.Models;

    public class MerchantService : QueryBaseService<Records::Merchant, Models::MerchantModel>, IMerchantService
    {
        private readonly IEnjoyAuthService Auth;

        public readonly IWeChatApi WeChat;
        private ModelClient client = new ModelClient();

        public override Type ModelType { get { return typeof(Models::MerchantModel); } }



        public MerchantService(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat) :
            base(os)
        {
            this.Auth = auth;

            this.WeChat = wechat;
        }

        public Models.MerchantModel GetDefaultMerchant()
        {
            var active_user = this.Auth.GetAuthenticatedUser();
            if (active_user == null) throw new NullReferenceException();
            var merchart = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("EnjoyUser.Id", active_user.Id));

            }, (record) =>
            {
                if (record == null)
                {
                    return new Models.MerchantModel()
                    {
                        EnjoyUser = active_user as EnjoyUserModel
                    };
                }
                else
                {
                    return new Models.MerchantModel(record);
                }
            });
            return merchart;
        }

        public Models.ActionResponse<Models.MerchantModel> SaveOrUpdate(
            Models::MerchantModel model)
        {
            return this.SaveOrUpdate(model, Validate, Convert);
        }

        public Models.ActionResponse<Models.MerchantModel> SaveAndPushToWeChat(
            Models.MerchantModel model,
            Action<Models::MerchantModel> push = null)
        {
            if (push == null) push = PushToWechat;
            push(model);
            var result = this.SaveOrUpdate(model);
            if (string.IsNullOrEmpty(model.ErrMsg) == false)
            {
                return new ActionResponse<MerchantModel>(EnjoyConstant.ErrorMerchantState, model);
            }
            else
            {
                return new ActionResponse<MerchantModel>(EnjoyConstant.Success, model);
            }
        }
        private Records::Merchant Convert(Models::MerchantModel model)
        {
            var record = this.ConvertToRecord<Int32>(model, (r, m) =>
            {
                if (m.EnjoyUser == null) throw new NullReferenceException("enjoye user is null");

                if (r == null) r = new Records.Merchant();

                r.Address = m.Address;
                r.AgreementMediaId = m.AgreementMediaId;
                r.AppId = m.AppId;
                r.BeginTime = m.BeginTime;
                r.BrandName = m.BrandName;
                r.Contact = m.Contact;
                r.CreateTime = m.CreateTime;
                r.EndTime = m.EndTime;
                r.EnjoyUser = new Records.EnjoyUser { Id = m.EnjoyUser.Id };
                r.LogoUrl = m.LogoUrl;
                r.MerchantId = m.MerchantId;
                r.Mobile = m.Mobile;
                r.OperatorMediaId = m.OperatorMediaId;
                r.PrimaryCategoryId = m.PrimaryCategoryId;
                r.Protocol = m.Protocol;
                r.SecondaryCategoryId = m.SecondaryCategoryId;
                r.UpdateTime = m.UpdateTime;
                r.Status = m.Status;
                r.ErrMsg = m.ErrMsg;
                return r;
            });
            return record;
        }
        public IResponse Validate(Models::MerchantModel model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.BrandName) || model.BrandName.Length > 20)
            {
                errors.Add("商户名称不能为空且不能大于20个汉字");
            }
            if (string.IsNullOrEmpty(model.AgreementMediaId))
            {
                errors.Add("没有上传营业执照");
            }
            if (string.IsNullOrEmpty(model.OperatorMediaId))
            {
                errors.Add("没有上传法人身份证");
            }

            if (errors.Count > 0)
            {
                return new Models::VerifyResponse(EnjoyConstant.VerifyFailed, errors.ToArray());
            }
            return Models::VerifyResponse.CreateSuccessInstance();
        }
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void PushToWechat(Models::MerchantModel model)
        {
            var request = WeChatApiRequestBuilder.GenerateWxSubmerchantUrl(this.WeChat.GetToken(), model.MerchantId == null);
            var wapper = new Models::WxRequestWapper<Models::SubMerchant>();
            wapper.Info = new Models.SubMerchant(model);
            wapper.Info.EndTime = DateTime.Now.AddMonths(1).ToUnixStampDateTime();
            var wxrep = this.WeChat.CreateSubmerchant(wapper);
            if (wxrep.HasError == false)
            {
                model.Status = AuditStatus.CHECKING;
                model.MerchantId = wxrep.Info.MerchantId;
                model.ErrMsg = string.Empty;
            }
            else
            {
                model.Status = AuditStatus.UnCommitted;
                model.ErrMsg = wxrep.ErrMsg;
            }
        }

        public Models.WxResponseWapper<Models.MerchantModel> QueryApproveStatus(string merchantid)
        {
            throw new NotImplementedException();
        }

        public Models.PagingData<Models.MerchantModel> QueryMyMerchants(int userid, int page)
        {
            var apply = this.WeChat.GetApplyProtocol();
            var condition = PagingCondition.GenerateByPageAndSize(page, EnjoyConstant.DefaultPageSize);

            return this.QueryByPaging(condition, (builder) =>
            {
                builder.Add(Expression.Eq("EnjoyUser.Id", userid));
            },
            (record) =>
            {
                var model = new Models::MerchantModel(record);
                var primary = apply.Categories.FirstOrDefault(o => o.PrimaryCategoryId.Equals(record.PrimaryCategoryId));
                var second = primary == null
                ? new Models.SecondaryCategory()
                : primary.SecondaryCategories.FirstOrDefault(o => o.SecondaryCategoryId.Equals(record.SecondaryCategoryId));
                model.CategoryName = string.Join("/", new string[] {
                    primary==null ? "":primary.CategoryName,
                    second==null?"":second.CategoryName
                });
                return model;
            });
        }

        public void UpdateMerchantStatus(int merchantId, AuditStatus status, string reson)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("MerchantId", merchantId));
            },
            r => new Models.MerchantModel(r));
            if (model == null) return;
            model.Status = status;
            model.ErrMsg = reson;
            this.SaveOrUpdate(model);
        }

        public Models.MerchantModel GetDefaultMerchant(int id)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));
            },
            r => new Models.MerchantModel(r));
            return model;
        }
    }
}