using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class SelectNodeViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public SelectNodeViewModel[] Items { get; set; }
    }
}