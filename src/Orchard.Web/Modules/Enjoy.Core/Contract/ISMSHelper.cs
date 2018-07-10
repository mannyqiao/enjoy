
namespace Enjoy.Core
{
    using Orchard;
    public interface ISMSHelper : IDependency
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        void Send(ISMSEntity entity);

    }
}
