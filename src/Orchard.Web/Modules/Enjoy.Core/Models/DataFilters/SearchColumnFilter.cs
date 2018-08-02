using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.EnjoyModels
{
    public class SearchColumnFilter
    {
        /// <summary>
        ///     全局的搜索条件的值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        ///     是否为正则表达式处理
        /// </summary>
        public bool Regex { get; set; }
    }
}