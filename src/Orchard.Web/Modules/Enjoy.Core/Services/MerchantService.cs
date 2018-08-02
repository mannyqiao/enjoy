


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
    using Enjoy.Core.ViewModels;

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
            return this.SaveOrUpdate(model, Validate, RecordSetter);
        }
        protected override void RecordSetter(Records::Merchant record, Models::MerchantModel model)
        {
            if (model.EnjoyUser == null) throw new NullReferenceException("enjoye user is null");
            record.Address = model.Address;
            record.AgreementMediaId = model.AgreementMediaId;
            record.AppId = model.AppId;
            record.BeginTime = model.BeginTime;
            record.BrandName = model.BrandName;
            record.Contact = model.Contact;
            record.CreateTime = model.CreateTime;
            record.EndTime = model.EndTime;
            record.EnjoyUser = new Records.EnjoyUser { Id = model.EnjoyUser.Id };
            record.LogoUrl = model.LogoUrl;
            record.MerchantId = model.MerchantId;
            record.Mobile = model.Mobile;
            record.OperatorMediaId = model.OperatorMediaId;
            record.PrimaryCategoryId = model.PrimaryCategoryId;
            record.Protocol = model.Protocol;
            record.SecondaryCategoryId = model.SecondaryCategoryId;
            record.LastActivityTime = model.UpdateTime;
            record.Status = model.Status;
            record.ErrMsg = model.ErrMsg;
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

        public Models.WxResponseWapper<AuditStatus> QueryMerchantStatus(long merchantid)
        {
            var model = this.QueryFirstOrDefault((builder) =>
             {
                 builder.Add(Expression.Eq("MerchantId", merchantid));
             }, r => new MerchantModel(r));
            return new WxResponseWapper<AuditStatus>()
            {
                ErrCode = model == null ? EnjoyConstant.ObjectNotExits : EnjoyConstant.Success,
                Info = model == null ? AuditStatus.NotFond : model.Status
            };
        }

        public Models.PagingData<Models.MerchantModel> QueryMyMerchants(long userid, int page)
        {
            var apply = this.WeChat.GetApplyProtocol();
            var condition = PagingCondition.GenerateByPageAndSize(page, EnjoyConstant.DefaultPageSize);

            return this.Query(condition, (builder) =>
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

        public void UpdateMerchantStatus(long merchantId, AuditStatus status, string reson)
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

        public Models.MerchantModel GetDefaultMerchant(long id)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));
            },
            r => new Models.MerchantModel(r));
            return model;
        }

        public Models.MerchantModel GetDefaultMerchantByWeChatMerchantId(long merchantid)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("MerchantId", merchantid));
            },
            r => new Models.MerchantModel(r));
            return model;
        }

        public PagingData<MerchantModel> QueryMyMerchants(
            QueryFilter filter,
            PagingCondition condition)
        {
            return this.Query(condition, (builder) =>
            {

            }, 
            r => new MerchantModel(r));
        }
    }
}