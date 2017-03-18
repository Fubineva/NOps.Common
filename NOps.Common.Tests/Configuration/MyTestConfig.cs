using NOps.Common.Configuration;

namespace NOps.Common.Tests
{
    public class MyTestConfig : Config
    {
        public string MyStringValue;

        public long MyNumbericValue;

        public MyNestedTestConfig NestedConfig;
    }

    public class MyNestedTestConfig
    {
        public string NestedValue;
    }
}