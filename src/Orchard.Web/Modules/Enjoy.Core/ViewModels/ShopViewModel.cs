

namespace Enjoy.Core.ViewModels
{
    using Enjoy.Core.Records;
    using Enjoy.Core.EnjoyModels;
    using Newtonsoft.Json;
    using Enjoy.Core.WeChatModels;

    public class ShopViewModel
    {
        public ShopViewModel(ShopModel model)
        {
            this.ShopModel = model;
            this.Address = new AddressViewModel(model.Address);
        }
        public ShopModel ShopModel { get; set; }       
        public string AddressInfo
        {
            get
            {
                return this.ShopModel.Address == null
                    ? string.Empty
                    : string.Join("/", new string[] {
                        this.Address.Province,
                        this.Address.City,
                        this.Address.Area
                    });

            }
        }
        [JsonIgnore]
        public AddressViewModel Address { get; set; }
        [JsonIgnore]
        public ApplyProtocolWxResponse Protocol { get; set; }

    }
}