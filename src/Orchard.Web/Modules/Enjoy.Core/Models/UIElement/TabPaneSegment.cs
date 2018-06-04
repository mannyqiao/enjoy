﻿using Enjoy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.UIElements
{
    public class TabPaneSegment<TModel> : ISegmentOption
    {
        private readonly string text;
        public TabPaneSegment(string id, string text, bool isActived)
        {
            this.Id = id;
            this.text = text;
            this.Actived = isActived;
        }
        public string Id { get; private set; }
        public bool Actived { get; private set; }
        public string Text
        {
            get { return this.text; }
            

        }
        public UIElement<TModel>[] Elements { get; set; }

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