

namespace Enjoy.Core
{
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class ModelClient
    {

        public Merchant Convert(SubMerchantViewModel model, IEnjoyAuthService auth)
        {
            return new Merchant()
            {
                AgreementMediaId = model.AgreementMediaId,
                AppId = string.Empty,
                BenginTime = DateTime.Now.ToUnixStampDateTime(),
                BrandName = model.BrandName,
                Contact = model.Contact,
                CreateTime = DateTime.Now.ToUnixStampDateTime(),
                EndTime = DateTime.Now.AddYears(1).ToUnixStampDateTime(),
                LogoUrl = model.LogoUrl,// model.LogoUrl,
                Mobile = model.Mobile,
                OperatorMediaId = model.OperatorMediaId,
                PrimaryCategoryId = model.PrimaryCategoryId,
                Protocol = model.Protocol,
                SecondaryCategoryId = model.SecondaryCategoryId,
                Status = MerchantStatus.CHECKING.ToString(),
                UpdateTime = DateTime.Now.ToUnixStampDateTime(),
                EnjoyUser = auth.GetAuthenticatedUser(),
                Address = string.Format("{0}/{1}/{2}", model.Province, model.City, model.Area)
            };
        }
        public WapperWxRequest<SubMerchant> Convert(Merchant merchant)
        {
            return new WapperWxRequest<SubMerchant>()
            {
                Info = new SubMerchant()
                {
                    BrandName = merchant.BrandName,
                    EndTime = merchant.EndTime,
                    OperatorMediaId = merchant.OperatorMediaId,
                    AgreementMediaId = merchant.AgreementMediaId,
                    Protocol = merchant.AgreementMediaId,//// TODO: Need change in product envirment.
                    LogoUrl = merchant.LogoUrl,
                    PrimaryCategoryId = merchant.PrimaryCategoryId,
                    SecondaryCategoryId = merchant.SecondaryCategoryId
                }
            };
        }
        public IEnumerable<SelectNodeViewModel> Convert(ApplyProtocolWxResponse response)
        {
            return response.Categories.Select((ctx) =>
            {
                return new SelectNodeViewModel()
                {
                    Id = ctx.PrimaryCategoryId.ToString(),
                    Text = ctx.CategoryName,
                    Items = ctx.SecondaryCategories.Select((child) =>
                    {
                        return new SelectNodeViewModel()
                        {
                            Id = child.SecondaryCategoryId.ToString(),
                            Text = child.CategoryName,
                            Items = new SelectNodeViewModel[] { }
                        };
                    }).ToArray()

                };
            });
        }
        public MerchantAdmin Convert(Merchant merchant, EnjoyUser user)
        {
            return new MerchantAdmin()
            {
                Merchant = merchant,
                EnjoyUser = user
            };
        }

    }
}