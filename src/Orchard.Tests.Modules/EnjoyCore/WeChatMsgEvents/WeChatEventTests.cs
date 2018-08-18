


namespace Orchard.Tests.Modules.EnjoyCore
{
    using NUnit.Framework;
    using Enjoy.Core.Records;
    using System.Globalization;
    using System.Threading;
    using Autofac;
    using NHibernate;
    using Enjoy.Core;
    using Enjoy.Core.Services;
   
    using Enjoy.Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using Orchard.Security;
    using Orchard.Users.Services;
    using Orchard.Mvc;
    using Orchard.Environment;
    using Orchard.Environment.Configuration;
    using Orchard.Caching;
    using Orchard.Security.Providers;
    using Orchard.ContentManagement;
    using Orchard.Tests.Stubs;
    using Orchard.ContentManagement.MetaData;
    using Moq;
    using Orchard.UI.Notify;
    using Orchard.Data;
    using Orchard.Tests.ContentManagement;
    using Orchard.DisplayManagement;
    using Orchard.DisplayManagement.Implementation;
    using Orchard.DisplayManagement.Descriptors;
    using Orchard.ContentManagement.Handlers;
    using Orchard.Tests.Modules.Comments.Services;
    using Orchard.Environment.Extensions;
    using Orchard.Tests.Modules.Users;
    using Orchard.Tests.Modules.ImportExport.Services;
    using Orchard.Environment.Descriptor;
    using Orchard.Environment.State;
    using Orchard.Messaging.Services;
    using Orchard.UI.PageClass;
    using Orchard.Services;
    using Orchard.Users.Events;
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.WeChatModels;

    [TestFixture]
    public class WeChatEventTests : DatabaseEnabledTestsBase
    {
        private IMerchantService _merchantService;
        private ICardCouponService _cardCouponService;
        private IEnumerable<IWeChatEventBehavior> _behaviors;
        private IVerifyCodeGenerator _verifyCodeGenerator;
        private IEnjoyAuthService _enjoyAuthService;
        private IWeChatMsgHandler _weChatMsgHandler;
        private long _merchantid;
        private string _cardid = Guid.NewGuid().ToString();
        protected override IEnumerable<Type> DatabaseTypes
        {
            get
            {
                return new Type[] {
                    typeof(EnjoyUser),//
                    typeof(Merchant),   //             
                    typeof(Shop),//
                    //typeof(Enjoy.Core.Records.ICardCoupon),//
                    typeof(Enjoy.Core.Records.WxUser),//
                    typeof(WxMsg),
                    //typeof(XNotification),
                    typeof(MerchantWxUser),
                    typeof(WxUserCardCoupon)
                };
            }
        }

        public override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultSslSettingsProvider>().As<ISslSettingsProvider>();
            builder.RegisterType<VerifyCodeGenerator>().As<IVerifyCodeGenerator>();
            builder.RegisterType<QCloudSMSHelper>().As<ISMSHelper>();
            builder.RegisterType<DefaultEncryptionService>().As<IEncryptionService>();
            builder.RegisterType<MerchantService>().As<IMerchantService>();
            builder.RegisterType<EnjoyAuthService>().As<IEnjoyAuthService>();
            builder.RegisterType<QCloudSMSHelper>().As<ISMSHelper>();
            builder.RegisterType<VerifyCodeGenerator>().As<IVerifyCodeGenerator>();
            builder.RegisterType<WeChatApi>().As<IWeChatApi>();
            builder.RegisterType<OrchardServices>().As<IOrchardServices>();
            builder.RegisterType<DefaultContentManager>().As<IContentManager>();
            builder.RegisterType<StubCacheManager>().As<ICacheManager>();
            builder.RegisterType<Signals>().As<ISignals>();
            builder.RegisterType<DefaultContentManagerSession>().As<IContentManagerSession>();
            builder.RegisterInstance(new Mock<IContentDefinitionManager>().Object);
            builder.RegisterInstance(new Mock<IAuthorizer>().Object);
            builder.RegisterInstance(new Mock<INotifier>().Object);
            builder.RegisterInstance(new Mock<IContentDisplay>().Object);
            builder.RegisterInstance(new Mock<IAuthenticationService>().Object);
            builder.RegisterType<OrchardServices>().As<IOrchardServices>();
            builder.RegisterType<DefaultShapeTableManager>().As<IShapeTableManager>();
            builder.RegisterType<DefaultShapeFactory>().As<IShapeFactory>();
            builder.RegisterType<StubWorkContextAccessor>().As<IWorkContextAccessor>();
            builder.RegisterType<DefaultContentQuery>().As<IContentQuery>();

