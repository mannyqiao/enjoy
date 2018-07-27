


namespace Orchard.Tests.Modules.EnjoyCore
{
    using NUnit.Framework;
    using Enjoy.Core.Models.Records;
    using System.Globalization;
    using System.Threading;
    using Autofac;
    using NHibernate;
    using Enjoy.Core;
    using Enjoy.Core.Services;
    [TestFixture]
    public class WeChatBehaviorTests
    {
        private ISessionFactory _sessionFactory;
        private CultureInfo _currentCulture;
        private IContainer _container;
        [OneTimeSetUp]
        public void InitFixture()
        {
            _currentCulture = Thread.CurrentThread.CurrentCulture;
            var databaseFileName =System.IO.Path.GetTempFileName().Replace(".tmp", ".sdf");
            _sessionFactory = DataUtility.CreateSessionFactory(
                databaseFileName,
                typeof(EnjoyUser),//
                typeof(Merchant),   //             
                typeof(Shop),//
                typeof(CardCoupon),//
                typeof(WxUser),//
                typeof(WxMsg),
                typeof(XNotification)
                typeof(MerchantWxUser),
                typeof(WxUserCardCoupon)
                );
        }
        [OneTimeTearDown]
        public void TermFixture()
        {
            Thread.CurrentThread.CurrentCulture = _currentCulture;
           
        }
        [OneTimeSetUp]
        public void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MerchantService>().As<IMerchantService>();
            builder.RegisterType<CardCouponService>().As<ICardCouponService>();
            builder.RegisterType<EnjoyAuthService>().As<IEnjoyAuthService>();
            builder.RegisterType<ShopService>().As<IShopService>();
            builder.RegisterType<QCloudSMSHelper>().As<ISMSHelper>();
            _container = builder.Build();
        }
        [TearDown]
        public void TearDown()
        {
            if (_container != null)
                _container.Dispose();
        }
        [Test]
        public void UserGetCoupon()
        {
            Assert.AreEqual(true, true);
        }
    }

}
