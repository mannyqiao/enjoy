﻿using Orchard.Environment.Extensions;
using Orchard.Layouts.Framework.Elements;
using Orchard.Localization;

namespace Orchard.Layouts.Elements {
    [OrchardFeature("Orchard.Layouts.Snippets")]
    public class Snippet : Element {
        public override string Category {
            get { return "Snippets"; }
        }

        public override bool IsSystemElement {
            get { return true; }
        }

        public override bool HasEditor {
            get { return false; }
        }

        public override string ToolboxIcon {
            get { return "\uf10c"; }
        }
    }
}