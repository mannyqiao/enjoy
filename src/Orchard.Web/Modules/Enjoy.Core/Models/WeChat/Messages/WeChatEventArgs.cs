


namespace Enjoy.Core.WeChatModels
{
    using System.Xml.Serialization;
    [XmlRoot("xml")]
    public abstract class WeChatEventArgs : WeChatMsgModel
    {
        public EventTypes Event { get; set; }
        public string CardId { get; set; }
    }
}