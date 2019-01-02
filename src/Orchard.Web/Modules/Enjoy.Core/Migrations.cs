


namespace Enjoy.Core
{
    using Orchard.Data.Migration;
    using Orchard;
    using Orchard.ContentManagement.MetaData;
    using Orchard.Core.Navigation.Models;
    using Orchard.Core.Contents.Extensions;
    using Orchard.ContentManagement;
    using Orchard.UI.Navigation;
    using Orchard.Core.Navigation.Services;
    using System.Linq;
    using Orchard.Core.Common.Fields;
    using Orchard.Fields.Fields;
    using Orchard.Widgets.Models;
    using System.Data;
    using Enjoy.Core.Records;
    using System;
    using Orchard.Security;
    using Enjoy.Core.WeChatModels;
    using System.Collections.Generic;

    public class Migrations : DataMigrationImpl
    {
        private readonly IOrchardServices OrchardServices;
        private readonly IEncryptionService EncryptionService;
        private readonly IMenuService MenuService;
        public Migrations(IOrchardServices orcahrd, IMenuService ms, IEncryptionService encryptionService)
        {
            this.OrchardServices = orcahrd;
            this.MenuService = ms;
            this.EncryptionService = encryptionService;
        }
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(typeof(MenuPart).Name,
                cfg => cfg.WithField("FontAwesome", builder => builder.OfType("TextField").WithDisplayName("FontAwesome"))
                         .WithField("Display", builder => builder.OfType("BooleanField").WithDisplayName("Display"))
                         .Attachable()
            );
            return 1;
        }

        public int UpdateFrom1()
        {
            CreateMenuItem();
            CreateLayer();
            CreateEnjoyDataSchema();
            return 2;
        }


        public void CreateEnjoyDataSchema()
        {
            //创建平台用户表
            SchemaBuilder.CreateTable("EnjoyUser", table => table
                    .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                    .Column("Mobile", DbType.String, column => column.WithLength(11).Unique().NotNull())
                    .Column("NickName", DbType.String, column => column.WithLength(20))
                    .Column("WxUser_Id", DbType.Int64, column => column.Nullable())
                    .Column("Password", DbType.String, column => column.WithLength(128))
                    .Column("LastPassword", DbType.String, column => column.WithLength(128))
                    .Column("Profile", DbType.String, column => column.WithLength(200))
                    .Column("CreatedTime", DbType.Int64)
                    .Column("LastActivityTime", DbType.Int64)
            );

            //平台商户表
            SchemaBuilder.CreateTable("Merchant", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Code", DbType.String, column => column.WithLength(24).NotNull().Unique())
                .Column("BrandName", DbType.String, column => column.WithLength(24).NotNull())
                .Column("EnjoyUser_Id", DbType.Int64)
                .Column("Contact", DbType.String, column => column.WithLength(36).Nullable())
                .Column("Mobile", DbType.String, column => column.WithLength(36).Nullable())
                .Column("Address", DbType.String, column => column.WithLength(128).Nullable())
                .Column("ErrMsg", DbType.String, column => column.WithLength(500).Nullable())
                .Column("CreateTime", DbType.Int64, column => column.NotNull())
                .Column("LastActivityTime", DbType.Int64, column => column.NotNull())
                .Column("Miniprogram", DbType.String, column => column.WithLength(500))
                .Column("Official", DbType.String, column => column.WithLength(500))
                .Column("Payment", DbType.String, column => column.WithLength(500))
            );
            //创建商户管理员
            SchemaBuilder.CreateTable("MerchantAdmin", table => table
              .Column("Merchant_Id", DbType.Int64)
              .Column("EnjoyUser_Id", DbType.Int64)
            );
            SchemaBuilder.AlterTable("MerchantAdmin", table => table.AddUniqueConstraint("PK_MerchantAdmin", new string[] { "Merchant_Id", "EnjoyUser_Id" }));

            //创建门店表
            SchemaBuilder.CreateTable("Shop", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Merchant_Id", DbType.Int64, column => column.Nullable())
                .Column("Pid", DbType.Int64, column => column.Nullable())
                .Column("ShopName", DbType.String, column => column.WithLength(120))
                .Column("Leader", DbType.String, column => column.WithLength(50))
                .Column("Longitude", DbType.Single)//经度
                .Column("Latitude", DbType.Single)//纬度                
                .Column("Address", DbType.String, column => column.WithLength(120))
                .Column("LastActivityTime", DbType.Int64)
            );
            SchemaBuilder.CreateTable("CardCoupon", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("BrandName", DbType.String, column => column.WithLength(20))//卡券类型
                .Column("Merchant_Id", DbType.Int64)
                .Column("Type", DbType.String, column => column.WithLength(20))//卡券类型
                .Column("WxNo", DbType.String, column => column.WithLength(40))//WeChat编号                
                .Column("Quantity", DbType.Int32, column => column.WithDefault(100))
                .Column("Status", DbType.String, column => column.WithLength(100))
                .Column("ErrMsg", DbType.String, column => column.WithLength(500).Nullable())
                .Column("CreatedTime", DbType.Int64)
                .Column("LastActivityTime", DbType.Int64)
                .Column("JsonMetadata", DbType.String, column => column.Unlimited())
            );

            SchemaBuilder.CreateTable("WxUser", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())                
                .Column("UnionId", DbType.String, column => column.WithLength(32))
                .Column("AppId", DbType.String, column => column.WithLength(32))
                .Column("OpenId", DbType.String, column => column.WithLength(32))
                .Column("Mobile", DbType.String, column => column.WithLength(11).Nullable())
                .Column("RegistryType", DbType.String, column => column.WithLength(20))
                .Column("NickName", DbType.String, column => column.WithLength(32))
                .Column("Country", DbType.String, column => column.WithLength(32))
                .Column("Province", DbType.String, column => column.WithLength(32))
                .Column("City", DbType.String, column => column.WithLength(32))
                .Column("CreatedTime", DbType.Int64)
                .Column("LastActivityTime", DbType.Int64)
                .Column("AvatarUrl", DbType.String, column => column.WithLength(200))
            );
            SchemaBuilder.AlterTable("WxUser", table => table
            .AddUniqueConstraint("UK_WxUser_AppIdOpenId", new string[] {
                "AppId","OpenId"
            }));


