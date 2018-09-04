

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;
    using System;
    
    [Serializable]
    public class DateInfo
    {
        private ExpiryDateTypes dateType = ExpiryDateTypes.DATE_TYPE_FIX_TERM;
        [JsonProperty("type")]
        public string Type
        {
            get
            {
                return dateType.ToString();
            }
            set
            {
                if (Enum.TryParse<ExpiryDateTypes>(value, out ExpiryDateTypes type))
                {
                    dateType = type;
                    switch (type)
                    {
                        case ExpiryDateTypes.DATE_TYPE_FIX_TERM:
                            this.FixedBeginTerm = null;
                            this.FixedTerm = null;
                            break;
                        case ExpiryDateTypes.DATE_TYPE_FIX_TIME_RANGE:
                            this.BeginTimestamp = null;
                            this.EndTimestamp = null;
                            break;
                        case ExpiryDateTypes.DATE_TYPE_PERMANENT:
                            this.BeginTimestamp = null;
                            this.EndTimestamp = null;
                            this.FixedTerm = null;
                            this.FixedBeginTerm = null;
                            break;
                    }
                }
            }
        }


        [JsonProperty("begin_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long? BeginTimestamp { get; set; }


        [JsonProperty("end_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long? EndTimestamp { get; set; }


        [JsonProperty("fixed_term", NullValueHandling = NullValueHandling.Ignore)]
        public int? FixedTerm { get; set; }


        [JsonProperty("fixed_begin_term", NullValueHandling = NullValueHandling.Ignore)]
        public int? FixedBeginTerm { get; set; }


        [JsonIgnore]
        public string BeginTimeString
        {
            get
            {
                return (this.BeginTimestamp ?? DateTime.UtcNow.ToUnixStampDateTime()).ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd");
            }
            set
            {
                if (Enum.TryParse<ExpiryDateTypes>(this.Type, out ExpiryDateTypes type))
                {
                    switch (type)
                    {
                        case ExpiryDateTypes.DATE_TYPE_FIX_TERM:
                            this.BeginTimestamp = null;
                            break;
                        case ExpiryDateTypes.DATE_TYPE_FIX_TIME_RANGE:
                            this.BeginTimestamp = value.ToDateTime().ToUnixStampDateTime();
                            break;
                        case ExpiryDateTypes.DATE_TYPE_PERMANENT:
                            this.BeginTimestamp = null;
                            break;
                    }
                }

            }
        }

        [JsonIgnore]
        public string EndTimeString
        {
            get
            {
                return (this.EndTimestamp ?? DateTime.UtcNow.AddDays(30).ToUnixStampDateTime()).ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd");
            }
            set
            {
                if (Enum.TryParse<ExpiryDateTypes>(this.Type, out ExpiryDateTypes type))
                {
                    switch (type)
                    {
                        case ExpiryDateTypes.DATE_TYPE_FIX_TERM:
                            this.EndTimestamp = null;
                            break;
                        case ExpiryDateTypes.DATE_TYPE_FIX_TIME_RANGE:
                            this.EndTimestamp = value.ToDateTime().ToUnixStampDateTime();
                            break;
                        case ExpiryDateTypes.DATE_TYPE_PERMANENT:
                            this.EndTimestamp = null;
                            break;
                    }
                }
            }
        }

    }
}