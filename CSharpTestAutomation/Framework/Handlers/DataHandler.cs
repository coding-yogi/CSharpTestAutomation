using System;
using System.IO;
using CSharpAutomationFramework.Framework.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSharpAutomationFramework.Framework.Handlers
{
    public class DataHandler
    {
        String env;
        String configFile;
        String dataFile;
        String parentFolder;

        public DataHandler(String configFileName, String dataFileName)
        {
            //Set env
            this.env = GetEnv();
            configFile = configFileName;
            dataFile = dataFileName;
            parentFolder = Generic.GetBasePath();
        }

        private String GetEnv()
        {
            String environment = Environment.GetEnvironmentVariable("envName").Trim().ToLower();
            if (environment.Length == 0)
            {
                Console.WriteLine("No environment set. Exiting...");
                Environment.Exit(-1); //Create an Invalid Exception Class later
            }
            else if (!environment.Equals("dev") && !environment.Equals("integration") && !environment.Equals("staging") && !environment.Equals("production"))
            {
                Console.WriteLine(environment + " is not a valid environment. Exiting...");
                Environment.Exit(-1);//Create an Invalid Exception Class later
            }

            return environment;
        }

        private JToken GetAppConfigJson()
        {
            String app_config_path = "Config/" + configFile;
            using (StreamReader reader = File.OpenText(parentFolder + "/" + app_config_path))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                return o["appconfig"];
            }
        }

        private JToken GetAppDataJson()
        {
            String appdata_file_path = "Data/" + dataFile;

            using (StreamReader reader = File.OpenText(parentFolder + "/" + appdata_file_path))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                return o["appdata"];
            }
        }

        public String GetAppConfig(String configKey)
        {
            try
            {
                JToken appConfig = GetAppConfigJson();
                return appConfig["env"][env][configKey].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex + " occurred while fetching value for key " + configKey);
                throw ex;
            }
        }

        public String GetAppData(String dataKey)
        {
            try
            {
                JToken appConfig = GetAppDataJson();
                return appConfig["env"][env][dataKey].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex + " occurred while fetching value for key " + dataKey);
                throw ex;
            }
        }
    }
}
