


namespace Enjoy.Core.ViewModels
{
    using Newtonsoft.Json;
    using System;
    using Enjoy.Core.Models;
    public class VerificationCodeViewModel
    {

        public VerificationCodeViewModel(string mobile, string code)
        {
            this.Mobile = mobile;
            this.Code = code;
            this.CreatedAt = DateTime.Now;
        }
        [JsonProperty("mobile")]
        public string Mobile { get; private set; }
        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; private set; }
        public bool Sended { get;  set; }
        public void SetSended()
        {
            if (this.Sended == false) this.Sended = true;
        }

    }
}