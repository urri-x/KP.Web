using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KP.WebApi.Helpers
{
    public class ServerPathProvider
    {
        public static string MapPath(string virtualPath)
        {
            var path = virtualPath.StartsWith("~")? virtualPath.Substring(2) : virtualPath;
            return Path.Combine(AssemblyDirectory,path);
        }
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

    }
}
