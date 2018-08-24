
namespace Enjoy.Core.ApiModels
{
    using Newtonsoft.Json;
    public class UserState
    {
        /// <summary>
        /// 是否注册
        /// </summary>
        [JsonProperty("signup")]
        public bool Signup { get; set; }
        /// <summary>
        /// 是否绑定手机号码
        /// </summary>
        [JsonProperty("hasMobile")]
        public bool HasMobile { get; set; }
    }
}