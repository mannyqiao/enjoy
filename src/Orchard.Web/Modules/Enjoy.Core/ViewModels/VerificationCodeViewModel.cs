using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.ViewModels
{
    using Newtonsoft.Json;
    public class VerificationCodeViewModel
    {

        public VerificationCodeViewModel(string mobile, string code)
        {
            this.Mobile = mobile;
            this.Code = code;
            this.CreatedAt = DateTime.UtcNow;
        }
        [JsonProperty("mobile")]
        public string Mobile { get; private set; }
        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; private set; }
        public bool Sended { get; private set; }
        public void SetSended()
        {
            if (this.Sended == false) this.Sended = true;
        }

    }
}