            SchemaBuilder.CreateTable("WxMsg", table => table
                    .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                    .Column("MsgType", DbType.String, column => column.WithLength(32))
                    .Column("FromUser", DbType.String, column => column.WithLength(32))
                    .Column("ToUser", DbType.String, column => column.WithLength(32))
                    .Column("LastActivityTime", DbType.Int64)
                    .Column("Metadata", DbType.String, column => column.Unlimited())
            );

            SchemaBuilder.CreateTable("Notification", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("EnjoyUser_Id", DbType.Int64)
                .Column("Title", DbType.String, column => column.WithLength(32))
                .Column("SendBySMS", DbType.Boolean, column => column.WithDefault(false))
                .Column("Read", DbType.Boolean, column => column.WithDefault(false))
                .Column("CreatedTime", DbType.Int64)
                .Column("LastActivityTime", DbType.Int64)
                .Column("Body", DbType.String, column => column.Unlimited())
            );

            SchemaBuilder.CreateTable("MerchantWxUser", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Merchant_Id", DbType.Int64)
                .Column("WxUser_Id", DbType.Int64)
                .Column("OpenId", DbType.String)
                .Column("LastActivityTime", DbType.Int64)
            );

            SchemaBuilder.CreateTable("WxUserCardCoupon", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Merchant_Id", DbType.Int64)
                .Column("Owner_Id", DbType.Int64)
                .Column("Gotfrom_Id", DbType.Int64, column => column.Nullable())
                .Column("CardCoupon_Id", DbType.Int64)
                .Column("UserCardCode", DbType.String, column => column.WithLength(32))
                .Column("OldUserCardCode", DbType.String, column => column.WithLength(32))
                .Column("IsGiveByFriend", DbType.Boolean)
                .Column("FriendUserName", DbType.String, column => column.WithLength(32))
                .Column("State", DbType.String, column => column.WithLength(32))
                .Column("Type", DbType.String, column => column.WithLength(32))
                .Column("IsGiftingToFriend", DbType.Boolean)
                .Column("ExtraInfo", DbType.String, column => column.Unlimited())
                .Column("LastActivityTime", DbType.Int64, column => column.Nullable())
            );

