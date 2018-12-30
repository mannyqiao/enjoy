

namespace Enjoy.Core
{
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WeChatModels = Enjoy.Core.WeChatModels;
    using Enjoy.Core.WeChatModels;

    public class ModelClient
    {
        public MerchantViewModel Convert(MerchantModel model, WeChatModels::ApplyProtocolWxResponse apply_protocol)
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
            viewModel.StartTimeString = model.BeginTime.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd");
            viewModel.EndTimeString = model.EndTime.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd");
            viewModel.Status = model.Status;
            return viewModel;
        }
        //public CardCounponModel Convert(CardCounponViewModel viewModel, MerchantModel merchant)
        //{

        //    var model = new CardCounponModel();
        //    viewModel.BaseInfo.LogoUrl = merchant.LogoUrl;
        //    viewModel.BaseInfo.Merchant = new SubMerchantInfo() { MerchantId = merchant.MerchantId ?? 0 };
        //    model.CreatedTime = viewModel.Id.Equals(0) ? DateTime.UtcNow.ToUnixStampDateTime() : viewModel.CreatedTime;
        //    model.Id = viewModel.Id;
        //    model.Merchant = merchant;
        //    //// Enjoy TOOD :need change model.BarandName to model.Title
        //    model.BrandName = viewModel.BaseInfo.Title;
        //    model.LastUpdateTime = DateTime.UtcNow.ToUnixStampDateTime();
        //    model.Quantity = (int)viewModel.BaseInfo.Sku.Quantity;
        //    model.Type = viewModel.CardType;
        //    model.WxNo = viewModel.WxNo;
        //    model.State = viewModel.State;

        //    switch (viewModel.CardType)
        //    {
        //        case CardTypes.CASH:
        //            model.CardCoupon = new WeChatModels::WxCardCoupon<ICardCoupon>()
        //            {
        //                Card = new WeChatModels::CashCoupon()
        //                {
        //                    CardCoupon = new WeChatModels::CashWapper()
        //                    {
        //                        BaseInfo = viewModel.BaseInfo,
        //                        AdvancedInfo = viewModel.AdvancedInfo,
        //                        LeastCost = viewModel.Cash.LeastCost,
        //                        ReduceCost = viewModel.Cash.ReduceCost
        //                    }
        //                }
        //            };
        //            break;
        //        case CardTypes.DISCOUNT:
        //            model.CardCoupon = new WeChatModels::WxCardCoupon<ICardCoupon>()
        //            {
        //                Card = new WeChatModels::DiscountCoupon()
        //                {
        //                    CardCoupon = new WeChatModels::DiscountWapper()
        //                    {
        //                        BaseInfo = viewModel.BaseInfo,
        //                        AdvancedInfo = viewModel.AdvancedInfo,
        //                        Discount = viewModel.Discount.Discount,
        //                    },

        //                }
        //            };
        //            break;
        //        case CardTypes.GENERAL_COUPON:
        //            model.CardCoupon = new WeChatModels::WxCardCoupon<ICardCoupon>()
        //            {
        //                Card = new WeChatModels::GeneralCoupon()
        //                {
        //                    CardCoupon = new WeChatModels::GeneralWapper()
        //                    {
        //                        BaseInfo = viewModel.BaseInfo,
        //                        AdvancedInfo = viewModel.AdvancedInfo,
        //                        DefaultDetail = viewModel.General.DefaultDetail
        //                    }

        //                }
        //            };
        //            break;
        //        case CardTypes.GIFT:
        //            model.CardCoupon = new WeChatModels::WxCardCoupon<ICardCoupon>()
        //            {
        //                Card = new WeChatModels::GiftCoupon()
        //                {
        //                    CardCoupon = new GiftWapper()
        //                    {
        //                        BaseInfo = viewModel.BaseInfo,
        //                        AdvancedInfo = viewModel.AdvancedInfo,
        //                        Gift = viewModel.Gift.Detail
        //                    }

