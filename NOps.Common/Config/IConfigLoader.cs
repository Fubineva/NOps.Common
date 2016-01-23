using System.IO;

namespace NOps.Common
{
    public interface IConfigLoader
    {
        T Load<T>(string filePathName);

        T Load<T>(Stream stream);

        void Save<T>(T config, string filePathName);
    }
}