            SchemaBuilder.CreateTable("Category", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Name", DbType.String, column => column.WithLength(32).NotNull())
                .Column("Merchant_Id", DbType.Int64, column => column.NotNull())
                .Column("Settings", DbType.String, column => column.WithLength(2000))
                .Column("LastActivityTime", DbType.Int64)
            );

            SchemaBuilder.CreateTable("Product", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Name", DbType.String, column => column.WithLength(32).NotNull())
                .Column("Category_Id", DbType.Int64, column => column.NotNull())
                .Column("Merchant_Id", DbType.Int64, column => column.NotNull())
                .Column("Trades", DbType.Int64, column => column.NotNull())
                .Column("Price", DbType.Int64, column => column.NotNull())
                .Column("LastActivityTime", DbType.Int64)
                .Column("Settings", DbType.String, column => column.Unlimited())
            );



            //用户分享记录
            SchemaBuilder.CreateTable("SharingDetails", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Merchant_Id", DbType.Int64, column => column.NotNull())
                .Column("SharedBy", DbType.String, column => column.WithLength(32).NotNull())
                .Column("AppId", DbType.String, column => column.WithLength(32).NotNull())
                .Column("CardId", DbType.String, column => column.WithLength(32).NotNull())
                .Column("CreatedTime", DbType.Int64, column => column.NotNull())
            );
            //交易详情
            SchemaBuilder.CreateTable("TradeDetails", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("TradeId", DbType.String, column => column.WithLength(64).Unique())
                .Column("OrderId", DbType.String, column => column.WithLength(64).Unique())
                .Column("Type", DbType.String, column => column.WithLength(32))
                .Column("AppId", DbType.String, column => column.WithLength(32))
                .Column("OpenId", DbType.String, column => column.WithLength(32))
                .Column("MchId", DbType.String, column => column.WithLength(32))
                .Column("State", DbType.String,column=>column.WithLength(32).NotNull())
                .Column("Money", DbType.Int32)
                .Column("CreatedTime", DbType.Int64)
                .Column("ConfirmTime", DbType.Int64)
                .Column("Description", DbType.String, colum => colum.Unlimited())
            );        
        

