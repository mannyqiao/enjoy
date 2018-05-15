


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


            return 2;

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