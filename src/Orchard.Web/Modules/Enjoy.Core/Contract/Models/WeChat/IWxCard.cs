

namespace Enjoy.Core
{
    using System;
  
    public interface IWxCard
    {
        void Set(string path, object value);
        T Get<T>(string path);
    }
}