        SchemaBuilder.CreateTable("VirtualAccount", table => table
                .Column("Id", DbType.Int64)
                .Column("AppId",DbType.String,column=>column.WithLength(32).NotNull())
                .Column("OpenId",DbType.String,column=>column.WithLength(32).NotNull())                
                .Column("CardId",DbType.String,column=>column.WithLength(32).NotNull())
                .Column("Code", DbType.String, column => column.WithLength(32).NotNull())
                .Column("Type",DbType.String,column=>column.WithLength(32).NotNull())
                .Column("State", DbType.String, column => column.WithLength(32).NotNull())
                .Column("Money", DbType.Int32, column => column.NotNull())
                .Column("TradeDetails_Id", DbType.Int64)
                .Column("LastUpdatedTime", DbType.Int64)
         );
         SchemaBuilder.AlterTable("VirtualAccount", table => table.AddUniqueConstraint("UK_VirtualAccount", 
             new string[] { "AppId", "OpenId", "CardId", "Code", "Type" }));
        }
        private void CreateLayer()
        {
            var layer = this.OrchardServices.ContentManager.Create<LayerPart>("Layer");
            layer.Name = "dashboard";
            layer.Description = "merchant background";
            layer.LayerRule = string.Join(" or ", new string[] {
                string.Format("url(\"{0}\")","~/dashboard/*"),
                string.Format("url(\"{0}\")","~/merchant/*"),
                string.Format("url(\"{0}\")","~/cards/*"),
                string.Format("url(\"{0}\")","~/marketing/*"),
                string.Format("url(\"{0}\")","~/finance/*")
            });

        }
        private void CreateMenuItem()
        {
            var menu = this.MenuService.GetMenus().FirstOrDefault();
            CreateMenuItem(menu, "商户概况", "0", "/dashboard/summary", "fa fa-dashboard fa-fw", true);

            CreateMenuItem(menu, "商户管理", "1", "javascript:void(0);", "fa fa-magic fa-fw", true);
            CreateMenuItem(menu, "我的商户", "1.1", "/merchant/mymerchant", "", false);
            CreateMenuItem(menu, "门店管理", "1.2", "/merchant/myshops", "", true);
            // CreateMenuItem(menu, "绑定管理员", "1.3", "/merchant/muser;", "", true);
            CreateMenuItem(menu, "商户账户", "1.4", "/finance/myaccount", "", true);
            CreateMenuItem(menu, "平台账户", "1.5", "/finance/paccount", "", true);

            CreateMenuItem(menu, "卡券中心", "2", "javascript:void(0);", "fa fa-exchange fa-fw", true);
            CreateMenuItem(menu, "卡券管理", "2.1", "/cards/coupon", "", false);
            CreateMenuItem(menu, "会员卡", "2.2", "/cards/mcard", "", true);


            CreateMenuItem(menu, "营销中心", "3", "javascript:void(0);", "fa fa-exchange fa-fw", true);
            CreateMenuItem(menu, "会员中心", "3.1", "", "/marketing/merbers", false);
            CreateMenuItem(menu, "消息推送", "3.2", "", "/marketing/message", false);
            CreateMenuItem(menu, "卡券投放", "3.3", "/marketing/publish", "", true);
            CreateMenuItem(menu, "推广裂变", "3.4", "/marketing/pyramid;", "", true);
        }
        private void CreateMenuItem(ContentItem owner, string text, string position, string url, string fontAwesome, bool display)
        {
            var item = this.OrchardServices.ContentManager.Create(typeof(MenuItem).Name);
            item.As<MenuPart>().Menu = owner;
            item.As<MenuPart>().MenuText = text;
            item.As<MenuPart>().MenuPosition = position;
            item.As<MenuItemPart>().Url = url;
            foreach (var field in item.As<MenuPart>().Fields)
            {

                switch (field.Name)
                {
                    case "FontAwesome":
                        ((TextField)field).Value = fontAwesome;
                        break;
                    case "Display":
                        ((BooleanField)field).Value = display;
                        break;
                }
            }
        }
        public int UpdateFrom2()
        {
            //导入柠檬工坊基础数据
            var session = this.OrchardServices.TransactionManager.GetSession();
            var enjoyUser = new EnjoyUser()
            {
                Mobile = "13961576298",
                NickName = "稻草人",
                WxUser = null,
                Password = this.EncryptionService.Ciphertext("Window2008"),
                LastPassword = this.EncryptionService.Ciphertext("Window2008"),
                Profile = string.Empty,
                CreatedTime = DateTime.Now.ToUnixStampDateTime(),
                LastActivityTime = DateTime.Now.ToUnixStampDateTime()
            };
            session.SaveOrUpdate(enjoyUser);
            var miniprogarm = new WeChatConfig("wx6a15c5888e292f99", "74c4c300a46b8c6eb8c79b3689065673",
                "1520961881", "EA62B75D5D3941C3A632B8F18C7B3575");
            var official = new WeChatConfig("wx20da9548445a2ca7", "8fd877e51aa338a2c660e35d1f876e70",
                "1520961881", "EA62B75D5D3941C3A632B8F18C7B3575");
            var payment = new Dictionary<string, string>();
            payment.Add("1520961881", "EA62B75D5D3941C3A632B8F18C7B3575");

            var merchant = new Merchant()
            {
                Address = "四川省眉山市东坡区东坡里商业水街2号楼14号",
                BrandName = "柠檬工坊",
                Code = "92511402MA6941EG0R",
                Contact = "刘丽群",
                CreateTime = DateTime.Now.ToUnixStampDateTime(),
                EnjoyUser = enjoyUser,
                LastActivityTime = DateTime.Now.ToUnixStampDateTime(),
                Miniprogram = miniprogarm.SerializeToJson(),
                Official = official.SerializeToJson(),
                Payment = payment.SerializeToJson(),
                Mobile = "13890397856"
            };
            session.SaveOrUpdate(merchant);
            var shop = new Shop()
            {
                Address = merchant.Address,
                Pid = 491431822,
                LastActivityTime = DateTime.Now.ToUnixStampDateTime(),
                Leader = "刘丽群",
                Merchant = merchant,
                ShopName = "柠檬工坊东坡店"
            };
            session.SaveOrUpdate(shop);
            return 3;
        }
    }
}
