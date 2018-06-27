using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core
{
    using Orchard;
    using System.IO;
    public static class DirectoryExtension
    {
        
        //public static void CreateIfNotExits(this string virtualPath,IOrchardServices os)
        //{
        //    var directory = Path.Combine(os.WorkContext.HttpContext.Server.MapPath("~/"), virtualPath);
        //    if (Directory.Exists(directory) == false)
        //    {
        //        Directory.CreateDirectory(directory);
        //    }
        //}
        public static void CreateMediaDirectoryIfNotExits(this IOrchardServices os,string virtualPath)
        {
            var directory = os.WorkContext.HttpContext.Server.MapPath(virtualPath); //Path.Combine(os.WorkContext.HttpContext.Server.MapPath("~/media"), virtualPath);
            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }
        }
        
    }
}