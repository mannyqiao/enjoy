


namespace Enjoy.Core.Services
{
    using System;
    using NHibernate;
    using Orchard;
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;
    using System.Linq;
    using NHibernate.Criterion;
    using System.Collections.Generic;

    using Enjoy.Core.ViewModels;

    public class MerchantService : QueryBaseService<Merchant, MerchantModel>, IMerchantService
    {
        private readonly IEnjoyAuthService Auth;

        public readonly IWeChatApi WeChat;
        private ModelClient client = new ModelClient();

        public override Type ModelType { get { return typeof(MerchantModel); } }



        public MerchantService(
            IOrchardServices os,
            IEnjoyAuthService auth,
            IWeChatApi wechat) :
            base(os)
        {
            this.Auth = auth;

            this.WeChat = wechat;
        }
        public MerchantModel GetDefaultMerchant()
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
                    return new MerchantModel()
                    {
                        EnjoyUser = active_user as EnjoyUserModel
                    };
                }
                else
                {
                    return new MerchantModel(record);
                }
            });
            return merchart;
        }

        public ActionResponse<MerchantModel> SaveOrUpdate(MerchantModel model)
        {
            return this.SaveOrUpdate(model, Validate, RecordSetter);
        }
        protected override void RecordSetter(Merchant record, MerchantModel model)
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
        public ActionResponse<MerchantModel> SaveAndPushToWeChat(
            MerchantModel model,
            Action<MerchantModel> push = null)
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

        public IResponse Validate(MerchantModel model)
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
                return new VerifyResponse(EnjoyConstant.VerifyFailed, errors.ToArray());
            }
            return VerifyResponse.CreateSuccessInstance();
        }
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void PushToWechat(MerchantModel model)
        {
            var request = WeChatApiRequestBuilder.GenerateWxSubmerchantUrl(this.WeChat.GetToken(), model.MerchantId == null);
            var wapper = new WxRequestWapper<SubMerchant>();
            wapper.Info = new SubMerchant(model);
            wapper.Info.EndTime = model.EndTime;
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

        public WxResponseWapper<AuditStatus> QueryMerchantStatus(long merchantid)
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

        public PagingData<MerchantModel> QueryMyMerchants(long userid, int page)
        {
            var apply = this.WeChat.GetApplyProtocol();
            var condition = PagingCondition.GenerateByPageAndSize(page, EnjoyConstant.DefaultPageSize);

            return this.Query(condition, (builder) =>
            {
                builder.Add(Expression.Eq("EnjoyUser.Id", userid));
            },
            record => Convert(record));
        }

        public void UpdateMerchantStatus(long merchantId, AuditStatus status, string reson)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("MerchantId", merchantId));
            },
            r => new MerchantModel(r));
            if (model == null) return;
            model.Status = status;
            model.ErrMsg = reson;
            this.SaveOrUpdate(model);
        }

        public MerchantModel GetDefaultMerchant(long id)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));
            },
            r => new MerchantModel(r));
            return model;
        }

        public MerchantModel GetDefaultMerchantByWeChatMerchantId(long merchantid)
        {
            var model = this.QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("MerchantId", merchantid));
            },
            r => new MerchantModel(r));
            return model;
        }

        public PagingData<MerchantModel> QueryMerchants(
            QueryFilter filter,
            PagingCondition condition)
        {
            
            return this.Query(condition, (builder) =>
            {
                builder.WithQueryFilter(filter);
                //builder.WithQueryOrder(filter);
            },
            (record) => Convert(record));
        }
        MerchantModel Convert(Merchant record)
        {
            var apply = this.WeChat.GetApplyProtocol();
            var model = new MerchantModel(record);
            var primary = apply.Categories.FirstOrDefault(o => o.PrimaryCategoryId.Equals(record.PrimaryCategoryId));
            var second = primary == null
            ? new SecondaryCategory()
            : primary.SecondaryCategories.FirstOrDefault(o => o.SecondaryCategoryId.Equals(record.SecondaryCategoryId));
            model.CategoryName = string.Join("/", new string[] {
                    primary==null ? "":primary.CategoryName,
                    second==null?"":second.CategoryName
                });
            return model;
        }

        BaseResponse IMerchantService.Delete(long id)
        {
            return base.Delete(id);
        }


    }
}