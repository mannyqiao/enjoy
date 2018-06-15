
namespace Enjoy.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class Miniprogram : IMiniprogram
    {
        public Miniprogram(string appid, string appsecrect)
        {
            this.AppId = appid;
            this.AppSecrect = appsecrect;
        }

        public string AppId { get; private set; }

        public string AppSecrect { get; private set; }
    }
}
