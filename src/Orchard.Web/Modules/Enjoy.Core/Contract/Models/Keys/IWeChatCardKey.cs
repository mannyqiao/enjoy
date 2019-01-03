
namespace Enjoy.Core
{
    public interface IWeChatCardKey
    {
        string AppId { get; }
        string OpenId { get; }
        string CardId { get; }
        string Code { get; }
    }
}