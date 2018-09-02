

namespace Enjoy.Core
{
    using System;

    public interface IWeChatConfig
    {
        string AppId { get; }
        string AppSecrect { get; }
        string MchId { get; set; }
        string Key { get; set; }
    }
}
