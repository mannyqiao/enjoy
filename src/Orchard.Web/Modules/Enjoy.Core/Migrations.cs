


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
            var menuitem = this.OrchardServices.ContentManager.Create(typeof(MenuItem).Name);
            menuitem.As<MenuPart>().MenuText = "asd";
            menuitem.As<MenuPart>().Menu = menu;
            menuitem.As<MenuPart>().MenuPosition = "1";

            menuitem.As<MenuItemPart>().Url = "~/121323/d";
            foreach (var field in menuitem.As<MenuPart>().Fields)
            {

                switch (field.Name)
                {
                    case "FontAwesome":
                        ((TextField)field).Value = "Orchard is too hard.";
                        break;
                    case "Display":
                        ((BooleanField)field).Value = true;
                        break;
                }
            }

            return 2;
                 
        }
        //public int UpdateFrom1()
        //{
        //    var session = this.OrchardServices.TransactionManager.GetSession();
        //    var menu = this.OrchardServices.ContentManager.Get<MenuPart>(8);
 
        //    return 2;
        //}

    }
}