/*
 * 微信门店文档地址   //https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444378120
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.WeChatModels
{
    using Newtonsoft.Json;


    public class WxShopBaseInfo
    {
        [JsonProperty("sid")]
        public string Sid { get; set; }


        [JsonProperty("business_name")]
        public string BusinessName { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty("district")]
        public string District { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [JsonProperty("address")]
        public string address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [JsonProperty("categories")]
        public string[] Categories { get; set; }

        [JsonProperty("offset_type")]
        public int OffsetType { get; set; }

        public float Longitude { get; set; }
        public float Latitude { get; set; }

        //[{"photo_url":"https:// 不超过20张.com"}，{"photo_url":"https://XXX.com"}],
        [JsonProperty("photo_list")]
        public WxShopPhoto[] PhotoList { get; set; }

        /// <summary>
        /// 门店推荐
        /// </summary>
        [JsonProperty("recommend")]
        public string Recommend { get; set; }

        /// <summary>
        /// 门店特色
        /// </summary>
        [JsonProperty("special")]
        public string Special { get; set; }

        /// <summary>
        /// 门店介绍
        /// </summary>
        [JsonProperty("introduction")]
        public string Introduction { get; set; }

        /// <summary>
        /// 营业时间
        /// </summary>
        public string OpenTime { get; set; }
        /// <summary>
        /// 人均消费
        /// </summary>
        [JsonProperty("avg_price")]
        public int AvgPrice { get; set; }
        //"longitude":115.32375,
        //"latitude":25.097486,

        //        {"business":{
        //"base_info":{
        //"sid":"33788392",
        //"business_name":"15个汉字或30个英文字符内",
        //"branch_name":"不超过10个字，不能含有括号和特殊字符",
        //"province":"不超过10个字",
        //"city":"不超过30个字",
        //"district":"不超过10个字",
        //"address":"门店所在的详细街道地址（不要填写省市信息）：不超过80个字",
        //"telephone":"不超53个字符（不可以出现文字）",
        //"categories":["美食,小吃快餐"],
        //"offset_type":1,
        //"longitude":115.32375,
        //"latitude":25.097486,
        //"photo_list":[{"photo_url":"https:// 不超过20张.com"}，{"photo_url":"https://XXX.com"}],
        //"recommend":"不超过200字。麦辣鸡腿堡套餐，麦乐鸡，全家桶",
        //"special":"不超过200字。免费wifi，外卖服务",
        //"introduction":"不超过300字。麦当劳是全球大型跨国连锁餐厅，1940 年创立于美国，在世界上大约拥有3 万间分店。
        //主要售卖汉堡包，以及薯条、炸鸡、汽水、冰品、沙拉、 水果等快餐食品",
        //"open_time":"8:00-20:00",
        //"avg_price":35
        //}
        //}
        //}
    }
}