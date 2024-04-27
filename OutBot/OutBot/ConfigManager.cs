using Newtonsoft.Json;
using OutBot.Classes;

namespace OutBot
{
    public class ConfigManager
    {
        private Config? configcache;

        public Config Config
        {
            get
            {
                if (configcache == null) {
                    return this.getConfig();
                }
                else return configcache;
            }
        }

        private Config getConfig()
        {
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            string filePath = this.getConfigPath();
            string fileText = File.ReadAllText(filePath);

            Config config = JsonConvert.DeserializeObject<Config>(fileText) ?? throw new JsonException("Unable to parse config file");

            this.configcache = config;
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
