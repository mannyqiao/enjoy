using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Common.Extensions
{
    public static class IWeChatConfigExtension
    {



        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
         * 1.证书文件不能放在web服务器虚拟目录，应放在有访问权限控制的目录中，防止被他人下载；
         * 2.建议将证书文件名改为复杂且不容易猜测的文件
         * 3.商户服务器要做好病毒和木马防护工作，不被非法侵入者窃取证书文件。
        */
        public static string GetSSlCertPath(this IWeChatConfig config)
        {
            return string.Empty;

        }
        public static string GetSSlCertPassword(this IWeChatConfig config)
        {
            return string.Empty;
        }



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public static string GetNotifyUrl(this IWeChatConfig config)
        {
            return string.Empty;
        }

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public static string GetIp(this IWeChatConfig config)
        {
            return string.Empty;
        }


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public static string GetProxyUrl(this IWeChatConfig config)
        {
            return string.Empty;
        }


        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public static int GetReportLevel(this IWeChatConfig config)
        {
            return 1;
        }


        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public static int GetLogLevel(this IWeChatConfig config)
        {
            return 1;
        }
    }
}