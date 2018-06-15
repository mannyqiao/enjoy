

namespace Enjoy.Core
{
    using System;
    using WeChat.Models;
    public interface IWxCard
    {
        void Set(string path, object value);
        T Get<T>(string path);
    }
}
