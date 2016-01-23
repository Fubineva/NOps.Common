using System.IO;
using System.Threading;

using Newtonsoft.Json;

using Formatting = Newtonsoft.Json.Formatting;

namespace NOps.Common
{
    public class ConfigLoader : IConfigLoader
    {
        private static readonly JsonSerializerSettings s_serializerSettings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                };

        public T Load<T>(string filePathName)
        {
            using (var fileStream = new FileStream(filePathName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var config = Load<T>(fileStream);

                var iConfig = config as IConfig;
                iConfig?.SetFilePathName(filePathName);

                return config;
            }
        }

        public T Load<T>(Stream stream)
        {
            using (var textReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(textReader))
            {
                var deserializer = JsonSerializer.Create(s_serializerSettings);
                var config = deserializer.Deserialize<T>(reader);
                
                return config;
            }
        }
        
        public void Save<T>(T config, string filePathName)
        {
            // backslash is interpreted as visibility path, eliminate
            var mutex = new Mutex(false, "NOps.Config." + filePathName.Replace('\\', '_'));
            try
            {
                mutex.WaitOne(5000);
                SaveInner(config, filePathName);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        private static void SaveInner<T>(T config, string filePathName)
        {
            var iConfig = config as IConfig;
            iConfig?.SetFilePathName(filePathName);

            var serializer = JsonSerializer.Create(s_serializerSettings);
            using (var stream = new FileStream(filePathName, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, config);
            }
        }
    }
}
