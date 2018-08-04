

namespace Enjoy.Core.WeChatModels
{
    using System.Xml.Serialization;
    [XmlRoot("xml")]
    public class SubmitMemberCardWeChatEventArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }
    }
}