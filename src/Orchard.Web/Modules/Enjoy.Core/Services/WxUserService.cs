using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Services
{
    using Records = Enjoy.Core.Models.Records;
    using Models = Enjoy.Core.Models;
    using Orchard;
    using NHibernate.Criterion;


    public class WxUserService : QueryBaseService<Records::WxUser, Models::WxUserModel>, IWxUserService
    {
        public WxUserService(IOrchardServices os) : base(os) { }
        public override Type ModelType
        {
            get
            {
                return typeof(Models::WxUserModel);
            }

        }

        public Models.WxUserModel GetWxUser(long id)
        {
            return QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("Id", id));
            }, record => new Models.WxUserModel(record));
        }

        public Models.WxUserModel GetWxUser(string unionid)
        {
            return QueryFirstOrDefault((builder) =>
            {
                builder.Add(Expression.Eq("UnionId", unionid));
            }, record => new Models.WxUserModel(record));
        }

        public long Register(Models.WxUserModel userModel)
        {
            var model = new Models::WxUserModel();
            if (userModel.Id.Equals(0) == false && string.IsNullOrEmpty(userModel.UnionId) == false)
            {
                model = this.GetWxUser(userModel.UnionId);
                userModel.Id = model == null ? 0 : model.Id;
            }
            this.SaveOrUpdate(userModel,
                (wx) => { return new Models::BaseResponse(EnjoyConstant.Success); },
                RecordSetter);
            return userModel.Id;
        }

        public long Register(Models.WxUser userModel)
        {
            var model = new Models::WxUserModel(userModel);
            this.Register(model);
            return model.Id;
        }
        protected override void RecordSetter(Records::WxUser record, Models::WxUserModel model)
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