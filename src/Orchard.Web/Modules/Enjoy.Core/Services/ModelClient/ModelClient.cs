

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
            model.CreatedTime = viewModel.Id.Equals(0) ? DateTime.UtcNow.ToUnixStampDateTime() : viewModel.CreatedTime;
            model.Id = viewModel.Id;
            model.Merchant = merchant;
            //// Enjoy TOOD :need change model.BarandName to model.Title
            model.BrandName = viewModel.BaseInfo.Title;
            model.LastUpdateTime = DateTime.UtcNow.ToUnixStampDateTime();
            model.Quantity = (int)viewModel.BaseInfo.Sku.Quantity;
            model.Type = viewModel.CardType;
            model.WxNo = viewModel.WxNo;
            

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
                                AdvancedInfo = viewModel.AdvancedInfo,
                                LeastCost = viewModel.Cash.LeastCost ,
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
                            Coupon = new WeChat.Coupon()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                Discount = viewModel.Discount.Discount ,
                            },
                            
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
                            Coupon = new WeChat.Coupon()
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
                            Coupon = new WeChat.Coupon()
                            {
                                BaseInfo = viewModel.BaseInfo,
                                AdvancedInfo = viewModel.AdvancedInfo,
                                DealDetail = viewModel.Groupon.Detail,
                            }
                        }
                    };
                    break;
                case CardTypes.MEMBER_CARD:
                    break;
            }
            model.CardCouponWapper.Card.Specific((baseInfo, advanceInfo) =>
            {

                if (Enum.TryParse<ExpiryDateTypes>(baseInfo.Dateinfo.Type, out ExpiryDateTypes type))
                {
                    switch (type)
                    {
                        case ExpiryDateTypes.DATE_TYPE_FIX_TERM:


                            break;
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
                baseInfo.CenterSubTitle = "立即使用subtitle";
                baseInfo.CustomUrlName = "CustomUrlName";
                baseInfo.CustomUrl = "http://www.qq.com";
                baseInfo.CustomUrlSubTitle = "customUrlSubTitle";
                baseInfo.PromotionUrlName = "更多优惠";
                baseInfo.PromotionUrl = "http://www.badiuc.om";

                advanceInfo.Abstract = new WeChat.Abstract()
                {
                     AbstractX ="柠檬工坊推出更多东西，期待你的光临"
                };
                advanceInfo.TimeLimits = null;

            });
            return model;
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

    }
}