using Enjoy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class TabContainerSegment<TModel> 
    {
        public TabContainerSegment(string id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
        public TabContainerSegment() { }
        public string Id { get; private set; }

        public string Text { get; private set; }
        public TabNavSegment[] TabNavs { get; set; }
        public TabPaneSegment<TModel>[] TabPanes { get; set; }
    }
}