            builder.RegisterType<StubExtensionManager>().As<IExtensionManager>();
            builder.RegisterType<DefaultEncryptionService>().As<IEncryptionService>();
            builder.RegisterInstance(ShellSettingsUtility.CreateEncryptionEnabled());
            builder.RegisterType<ProcessingEngineStub>().As<IProcessingEngine>();
            builder.RegisterType<StubShellDescriptorManager>().As<IShellDescriptorManager>();
            builder.RegisterType<DefaultShapeFactory>().As<IShapeFactory>();

            builder.RegisterInstance(new Mock<IShapeTableLocator>().Object);
            builder.RegisterInstance(new Mock<IShapeDisplay>().Object);
            builder.RegisterInstance(new Mock<IMessageService>().Object);
            builder.RegisterType<StubClock>().As<IClock>();
            builder.RegisterInstance(new Mock<IPageClassBuilder>().Object);
            builder.RegisterType<DefaultContentDisplay>().As<IContentDisplay>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.RegisterType<MembershipService>().As<IMembershipService>();

            var _stubContextAccessor = new StubHttpContextAccessor();
            builder.RegisterInstance(_stubContextAccessor).As<IHttpContextAccessor>();
            builder.RegisterInstance(new Mock<IUserEventHandler>().Object);
            builder.RegisterInstance(new Mock<IAppConfigurationAccessor>().Object);
            builder.RegisterType<WeChatMsgHandler>().As<IWeChatMsgHandler>();
            builder.RegisterType<CardCouponService>().As<ICardCouponService>();
            builder.RegisterType<WxUserService>().As<IWxUserService>();
            builder.RegisterTypes(new System.Type[] {
                typeof(AuditNotPassWeChatMsgBehavior),
                typeof(AuditPassWeChatMsgBehavior),
                typeof(CardPayOrderWeChatMsgBehavior),
                typeof(CardSkuRemindWeChatMsgBehavior),
                typeof(DoNothingWeChatMsgBehavior),
                typeof(MerchantAuditWeChatMsgBehavior),
                typeof(SubmitMemberCardWeChatMsgBehavior),
                typeof(UpdateMemberCardWeChatMsgBehavior),
                typeof(UserConsumeCardWechatMsgBehavior),
                typeof(UserDelCardWeChatMsgBehavior),
                typeof(UserEnterSessionFromCardWeChatMsgBehavior),
                typeof(UserGetCardWeChatMsgBehavior),
                typeof(UserGiftingCardWeChatMsgBehavior),
                typeof(UserPayFromPayCellWeChatMsgBehavior),
                typeof(UserViewCardWeChatMsgBehavior)
            })
            .As<IWeChatEventBehavior>();
        }
        public override void Init()
        {
            base.Init();
            var os = _container.Resolve<IOrchardServices>();
            _merchantService = _container.Resolve<IMerchantService>();
            _verifyCodeGenerator = _container.Resolve<IVerifyCodeGenerator>();
            long.TryParse(_verifyCodeGenerator.GenerateNewVerifyCode(), out _merchantid);
            _enjoyAuthService = _container.Resolve<IEnjoyAuthService>();
            _weChatMsgHandler = _container.Resolve<IWeChatMsgHandler>();
            _cardCouponService = _container.Resolve<ICardCouponService>();
            CreatingEnjoyUserAndMerchant();
            //CreatingCardCoupon();
        }
        [TearDown]
        public void TearDown()
        {
            if (_container != null)
                _container.Dispose();
        }

