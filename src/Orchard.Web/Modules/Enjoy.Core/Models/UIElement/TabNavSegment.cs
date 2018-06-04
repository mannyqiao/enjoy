using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class TabNavSegment : ISegmentOption
    {
        public TabNavSegment(string id, string text, bool actived)
        {
            this.Id = id;
            this.Text = text;
            this.Actived = actived;
        }
        public bool Actived { get; set; }
        public string Id { get; set; }
        public string Text { get; }

        public string IdOf<TProperty>()
        {
            return this.Id;
        }

        public string NameOf<TProperty>()
        {
            return this.Id;
        }
    }
}