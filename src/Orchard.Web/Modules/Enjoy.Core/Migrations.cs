


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
    public class Migrations : DataMigrationImpl
    {
        private readonly IOrchardServices OrchardServices;
        private readonly IMenuService MenuService;
        public Migrations(IOrchardServices orcahrd, IMenuService ms)
        {
            this.OrchardServices = orcahrd;
            this.MenuService = ms;
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

            //创建商户
            SchemaBuilder.CreateTable("Merchant", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("MerchantId", DbType.Int64)
                .Column("EnjoyUser_Id", DbType.Int64)
                .Column("AppId", DbType.String, column => column.WithLength(26))
                .Column("BrandName", DbType.String, column => column.WithLength(36))
                .Column("LogoUrl", DbType.String, column => column.WithLength(128))
                .Column("BeginTime", DbType.Int64)
                .Column("EndTime", DbType.Int64)
                .Column("Status", DbType.String, column => column.WithLength(36).WithDefault(AuditStatus.UnCommitted.ToString()))
                .Column("Protocol", DbType.String, column => column.WithLength(128))
                .Column("PrimaryCategoryId", DbType.Int32)
                .Column("SecondaryCategoryId", DbType.Int32)
                .Column("AgreementMediaId", DbType.String, column => column.WithLength(128))
                .Column("OperatorMediaId", DbType.String, column => column.WithLength(128))
                .Column("Contact", DbType.String, column => column.WithLength(36).Nullable())
                .Column("Mobile", DbType.String, column => column.WithLength(36).Nullable())
                .Column("Address", DbType.String, column => column.WithLength(128).Nullable())
                .Column("ErrMsg", DbType.String, column => column.WithLength(500).Nullable())
                .Column("CreateTime", DbType.Int64)
                .Column("LastActivityTime", DbType.Int64)
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
                .Column("ShopName", DbType.String, column => column.WithLength(120))
                .Column("Leader", DbType.String, column => column.WithLength(50))
                .Column("Coordinate", DbType.String, column => column.WithLength(50))
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
                .Column("JsonMetadata", DbType.String, column => column.Unlimited())
                .Column("CreatedTime", DbType.Int64)
                .Column("LastActivityTime", DbType.Int64)
            );
            SchemaBuilder.CreateTable("WxUser", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("UnionId", DbType.String, column => column.WithLength(32))
                .Column("Mobile", DbType.String, column => column.WithLength(11).Nullable())
                .Column("RegistryType", DbType.String, column => column.WithLength(20))
                .Column("NickName", DbType.String, column => column.WithLength(32))
                .Column("Country", DbType.String, column => column.WithLength(32))
                .Column("Province", DbType.String, column => column.WithLength(32))
                .Column("City", DbType.String, column => column.WithLength(32))
                .Column("CreatedTime", DbType.Int64)
                .Column("LastActivityTime", DbType.Int64)
            );


            SchemaBuilder.CreateTable("WxMsg", table => table
                    .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                    .Column("MsgType", DbType.String, column => column.WithLength(32))
                    .Column("FromUser", DbType.String, column => column.WithLength(32))
                    .Column("ToUser", DbType.String, column => column.WithLength(32))
                    .Column("Metadata", DbType.String, column => column.Unlimited())
                    .Column("LastActivityTime", DbType.Int64)
            );

            SchemaBuilder.CreateTable("Notification", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("EnjoyUser_Id", DbType.Int64)
                .Column("Title", DbType.String, column => column.WithLength(32))
                .Column("SendBySMS", DbType.Boolean, column => column.WithDefault(false))
                .Column("Read", DbType.Boolean, column => column.WithDefault(false))
                .Column("Body", DbType.String, column => column.Unlimited())
                .Column("CreatedTime", DbType.Int64)
                .Column("LastActivityTime", DbType.Int64)
            );

            SchemaBuilder.CreateTable("MerchantWxUser", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Merchant_Id", DbType.Int64)
                .Column("WxUser_Id", DbType.Int64)
                .Column("OpenId", DbType.String)
                .Column("LastActivityTime", DbType.Int64)
            );

            SchemaBuilder.CreateTable("WxUserCoupon", table => table
                .Column("Id", DbType.Int64, column => column.PrimaryKey().Identity())
                .Column("Merchant_Id", DbType.Int64)
                .Column("Owner_Id", DbType.Int64)
                .Column("Gotfrom_Id", DbType.Int64, column => column.Nullable())
                .Column("OwnCardCoupon", DbType.Int64)
                .Column("UserCardCode", DbType.String, column => column.WithLength(32))
                .Column("OldUserCardCode", DbType.String, column => column.WithLength(32))
                .Column("IsGiveByFriend", DbType.Boolean)
                .Column("FriendUserName", DbType.String, column => column.WithLength(32))
                .Column("State", DbType.String, column => column.WithLength(32))
                .Column("Type", DbType.String, column => column.WithLength(32))
                .Column("ExtraInfo", DbType.String, column => column.Unlimited())                
                .Column("LastActivityTime", DbType.Int64, column => column.Nullable())
            );

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
    }
}
