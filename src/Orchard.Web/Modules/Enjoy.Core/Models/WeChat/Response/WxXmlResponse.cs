
namespace Enjoy.Core.WeChatModels
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    
    [Serializable]
    [XmlRoot("xml")]
    public class WxXmlResponse
    {
        private string return_code;
        [XmlElement("return_code")]
        public XmlCDataSection ReturnCode
        {
            get {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(return_code);
            }
            set {
                return_code = value.Value;
            }
        }
        private string return_msg;
        [XmlElement("return_msg")]
        public XmlCDataSection ReturnMsg {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(return_msg);
            }
            set
            {
                return_msg = value.Value;
            }
        }
    }
}