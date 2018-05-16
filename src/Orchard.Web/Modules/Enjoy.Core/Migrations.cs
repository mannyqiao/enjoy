


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
        public int UpdateFrom2()
        {
            CreateEnjoyDataSchema();
            return 3;
        }

        public void CreateEnjoyDataSchema()
        {
            SchemaBuilder.CreateTable("EnjoyUser", table => table
                    .Column("Id", System.Data.DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("Mobile", System.Data.DbType.String, column => column.WithLength(11).Unique().NotNull())
                    .Column("NickName", System.Data.DbType.String, column => column.WithLength(20))
                    .Column("WeChatId", System.Data.DbType.String, column => column.WithLength(50))
                    .Column("Gender", System.Data.DbType.Int32, column => column.NotNull().WithDefault(0))
                    .Column("Country", System.Data.DbType.String, column => column.WithLength(50).WithDefault("中国"))
                    .Column("Province", System.Data.DbType.String, column => column.WithLength(50).Nullable())
                    .Column("City", System.Data.DbType.String, column => column.WithLength(50).Nullable())
                    .Column("LastPassword", System.Data.DbType.String, column => column.WithLength(200))
                    .Column("LastSign", System.Data.DbType.DateTime)
                    .Column("LastUpdatedTime", System.Data.DbType.DateTime)
                    .Column("CreatedTime", System.Data.DbType.DateTime)
            );

            SchemaBuilder.CreateTable("Merchant", table => table
                .Column("Id", System.Data.DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("MerchantName", System.Data.DbType.String, column => column.WithLength(100).Unique().NotNull())
                .Column("Category", System.Data.DbType.String, column => column.WithLength(100))
                .Column("License", System.Data.DbType.String, column => column.WithLength(50))
                .Column("LicenseImageUrl", System.Data.DbType.String, column => column.WithLength(200).NotNull())
                .Column("Contact", System.Data.DbType.String, column => column.WithLength(20))
                .Column("AppId", System.Data.DbType.String, column => column.WithLength(50).Nullable())
                .Column("AppSecret", System.Data.DbType.String, column => column.WithLength(100).Nullable())
                .Column("EnjoyUser_Id", System.Data.DbType.Int32)// foreign key ， reference  EnjoyUser Id
                .Column("LastUpdatedTime", System.Data.DbType.DateTime, column => column.Nullable())
                .Column("CreatedTime", System.Data.DbType.DateTime, column => column.NotNull())
            );

            SchemaBuilder.CreateTable("MerchantAdmin", table => table
              .Column("Merchant_Id", System.Data.DbType.Int32)
              .Column("EnjoyUser_Id", System.Data.DbType.Int32)
            );
            SchemaBuilder.AlterTable("MerchantAdmin", table => table.AddUniqueConstraint("PK_MerchantAdmin", new string[] { "Merchant_Id", "EnjoyUser_Id" }));



        }
        private void CreateLayer()
        {
            var layer = this.OrchardServices.ContentManager.Create<LayerPart>(typeof(LayerPart).Name);
            layer.Name = "dashboard";
            layer.Description = "merchant background";
            layer.LayerRule = string.Join("or", new string[] {
                string.Format("url{\"0\"}","~/dashboard/*"),
                string.Format("url{\"0\"}","~/merchant/*")
            });
            //url("~/dashboard/*", "~/Merchant/*")

            //layer.LayerRule = string.Format("url({0})", string.Join(",", new string[] 
            //{
            //    "~/dashboard/*",
            //    "~/merchant/*"
            //}));

        }
        private void CreateMenuItem()
        {
            var menu = this.MenuService.GetMenus().FirstOrDefault();
            CreateMenuItem(menu, "商户概况", "0", "/dashboard/summary", "fa fa-dashboard fa-fw", true);

            CreateMenuItem(menu, "商户管理", "1", "javascript:void(0);", "fa fa-magic fa-fw", true);
            CreateMenuItem(menu, "新建商户", "1.1", "/merchant/create", "", false);
            CreateMenuItem(menu, "门店管理", "1.2", "/merchant/shop;", "", true);
            CreateMenuItem(menu, "绑定管理员", "1.3", "/merchant/muser;", "", true);
            CreateMenuItem(menu, "我的账户", "1.4", "/merchant/account", "", true);


            CreateMenuItem(menu, "卡券管理", "2", "javascript:void(0);", "fa fa-exchange fa-fw", true);
            CreateMenuItem(menu, "会员卡", "2.1", "/cards/menberc", "", false);
            CreateMenuItem(menu, "优惠券", "2.2", "/cards/coupon", "", true);
            CreateMenuItem(menu, "领用情况", "2.3", "/cards/muser;", "", true);

            CreateMenuItem(menu, "营销中心", "3", "javascript:void(0);", "fa fa-exchange fa-fw", true);
            CreateMenuItem(menu, "消息推送", "3.1", "", "/marketing/message", false);
            CreateMenuItem(menu, "智能投放", "3.2", "/marketing/ai;", "", true);
            CreateMenuItem(menu, "推广裂变", "3.3", "/marketing/pyramid;", "", true);
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