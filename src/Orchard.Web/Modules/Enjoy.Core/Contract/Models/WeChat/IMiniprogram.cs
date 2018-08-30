

namespace Enjoy.Core
{
    using System;

    public interface IMiniprogram
    {
        string AppId { get; }
        string AppSecrect { get; }
        string MchId { get; set; }
        string Key { get; set; }
    }
}
