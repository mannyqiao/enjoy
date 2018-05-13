
using Orchard.Localization;
using Orchard.UI.Navigation;
using Orchard;
namespace Enjoy.Core
{
    public class AdminMenu : Component, INavigationProvider
    {

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.Add(T("Settings"), settings => settings
                .Add(T("Enjoy"), "17", dashboard => dashboard
                    .Action("Index", "Admin", new { area = "Enjoy.Core" })));
        }
    }
}