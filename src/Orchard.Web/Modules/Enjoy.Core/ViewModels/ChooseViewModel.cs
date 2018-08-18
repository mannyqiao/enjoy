using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    public class ChooseViewModel
    {
        public CardTypes Selected { get; set; }
        public CardTypes[] Items { get; set; }
    }
}