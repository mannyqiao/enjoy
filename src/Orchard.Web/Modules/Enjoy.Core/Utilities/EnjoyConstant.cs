﻿

namespace Enjoy.Core
{
    using System.Collections.Generic;
    public static class EnjoyConstant
    {
        public static readonly Dictionary<int, string> ErrorrCodeDescriptor = new Dictionary<int, string>()
        {
            { 0,"没有错误"},
            { -1,"错误描述未定义"},
            {1001,"手机号码已被占用!" },
            {1002,"手机号码不存在!" },
            {1003,"密码不正确!" },
            {1004,"两次密码输入不一致!" },
            {1005,"登录会话已过期,需要从新登录" },
            {1006,"数据源为空"},
            {1007,"密码不能为空"},
            {1008,"用户名和密码不匹配"},
            {1009,"输入有效性验证失败"}

        };
        public const int Success = 0;

        public const int ErrorMessageNotDefined = -1;
        public const int MobileExists = 1001;
        public const int MobileNotExists = 1002;
        public const int IncorrectPasword = 1003;
        public const int ConfirPasswordIncorrent = 1004;
        public const int SessionExpired = 1005;
        public const int EmptyOrNullDataSource = 1006;
        public const int PasswordCantBeNullOrEmpty = 1007;
        public const int UPasswordNotMatch = 1008;
        public const int VerifyFailed = 1009;
        public static readonly Dictionary<string, string> BusinessService = new Dictionary<string, string>()
        {
            {"BIZ_SERVICE_DELIVER","外卖服务" },
            {"BIZ_SERVICE_FREE_PARK","停车位"},
            {"BIZ_SERVICE_WITH_PET","可带宠物"},
            {"BIZ_SERVICE_FREE_WIFI","免费WIFI"}
        };


        public static readonly Dictionary<string, string> CouponBackgroundColors = new Dictionary<string, string>
        {
            {"Color010","#63b359"},
            {"Color020","#2c9f67"},
            {"Color030","#509fc9"},
            {"Color040","#5885cf"},
            {"Color050","#9062c0"},
            {"Color060","#d09a45"},
            {"Color070","#e4b138"},
            {"Color080","#ee903c"},
            {"Color081","#f08500"},
            {"Color082","#a9d92d"},
            {"Color090","#dd6549"},
            {"Color100","#cc463d"},
            {"Color101","#cf3e36"},
            {"Color102","#5E6671"}
        };
        public const int DefaultPageSize = 10;
#if DEBUG
        // UAT official  account "Enjoy.Vip@hotmail.com" 
        //var token = "EnjoyMini";
        //var encodingAESKey = "2f0utlUlEJCGJmpwGYDmX184OZpLGrHj7EXG2ynyThC";
        public static IMiniprogram Miniprogram = new Models.Miniprogram("wxeb6c176a36bb7b69", "5c8f7bacf759bfab19d0d1d821625c03");
#else
        //Product official account 
        public static IMiniprogram Miniprogram = new Models.Miniprogram("wxeb6c176a36bb7b69", "5c8f7bacf759bfab19d0d1d821625c03"); //         
#endif
        public const string WxBizMsgToken = "EnjoyMini";
        public const string EncodingAESKey = "2f0utlUlEJCGJmpwGYDmX184OZpLGrHj7EXG2ynyThC";
        public const string Directory_Media_Protocol_ROOT = "~/media/protocols";

        //短信平台 appid 以及  appkey
        public const int SMS_AppId = 1400108197;
        public const string SMS_AppKey = "1add466d18076fcbb47e0e3b28e55490";

        public const string PlatformName = "优享--微信会员卡营销平台";
        public const string PlatformShortName = "优享";

    }
}