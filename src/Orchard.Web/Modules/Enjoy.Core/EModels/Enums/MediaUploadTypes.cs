using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    /// <summary>
    /// 素材上传类型 ，差别在与调用API接口不一样，一级素材用途不一致，返回值不一致
    /// </summary>
    public enum MediaUploadTypes
    {
        /// <summary>
        /// 普通输出，logo 等
        /// </summary>
        Material = 1,
        /// <summary>
        /// 认证素材
        /// </summary>
        AuthMaterial = 2
    }
}