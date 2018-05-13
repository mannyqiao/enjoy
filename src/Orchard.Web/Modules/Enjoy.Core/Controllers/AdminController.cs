using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard;
using Orchard.Core.Navigation.Models;
using Orchard.Core.Navigation.Services;
using Orchard.Core.Title.Models;
using Orchard.Core.Common.Fields;
using Orchard.Projections.Models;
using Orchard.Core.Common.Settings;
using Orchard.Fields.Fields;

namespace Enjoy.Core.Controllers
{
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IOrchardServices OrchardServices;
        private readonly IMenuService MenuService;
        private readonly IMenuManager MenuManager;
        public AdminController(IOrchardServices services, IMenuService menuService, IMenuManager menumanager)
        {
            this.OrchardServices = services;
            this.MenuService = menuService;
            this.MenuManager = menumanager;
        }
        // GET: Admin
        public void Index()
        {
           
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}