        //                }
        //            };
        //            break;
        //        case CardTypes.GROUPON:
        //            model.CardCoupon = new WeChatModels::WxCardCoupon<ICardCoupon>()
        //            {
        //                Card = new WeChatModels::Groupon()
        //                {
        //                    CardCoupon = new WeChatModels::GrouponWapper()
        //                    {
        //                        BaseInfo = viewModel.BaseInfo,
        //                        AdvancedInfo = viewModel.AdvancedInfo,
        //                        DealDetail = viewModel.Groupon.Detail,
        //                    }
        //                }
        //            };
        //            break;
        //        case CardTypes.MEMBER_CARD:
        //            viewModel.BaseInfo.Dateinfo.Type = ExpiryDateTypes.DATE_TYPE_PERMANENT.ToString();
        //            viewModel.BaseInfo.CenterTitle = "快速买单";
        //            viewModel.BaseInfo.CustomUrlName = "分享赚积分";
        //            viewModel.BaseInfo.CustomUrlSubTitle = "戳我";
        //            viewModel.BaseInfo.CustomUrl = "";
        //            model.CardCoupon = new WeChatModels::WxCardCoupon<ICardCoupon>()
        //            {
        //                Card = new WeChatModels::MemberCard()
        //                {
        //                    CardCoupon = new WeChatModels::MerberCardWapper()
        //                    {
        //                        BaseInfo = viewModel.BaseInfo,
        //                        AdvancedInfo = viewModel.AdvancedInfo,
        //                        BackgroundPicUrl = viewModel.MerberCard.BackgroundPicUrl,
        //                        SupplyBanlance = true,
        //                        SupplyBonus = false,
        //                        ActivateUrl = "",
        //                        CustomCell = null,
        //                        BonusRule = new BonusRule() { },
        //                        //CustomField1 = new WeChat.CustomField()
        //                        //{
        //                        //    NameType = "FIELD_NAME_TYPE_LEVEL",
        //                        //    Url = "www.baidu.com"
        //                        //},
        //                        Prerogative = viewModel.MerberCard.Prerogative,
        //                        AutoActivate = true,
        //                        Discount = 90
        //                    }
        //                }
        //            };
        //            break;
        //    }
        //    model.CardCoupon.Card.Specific((baseInfo, advanceInfo) =>
        //    {

        //        if (Enum.TryParse<ExpiryDateTypes>(baseInfo.Dateinfo.Type, out ExpiryDateTypes type))
        //        {
        //            switch (type)
        //            {
        //                case ExpiryDateTypes.DATE_TYPE_FIX_TIME_RANGE:
        //                    if (DateTime.TryParse(viewModel.FixedExpiryDateDescriptor[0], out DateTime beginTime))
        //                    {
        //                        baseInfo.Dateinfo.BeginTimestamp = beginTime.ToUnixStampDateTime();
        //                    }

        //                    if (DateTime.TryParse(viewModel.FixedExpiryDateDescriptor[1], out DateTime endTime))
        //                    {
        //                        baseInfo.Dateinfo.EndTimestamp = endTime.ToUnixStampDateTime();
        //                    }

        //                    break;
        //                case ExpiryDateTypes.DATE_TYPE_FIX_TERM:
        //                case ExpiryDateTypes.DATE_TYPE_PERMANENT://会员卡 专用
        //                    break;
        //            }
        //        }
        //        baseInfo.BrandName = merchant.BrandName;
        //        baseInfo.CodeType = CodeTypes.CODE_TYPE_QRCODE.ToString();
        //        baseInfo.ServicePhone = merchant.Mobile;
        //        baseInfo.Source = "优享";
        //        baseInfo.Merchant.MerchantId = merchant.MerchantId ?? 0;
        //        advanceInfo.TextImageList = advanceInfo.TextImageList.Where(o => o.ImageUrl != null).ToList();
        //        //未设置的属性
        //        baseInfo.LocationIdList = new long[] { 3233, 333 };
        //        baseInfo.CenterSubTitle = "使用后立减10元";
        //        baseInfo.CenterUrl = "https://www.yourc.club/";
        //        baseInfo.CenterTitle = "立即使用";
        //        baseInfo.CustomUrlName = "分享赚积分";
        //        baseInfo.CustomUrl = "gh_e1543e2be86d@app";
        //        baseInfo.CustomUrlSubTitle = "customUrlSubTitle";
        //        baseInfo.PromotionUrlName = "更多优惠";
        //        baseInfo.PromotionUrl = "gh_e1543e2be86d@app";

        //        advanceInfo.Abstract = new WeChatModels::Abstract()
        //        {
        //            AbstractX = "xxx"
        //        };
        //        advanceInfo.TimeLimits = null;


        //    });
        //    return model;
        //}


        public WeChatModels::WxRequestWapper<WeChatModels::SubMerchant> Convert(Merchant merchant)
        {
            return new WeChatModels::WxRequestWapper<WeChatModels::SubMerchant>()
            {
                Info = new WeChatModels::SubMerchant()
                {
                    BrandName = merchant.BrandName,
                    //EndTime = merchant.EndTime,
                   // OperatorMediaId = merchant.OperatorMediaId,
                   // AgreementMediaId = merchant.AgreementMediaId,
                   // Protocol = merchant.AgreementMediaId,//// TODO: Need change in product envirment.
                   // LogoUrl = merchant.LogoUrl,
                   // PrimaryCategoryId = merchant.PrimaryCategoryId,
                  //  SecondaryCategoryId = merchant.SecondaryCategoryId
                }
            };
        }
        public IEnumerable<SelectNodeViewModel> Convert(WeChatModels::ApplyProtocolWxResponse response)
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

        public PagingData<ShopViewModel> Convert(PagingData<ShopModel> pagingData)
        {
            if (pagingData == null || pagingData.Items.Count().Equals(0))
            {
                return new PagingData<ShopViewModel>()
                {
                    Items = new List<ShopViewModel>()
                };
            }
            return new PagingData<ShopViewModel>()
            {
                Items = pagingData.Items.Select(o => new ShopViewModel(o)),
                Paging = pagingData.Paging,
                TotalCount = pagingData.TotalCount
            };
        }
    }
}