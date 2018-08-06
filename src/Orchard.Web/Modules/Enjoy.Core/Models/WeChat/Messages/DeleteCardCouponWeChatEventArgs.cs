


namespace Enjoy.Core.WeChatModels
{
    using System.Xml.Serialization;
    [XmlRoot("xml")]
    public class DeleteCardCouponWeChatEventArgs : WeChatEventArgs
    {
        public string UserCardCode { get; set; }

    }
}