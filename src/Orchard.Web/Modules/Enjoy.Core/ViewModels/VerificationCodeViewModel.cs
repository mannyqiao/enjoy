


namespace Enjoy.Core.ViewModels
{
    using Newtonsoft.Json;
    using System;
    using Enjoy.Core.EnjoyModels;
    public class VerificationCodeViewModel
    {

        public VerificationCodeViewModel(string mobile, string code)
        {
            this.Mobile = mobile;
            this.Code = code;
            this.CreatedAt = DateTime.Now;
            this.RequestCount = 0;
        }
        [JsonProperty("mobile")]
        public string Mobile { get; private set; }
        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; private set; }

        public int RequestCount { get; private set; }
        public VerificationCodeViewModel Request()
        {
            this.RequestCount++;
            return this;
        }
    }
}