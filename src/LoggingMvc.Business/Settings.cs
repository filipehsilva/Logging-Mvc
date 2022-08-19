
namespace LoggingMvc.Business
{
    public static class Settings
    {
        //Valid types: json or sqlite
        public static string DatabaseType { get; private set; } = "json";
        public static string JsonPath { get; private set; } = "../logs.json";
    }
}
