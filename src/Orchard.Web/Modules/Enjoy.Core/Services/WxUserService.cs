using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using Orchard;
    using NHibernate.Criterion;


    public class WxUserService : QueryBaseService<WxUser, WxUserModel>, IWxUserService
    {
        public WxUserService(IOrchardServices os) : base(os) { }
        public override Type ModelType
        {
            get
            {
                return typeof(WxUserModel);
            }

        }

        public WxUserModel GetWxUser(long id)
        {
            return QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));
            }, record => new WxUserModel(record));
        }

        public WxUserModel GetWxUser(string unionid)
        {
            return QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("UnionId", unionid));
            }, record => new WxUserModel(record));
        }

        public long Register(WxUserModel userModel)
        {
            var model = new WxUserModel();
            if (userModel.Id.Equals(0) == false && string.IsNullOrEmpty(userModel.UnionId) == false)
            {
                model = this.GetWxUser(userModel.UnionId);
                userModel.Id = model == null ? 0 : model.Id;
            }
            this.SaveOrUpdate(userModel,
                (wx) => { return new BaseResponse(EnjoyConstant.Success); },
                RecordSetter);
            return userModel.Id;
        }

        public long Register(WeChatModels.WxUser userModel)
        {
            var model = new WxUserModel(userModel);
            this.Register(model);
            return model.Id;
        }
        protected override void RecordSetter(WxUser record, WxUserModel model)
        {
            record.City = model.City;
            record.Country = model.Country;
            record.CreatedTime = model.CreatedTime;
            record.LastActivityTime = model.LastActiveTime ?? DateTime.Now.ToUnixStampDateTime();
            record.Mobile = model.Mobile;
            record.NickName = model.NickName;
            //record.OpenId = model.OpenId;
            record.Province = model.Province;
            record.RegistryType = model.RegistryType;
            record.UnionId = model.UnionId;

        }
    }
}