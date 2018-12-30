using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.Records
{
    public class Pyramid
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 小程序Id
        /// </summary>
        public virtual string AppId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string OpenId { get; set; }   
        /// <summary>
        /// 
        /// </summary>
        public virtual string Parent { get; set; }
    }
}