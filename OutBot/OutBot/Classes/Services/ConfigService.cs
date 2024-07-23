using Newtonsoft.Json;

namespace OutBot.Classes.Services
{
    /// <summary>
    /// Helper class to read and cache configuration values
    /// </summary>
    public class ConfigService
    {
        private Config? configCache;

        /// <summary>
        /// Instance of <see cref="Classes.Config"/>
        /// </summary>
        public Config Config => this.configCache ?? this.GetConfig();

        /// <summary>
        /// Reads a config file and parses its content to a <see cref="Config"/> instance. Also caches the result for
        /// the <see cref="Config"/> getter
        /// </summary>
        /// <returns>returns the parsed <see cref="Config"/> instance</returns>
        /// <exception cref="JsonException">Thrown if config file content cant be parsed into
        /// <see cref="Config"/> object</exception>
        private Config GetConfig()
        {
            string filePath = GetConfigPath();
            string fileText = File.ReadAllText(filePath);

            Config config = JsonConvert.DeserializeObject<Config>(fileText) ?? throw new JsonException("Unable to parse config file");

            this.configCache = config;
            return config;
        }

        /// <summary>
        /// Determined the config file path.
        /// "dev.appsettings.json" will be preferred over "appsettings.json"
        /// </summary>
        /// <returns>string that contains the full filepath of a config file</returns>
        /// <exception cref="FileNotFoundException">Thrown if neither of the config files exist</exception>
        private static string GetConfigPath()
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
