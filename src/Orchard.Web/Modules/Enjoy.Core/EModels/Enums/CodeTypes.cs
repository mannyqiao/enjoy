using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    [Flags]
    public enum CodeTypes
    {
        /// <summary>
        /// 文 本 
        /// </summary>
        CODE_TYPE_TEXT = 1,
        /// <summary>
        /// 一维码
        /// </summary>
        CODE_TYPE_BARCODE = 2,
        /// <summary>
        /// 二维码
        /// </summary>
        CODE_TYPE_QRCODE = 3,
        /// <summary>
        /// 二维码无code显示
        /// </summary>
        CODE_TYPE_ONLY_QRCODE = 4,
        /// <summary>
        /// 一维码无code显示；
        /// </summary>
        CODE_TYPE_ONLY_BARCODE = 5,
        /// <summary>
        /// 不显示code和条形码类型
        /// </summary>
        CODE_TYPE_NONE = 6

        //码型： "CODE_TYPE_TEXT"文 本 ； "CODE_TYPE_BARCODE"一维码 "CODE_TYPE_QRCODE"二维码 "CODE_TYPE_ONLY_QRCODE", 二维码无code显示； "CODE_TYPE_ONLY_BARCODE", 一维码无code显示；CODE_TYPE_NONE， 不显示code和条形码类型
        //类别  字段名 适用核销方式
        //二维码/一维码显示code   CODE_TYPE_QRCODE/CODE_TYPE_BARCODE  适用于扫码/输码核销
        //二维码不显示code  CODE_TYPE_ONLY_QRCODE   仅适用于扫码核销
        //仅code类型 CODE_TYPE_TEXT  仅适用于输码核销
        //无code类型 CODE_TYPE_NONE  仅适用于线上核销，开发者须自定义跳转链接跳转至H5页面，允许用户核销掉卡券，自定义cell的名称可以命名为“立即使用”
    }
}