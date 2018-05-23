

namespace Enjoy.Core
{
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.ViewModels;
    using System;

    public class ModelClient
    {
     
        public Merchant Convert(CreatingSubMerchantViewModel model,IEnjoyAuthService auth)
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
                LogoUrl = string.Empty,// model.LogoUrl,
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
    }
}