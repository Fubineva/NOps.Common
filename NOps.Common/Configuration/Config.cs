using System;
using System.Runtime.Serialization;

namespace NOps.Common.Configuration
{
    public abstract class Config
    {
        private static readonly IConfigLoader s_cfgLoader = new ConfigLoader();
        
        public static T Load<T>(string filePathName) where T : Config
        {
            var config = s_cfgLoader.Load<T>(filePathName);
            config.FilePathName = filePathName;
            return config;
        }
        
        [IgnoreDataMember]
        public string FilePathName { get; set; }

        public void Save(string filePathName)
        {
            FilePathName = filePathName;
        
            s_cfgLoader.Save(this, filePathName);
        }
        
        public void Save()
        {
            if (string.IsNullOrEmpty(FilePathName))
            {
                throw new Exception("This configuration has no FilePathName set, please set it or specify one explicitly.");
            }

            Save(FilePathName);
        }
        
    }
}