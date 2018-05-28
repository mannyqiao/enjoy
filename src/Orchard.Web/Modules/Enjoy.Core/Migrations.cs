


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


        public void CreateEnjoyDataSchema()
        {
            //创建平台用户表
            SchemaBuilder.CreateTable("EnjoyUser", table => table
                    .Column("Id", System.Data.DbType.Int32, column => column.PrimaryKey().Identity())
                    .Column("Mobile", System.Data.DbType.String, column => column.WithLength(11).Unique().NotNull())
                    .Column("NickName", System.Data.DbType.String, column => column.WithLength(20))
                    .Column("Password", System.Data.DbType.String, column => column.WithLength(128))
                    .Column("WeChatId", System.Data.DbType.String, column => column.WithLength(50))
                    .Column("Gender", System.Data.DbType.Int32)
                    .Column("Country", System.Data.DbType.String, column => column.WithLength(50).WithDefault("中国"))
                    .Column("Province", System.Data.DbType.String, column => column.WithLength(50).Nullable())
                    .Column("City", System.Data.DbType.String, column => column.WithLength(50).Nullable())
                    .Column("LastPassword", System.Data.DbType.String, column => column.WithLength(128))
                    .Column("LastSign", System.Data.DbType.Int64)
                    .Column("LastUpdatedTime", System.Data.DbType.Int64)
                    .Column("CreatedTime", System.Data.DbType.Int64)
            );

            //创建商户
            SchemaBuilder.CreateTable("Merchant", table => table
                .Column("Id", System.Data.DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("MerchantId", System.Data.DbType.Int32)
                .Column("EnjoyUser_Id", System.Data.DbType.Int32)
                .Column("BenginTime", System.Data.DbType.Int64)
                .Column("CreateTime", System.Data.DbType.Int64)
                .Column("UpdateTime", System.Data.DbType.Int64)
                .Column("Status", System.Data.DbType.String, column => column.WithLength(36))
                .Column("AppId", System.Data.DbType.String, column => column.WithLength(26))
                .Column("BrandName", System.Data.DbType.String, column => column.WithLength(36))
                .Column("LogoUrl", System.Data.DbType.String, column => column.WithLength(128))
                .Column("Protocol", System.Data.DbType.String, column => column.WithLength(26))
                .Column("EndTime", System.Data.DbType.Int64)
                .Column("PrimaryCategoryId", System.Data.DbType.Int32)
                .Column("SecondaryCategoryId", System.Data.DbType.Int32)
                .Column("AgreementMediaId", System.Data.DbType.String, column => column.WithLength(128))
                .Column("OperatorMediaId", System.Data.DbType.String, column => column.WithLength(128))
                .Column("Contact", System.Data.DbType.String, column => column.WithLength(36).Nullable())
                .Column("Mobile", System.Data.DbType.String, column => column.WithLength(36).Nullable())
                .Column("Address", System.Data.DbType.String, column => column.WithLength(128).Nullable())
            );

            //创建商户管理员
            SchemaBuilder.CreateTable("MerchantAdmin", table => table
              .Column("Merchant_Id", System.Data.DbType.Int32)
              .Column("EnjoyUser_Id", System.Data.DbType.Int32)
            );
            SchemaBuilder.AlterTable("MerchantAdmin", table => table.AddUniqueConstraint("PK_MerchantAdmin", new string[] { "Merchant_Id", "EnjoyUser_Id" }));

            //创建门店表
            SchemaBuilder.CreateTable("Shop", table =>table
                .Column("Id",System.Data.DbType.Int32)
                .Column("Merchant_Id",System.Data.DbType.Int32)
                .Column("ShopName",System.Data.DbType.String,column=>column.WithLength(120))                
                .Column("Coordinate",System.Data.DbType.String,column=>column.WithLength(50))
                .Column("Leader", System.Data.DbType.String, column => column.WithLength(50))
                .Column("Address", System.Data.DbType.String, column => column.WithLength(120))
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
            CreateMenuItem(menu, "新建商户", "1.1", "/merchant/create", "", false);
            CreateMenuItem(menu, "门店管理", "1.2", "/merchant/myshops", "", true);
           // CreateMenuItem(menu, "绑定管理员", "1.3", "/merchant/muser;", "", true);
            CreateMenuItem(menu, "商户账户", "1.4", "/finance/myaccount", "", true);
            CreateMenuItem(menu, "平台账户", "1.5", "/finance/paccount", "", true);

            CreateMenuItem(menu, "卡券管理", "2", "javascript:void(0);", "fa fa-exchange fa-fw", true);
            CreateMenuItem(menu, "创建卡券", "2.1", "/cards/create", "", false);            
            CreateMenuItem(menu, "卡券库存", "2.2", "/cards/sku", "", true);


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