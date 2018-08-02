

namespace Enjoy.Core
{
    using Enjoy.Core.Models.Records;
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WeChat = WeChat.Models;
    using WeChat.Models;

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
            viewModel.StartTimeString = model.BeginTime.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd");
            viewModel.EndTimeString = model.EndTime.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd");
            return viewModel;
        }
        public CardCounponModel Convert(CardCounponViewModel viewModel, MerchantModel merchant)
        {

            var model = new CardCounponModel();
            viewModel.BaseInfo.LogoUrl = merchant.LogoUrl;
            model.CreatedTime = viewModel.Key.Equals(0) ? DateTime.UtcNow.ToUnixStampDateTime() : viewModel.CreatedTime;
            model.Id = viewModel.Key;
            model.Merchant = merchant;
            //// Enjoy TOOD :need change model.BarandName to model.Title
            model.BrandName = viewModel.BaseInfo.Title;
            model.LastUpdateTime = DateTime.UtcNow.ToUnixStampDateTime();
            model.Quantity = (int)viewModel.BaseInfo.Sku.Quantity;
            model.Type = viewModel.CardType;
            model.WxNo = viewModel.WxNo;
            model.Status = viewModel.CCStatus;

            switch (viewModel.CardType)
            {
                case CardTypes.CASH:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::CashCoupon()
                        {
                            CardCoupon = new WeChat.CashWapper()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                LeastCost = viewModel.Cash.LeastCost,
                                ReduceCost = viewModel.Cash.ReduceCost
                            }
                        }
                    };
                    break;
                case CardTypes.DISCOUNT:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::DiscountCoupon()
                        {
                            CardCoupon = new WeChat.DiscountWapper()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                Discount = viewModel.Discount.Discount,
                            },

                        }
                    };
                    break;
                case CardTypes.GENERAL_COUPON:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::GeneralCoupon()
                        {
                            CardCoupon = new WeChat.GeneralWapper()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                DefaultDetail = viewModel.General.DefaultDetail
                            }

                        }
                    };
                    break;
                case CardTypes.GIFT:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::GiftCoupon()
                        {
                            CardCoupon = new WeChat.GiftWapper()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                Gift = viewModel.Gift.Detail
                            }

                        }
                    };
                    break;
                case CardTypes.GROUPON:
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::Groupon()
                        {
                            CardCoupon = new WeChat.GrouponWapper()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                DealDetail = viewModel.Groupon.Detail,
                            }
                        }
                    };
                    break;
                case CardTypes.MEMBER_CARD:
                    viewModel.BaseInfo.Dateinfo.Type = ExpiryDateTypes.DATE_TYPE_PERMANENT.ToString();
                    viewModel.BaseInfo.CenterTitle = "快速买单";
                    viewModel.BaseInfo.CustomUrlName = "分享赚积分";
                    viewModel.BaseInfo.CustomUrlSubTitle = "戳我";
                    viewModel.BaseInfo.CustomUrl = "";
                    model.CardCouponWapper = new WxCardCouponWapper<ICardCoupon>()
                    {
                        Card = new WeChat::MemberCard()
                        {
                            CardCoupon = new WeChat::MerberCardWapper()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                BackgroundPicUrl = viewModel.MerberCard.BackgroundPicUrl,
                                SupplyBanlance = true,
                                SupplyBonus = true,
                                ActivateUrl = "",
                                CustomCell = null,
                                //CustomField1 = new WeChat.CustomField()
                                //{
                                //    NameType = "FIELD_NAME_TYPE_LEVEL",
                                //    Url = "www.baidu.com"
                                //},
                                Prerogative = viewModel.MerberCard.Prerogative,
                                AutoActivate = true,
                                Discount = 90
                            }
                        }
                    };
                    break;
            }
            model.CardCouponWapper.Card.Specific((baseInfo, advanceInfo) =>
            {

                if (Enum.TryParse<ExpiryDateTypes>(baseInfo.Dateinfo.Type, out ExpiryDateTypes type))
                {
                    switch (type)
                    {
                        case ExpiryDateTypes.DATE_TYPE_FIX_TIME_RANGE:
                            if (DateTime.TryParse(viewModel.FixedExpiryDateDescriptor[0], out DateTime beginTime))
                            {
                                baseInfo.Dateinfo.BeginTimestamp = beginTime.ToUnixStampDateTime();
                            }

                            if (DateTime.TryParse(viewModel.FixedExpiryDateDescriptor[1], out DateTime endTime))
                            {
                                baseInfo.Dateinfo.EndTimestamp = endTime.ToUnixStampDateTime();
                            }

                            break;
                        case ExpiryDateTypes.DATE_TYPE_FIX_TERM:
                        case ExpiryDateTypes.DATE_TYPE_PERMANENT://会员卡 专用
                            break;
                    }
                }
                baseInfo.BrandName = merchant.BrandName;
                baseInfo.CodeType = CodeTypes.CODE_TYPE_QRCODE.ToString();
                baseInfo.ServicePhone = merchant.Mobile;
                baseInfo.Source = "微信卡券营销平台";
                advanceInfo.TextImageList = advanceInfo.TextImageList.Where(o => o.ImageUrl != null).ToList();
                //未设置的属性
                baseInfo.LocationIdList = new long[] { 3233, 333 };
                baseInfo.CenterSubTitle = "xxxxx";
                baseInfo.CenterUrl = "gh_63766b0fcc93@app";
                baseInfo.CustomUrlName = "xxx";
                baseInfo.CustomUrl = "wx6647cb456db305dd@app";
                baseInfo.CustomUrlSubTitle = "customUrlSubTitle";
                baseInfo.PromotionUrlName = "更多优惠";
                baseInfo.PromotionUrl = "wx6647cb456db305dd@app";

                advanceInfo.Abstract = new WeChat.Abstract()
                {
                    AbstractX = "柠檬工坊推出更多东西，期待你的光临"
                };
                advanceInfo.TimeLimits = null;

            });
            return model;
        }

        public CardCounponViewModel Convert(CardCounponModel model)
        {
            var viewModel = new CardCounponViewModel();
            viewModel.Key = model.Id;
            viewModel.CCStatus = model.Status;
            viewModel.WxNo = model.WxNo;
            viewModel.CardType = model.Type;
            viewModel.CreatedTime = model.CreatedTime;
            switch (model.Type)
            {
                case CardTypes.CASH:
                    {
                        var common = model.CardCouponWapper.Card as CashCoupon;
                        viewModel.BaseInfo = common.CardCoupon.BaseInfo;
                        viewModel.AdvancedInfo = common.CardCoupon.AdvancedInfo;
                        viewModel.Cash = new CashSpecific()
                        {
                            LeastCost = common.CardCoupon.LeastCost,
                            ReduceCost = common.CardCoupon.ReduceCost
                        };
                    }
                    break;
                case CardTypes.DISCOUNT:
                    {
                        var common = model.CardCouponWapper.Card as DiscountCoupon;
                        viewModel.BaseInfo = common.CardCoupon.BaseInfo;
                        viewModel.AdvancedInfo = common.CardCoupon.AdvancedInfo;
                        viewModel.Discount = new DiscountSpecific()
                        {
                            Discount = common.CardCoupon.Discount
                        };
                    }
                    break;
                case CardTypes.GENERAL_COUPON:
                    {
                        var common = model.CardCouponWapper.Card as GeneralCoupon;
                        viewModel.BaseInfo = common.CardCoupon.BaseInfo;
                        viewModel.AdvancedInfo = common.CardCoupon.AdvancedInfo;
                        viewModel.General = new GeneralCouponSpecific()
                        {
                            DefaultDetail = common.CardCoupon.DefaultDetail
                        };
                    }
                    break;
                case CardTypes.GIFT:
                    {
                        var common = model.CardCouponWapper.Card as GiftCoupon;
                        viewModel.BaseInfo = common.CardCoupon.BaseInfo;
                        viewModel.AdvancedInfo = common.CardCoupon.AdvancedInfo;
                        viewModel.Gift = new GiftSpecific()
                        {
                            Detail = common.CardCoupon.Gift
                        };
                    }
                    break;
                case CardTypes.GROUPON:
                    {
                        var common = model.CardCouponWapper.Card as Groupon;
                        viewModel.BaseInfo = common.CardCoupon.BaseInfo;
                        viewModel.AdvancedInfo = common.CardCoupon.AdvancedInfo;
                        viewModel.Groupon = new GrounponSpecific()
                        {
                            Detail = common.CardCoupon.DealDetail
                        };
                    }
                    break;
                case CardTypes.MEMBER_CARD:
                    {
                        var common = model.CardCouponWapper.Card as MemberCard;
                        viewModel.BaseInfo = common.CardCoupon.BaseInfo;
                        viewModel.AdvancedInfo = common.CardCoupon.AdvancedInfo;
                        viewModel.MerberCard = new MerberCardWapper()
                        {
                            BackgroundPicUrl = common.CardCoupon.BackgroundPicUrl,
                            Prerogative = common.CardCoupon.Prerogative,
                            ActivateUrl = common.CardCoupon.ActivateUrl,
                            AutoActivate = common.CardCoupon.AutoActivate,
                            BonusRule = common.CardCoupon.BonusRule,
                            CustomCell = common.CardCoupon.CustomCell,
                            AdvancedInfo = common.CardCoupon.AdvancedInfo,
                            BaseInfo = common.CardCoupon.BaseInfo,
                            CustomField1 = common.CardCoupon.CustomField1,
                            Discount = common.CardCoupon.Discount,
                            SupplyBanlance = common.CardCoupon.SupplyBanlance,
                            SupplyBonus = common.CardCoupon.SupplyBonus
                        };
                    }
                    break;
            }

            if (Enum.TryParse<ExpiryDateTypes>(viewModel.BaseInfo.Dateinfo.Type, out ExpiryDateTypes datetype))
            {
                switch (datetype)
                {

                    case ExpiryDateTypes.DATE_TYPE_FIX_TIME_RANGE:
                        viewModel.FixedExpiryDateDescriptor = new string[] {
                            (viewModel.BaseInfo.Dateinfo.BeginTimestamp??0).ToDateString(),
                            (viewModel.BaseInfo.Dateinfo.EndTimestamp??0).ToDateString()
                        };
                        break;
                    case ExpiryDateTypes.DATE_TYPE_FIX_TERM:
                    case ExpiryDateTypes.DATE_TYPE_PERMANENT:
                        break;
                }
            }          
            return viewModel;
        }
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