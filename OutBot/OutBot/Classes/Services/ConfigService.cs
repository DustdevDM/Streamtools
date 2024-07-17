using Newtonsoft.Json;

namespace OutBot.Classes.Services
{
    public class ConfigService
    {
        private Config? configcache;

        public Config Config
        {
            get
            {
                if (configcache == null)
                {
                    return getConfig();
                }
                else return configcache;
            }
        }

        private Config getConfig()
        {
            string filePath = getConfigPath();
            string fileText = File.ReadAllText(filePath);

            Config config = JsonConvert.DeserializeObject<Config>(fileText) ?? throw new JsonException("Unable to parse config file");

            configcache = config;
            return config;
        }

        private string getConfigPath()
        {
            if (File.Exists("dev.appsettings.json"))
            {
                return Path.GetFullPath("dev.appsettings.json");
            }
            else if (File.Exists("appsettings.json"))
            {
                return Path.GetFullPath("appsettings.json");
            }
            else
            {
                throw new FileNotFoundException("Unable to find configuration");
            }
        }
    }
}
