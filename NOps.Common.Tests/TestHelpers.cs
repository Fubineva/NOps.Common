using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace NOps.Common.Tests
{
    public static class TestHelpers
    {
        public static string GetAppDir()
        {
            var appPath = PlatformServices.Default.Application.ApplicationBasePath;
            
            return Path.GetDirectoryName(appPath);
        }
    }
}