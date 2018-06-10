

namespace Enjoy.Core
{
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WeChat = WeChat.Models;
    public class ModelClient
    {
        public MerchantViewModel Convert(MerchantModel model, ApplyProtocolWxResponse apply_protocol)
        {
            var viewModel = new MerchantViewModel();
            viewModel.Merchant = model;
            var idx = 0;
            (model.Address ?? "").Split('/').Select((name) =>
              {
                  switch (idx)
                  {
                      case 0:
                          viewModel.Province = name;
                          break;
                      case 1:
                          viewModel.City = name;
                          break;
                      case 2:
                          viewModel.Area = name;
                          break;
                  }
                  idx++;
                  return name;
              }).ToList();
            viewModel.OwnerId = model.EnjoyUser.Id;
            viewModel.ApplyProtocol = apply_protocol;

            return viewModel;
        }
        public CardCounponModel Convert(CardCounponViewModel viewModel, MerchantModel merchant)
        {
            var model = new CardCounponModel();
            model.CreatedTime = viewModel.CreatedTime;
            model.Id = viewModel.Id;
            model.Merchant = merchant;
            model.LastUpdateTime = DateTime.UtcNow.ToUnixStampDateTime();
            model.Quantity = (int)viewModel.BaseInfo.Sku.Quantity;
            model.Type = viewModel.CardType;
            model.WxNo = string.Empty;
            model.BrandName = viewModel.BaseInfo.BrandName;
         
            switch (viewModel.CardType)
            {
                case CardTypes.CASH:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::CashCoupon()
                        {
                            Coupon = new WeChat.Coupon()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo
                            },
                            LeastCost = viewModel.Cash.LeastCost ?? 0,
                            ReduceCost = viewModel.Cash.ReduceCost ?? 0

                        }
                    };
                    break;
                case CardTypes.DISCOUNT:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::DiscountCoupon()
                        {
                            Coupon = new WeChat.Coupon()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo
                            },
                            Discount = viewModel.Discount.Discount ?? 0,
                        }
                    };
                    break;
                case CardTypes.GENERAL_COUPON:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::GeneralCoupon()
                        {
                            Coupon = new WeChat.Coupon()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo
                            },
                            DefaultDetail = viewModel.General.DefaultDetail
                        }
                    };
                    break;
                case CardTypes.GIFT:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::GiftCoupon()
                        {
                            Coupon = new WeChat.Coupon()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo
                            },
                            Gift = viewModel.Gift.Detail
                        }
                    };
                    break;
                case CardTypes.GROUPON:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::Groupon()
                        {
                            Coupon = new WeChat.Coupon()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo
                            },
                            DealDetail = viewModel.Groupon.Detail,
                        }
                    };
                    break;
                case CardTypes.MEMBER_CARD:
                    break;
            }
            return model;
        }

        //public Merchant Convert(MerchantViewModel model, IEnjoyAuthService auth)
        //{
        //    return new Merchant()
        //    {
        //        AgreementMediaId = model.AgreementMediaId,
        //        AppId = string.Empty,
        //        BenginTime = DateTime.Now.ToUnixStampDateTime(),
        //        BrandName = model.BrandName,
        //        Contact = model.Contact,
        //        CreateTime = DateTime.Now.ToUnixStampDateTime(),
        //        EndTime = DateTime.Now.AddYears(1).ToUnixStampDateTime(),
        //        LogoUrl = model.LogoUrl,// model.LogoUrl,
        //        Mobile = model.Mobile,
        //        OperatorMediaId = model.OperatorMediaId,
        //        PrimaryCategoryId = model.PrimaryCategoryId,
        //        Protocol = model.Protocol,
        //        SecondaryCategoryId = model.SecondaryCategoryId,
        //        Status = MerchantStatus.CHECKING.ToString(),
        //        UpdateTime = DateTime.Now.ToUnixStampDateTime(),
        //        EnjoyUser = auth.GetAuthenticatedUser(),
        //        Address = string.Format("{0}/{1}/{2}", model.Province, model.City, model.Area)
        //    };
        //}
        public WxRequestWapper<SubMerchant> Convert(Merchant merchant)
        {
            return new WxRequestWapper<SubMerchant>()
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