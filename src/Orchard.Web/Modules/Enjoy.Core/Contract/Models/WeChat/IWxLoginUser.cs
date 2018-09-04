

namespace Enjoy.Core
{
    /// <summary>
    /// 从https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN 接口返回的当前微信登录用户信息
    /// </summary>
    public interface IWxLoginUser
    {
        int Subscribe { get; }
        string Openid { get; }
        string Nickname { get; }
        int Sex { get; }
        string Language { get; }
        string City { get; }
        string Province { get; }
        string Country { get; }
        string Headimgurl { get; }
        long Subscribe_time { get; }
        string Unionid { get; }
        string Remark { get; }
        int Groupid { get; }
        int[] Tagid_list { get; }
        string Subscribe_scene { get; }
        int Qr_Scene { get; }
        string Qr_Scene_Str { get; }
        //{
        //    "subscribe": 1, 
        //    "openid": "o6_bmjrPTlm6_2sgVt7hMZOPfL2M", 
        //    "nickname": "Band", 
        //    "sex": 1, 
        //    "language": "zh_CN", 
        //    "city": "广州", 
        //    "province": "广东", 
        //    "country": "中国", 
        //    "headimgurl":"http://thirdwx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0",
        //    "subscribe_time": 1382694957,
        //    "unionid": " o6_bmasdasdsad6_2sgVt7hMZOPfL"
        //    "remark": "",
        //    "groupid": 0,
        //    "tagid_list":[128,2],
        //    "subscribe_scene": "ADD_SCENE_QR_CODE",
        //    "qr_scene": 98765,
        //    "qr_scene_str": ""
        //}
    }
}