        [Test]
        public void MerchantAudit()
        {
            var xmlMsg =
@"<xml>
    <ToUserName><![CDATA[toUser]]></ToUserName>
    <FromUserName><![CDATA[FromUser]]></FromUserName>
    <CreateTime> 123456789 </CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[card_merchant_check_result]]></Event>
    <MerchantId>{0}</MerchantId>
    <IsPass>{1}</IsPass>
    <Reason><![CDATA[{2}]]></Reason>
</xml>";
            var passMsg = string.Format(xmlMsg, _merchantid, 1, string.Empty);
            _weChatMsgHandler.Handle(passMsg);
            var status = _merchantService.QueryMerchantStatus(_merchantid);
            Assert.AreEqual(AuditStatus.APPROVED, status.Info);

            var notPass = string.Format(xmlMsg, _merchantid, 0, "审核不通过");
            _weChatMsgHandler.Handle(notPass);
            status = _merchantService.QueryMerchantStatus(_merchantid);
            Assert.AreEqual(AuditStatus.REJECTED, status.Info);
        }
        [Test]
        public void CardCouponAudit()
        {
            var xmlMsg = @"
<xml>
   <ToUserName><![CDATA[toUser]]></ToUserName>
    <FromUserName><![CDATA[FromUser]]></FromUserName>
    <CreateTime>123456789</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[card_pass_check]]></Event> 
   <CardId><![CDATA[{0}]]></CardId>
    <RefuseReason><![CDATA[{1}]]></RefuseReason> 
</xml>
";
            string pass = string.Format(xmlMsg, _cardid, string.Empty);
            _weChatMsgHandler.Handle(pass);
            var card = this._cardCouponService.GetCardCounpon(1);
            Assert.AreEqual(CardCouponStates.Rejected, card.State);
        }

        public void CreatingEnjoyUserAndMerchant()
        {
            var verifyCode = _verifyCodeGenerator.GenerateNewVerifyCode();
            var signup = new SignUpViewModel()
            {
                Mobile = "13961576298",
                Password = "Window2008",
                ConfirmPassword = "Window2008",
                Signin = false,
                VerificationCode = verifyCode
            };
            _enjoyAuthService.SignUp(signup);
            var merchant = new MerchantViewModel()
            {
                Province = "四川",
                City = "成都",
                Area = "双流",
                OwnerId = 1,
                Status = AuditStatus.UnCommitted,
                StartTimeString = "2018-08-01",
                EndTimeString = "2019-08-01",
                Merchant = new MerchantModel()
                {
                    LogoUrl = Guid.NewGuid().ToString(),
                    Address = "四川省双流县",
                    AppId = string.Empty,
                    BrandName = "一品现捞",
                    AgreementMediaId = Guid.NewGuid().ToString(),
                    CategoryName = "美食/小吃",
                    BeginTime = DateTime.Now.ToUnixStampDateTime(),
                    CreateTime = DateTime.Now.ToUnixStampDateTime(),
                    EndTime = DateTime.Now.AddYears(3).ToUnixStampDateTime(),
                    Contact = "13961576298",
                    EnjoyUser = new EnjoyUserModel() { Id = 1 },
                    MerchantId = _merchantid,
                    OperatorMediaId = Guid.NewGuid().ToString(),
                    PrimaryCategoryId = 12345,
                    Protocol = Guid.NewGuid().ToString(),
                    Status = AuditStatus.CHECKING,
                    Mobile = "139615796298",
                    ErrMsg = string.Empty,
                    UpdateTime = DateTime.Now.ToUnixStampDateTime(),
                    SecondaryCategoryId = 333
                }
            };
            var result = this._merchantService.SaveOrUpdate(merchant.Merchant);
            Assert.AreEqual(result.ErrorCode, EnjoyConstant.Success);
        }
        public void CreatingCardCoupon()
        {
            
        }
    }

}
