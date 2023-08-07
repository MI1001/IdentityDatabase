// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nuget.IdentityDatabase
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = Path.Combine("indices.dat");
            string configurationPath = Path.Combine("Configuration.json");

            ContentJSON contentJSON = new();

            IDENTITY_DATABASE idb = new()
            {
                Defident = "Man",
                MultiplierA = "Socrates is a man",
                MultiplierB = "All men are mortal", // man are mortal
                Premise = { }
            };

            DATA_Сomposition_Premise trueStatement = new()
            {
                Proposition = "Socrates is mortal"
            };

            string json = JsonConvert.SerializeObject(idb, Formatting.Indented);            

            // Copyright
            Console.WriteLine("{0} | Identity Database | INFO | IDB | InitCore() Copyright © 2023 WG Element.",
                new DateTime(2023, 8, 2, 0, 0, 0, DateTimeKind.Utc));

            if (File.Exists(path) && File.Exists(configurationPath))
            {
                Console.WriteLine("{0} | Identity Database | INFO | IDB | Файл *.dat ({1}) уже существует. Конфигурационный файл ({2}) присутствует.",
                    DateTime.UtcNow, path, configurationPath);

                // Continue...


            }
            else
            {
                if (!File.Exists(path))
                {
                    StreamWriter sw = File.CreateText(path);

                    sw.Close();
                }
            }

            IDENTITY_DATABASE config_defaultGet = JsonConvert.DeserializeObject<IDENTITY_DATABASE>(json);
                        
            DATA_Сomposition configGet = JsonConvert.DeserializeObject<DATA_Сomposition>(contentJSON.Get_JSON(configurationPath, "data[0]"));

            int dfdCharLength = configGet.Defident.Length;

            string newString_Dfn1 = string.Empty;
            string newString_Dfn2 = string.Empty;

            if (index_Dfn1 > -1)
                newString_Dfn1 = configGet.MultiplierA.Remove(index_Dfn1, dfdCharLength);

            if (index_Dfn2 > -1)
                newString_Dfn2 = configGet.MultiplierB.Remove(index_Dfn2, dfdCharLength);

            Console.WriteLine("Dfd: {0}", configGet.Defident);
            Console.WriteLine("Dfn 1: {0}", configGet.MultiplierA);
            Console.WriteLine("Dfn 2: {0}", configGet.MultiplierB);
            Console.WriteLine("Осмысленное произведение речи: {0}", newString_Dfn1 + newString_Dfn2);
            _ = Console.ReadLine();
        }

        
    }

    // Error.

    /// <summary>
    /// error
    /// </summary>
    public partial class P_API_ERROR { }

    // JSON

    /// <summary>
    /// Работа с JSON.
    /// </summary>
    public partial class ContentJSON
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lacal path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Get_JSON(string lacal_path, string token)
        {
            return ParseJSON_SelectToken(
                GetResponse_TypeJSON(lacal_path),
                token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="local path"></param>
        /// <returns></returns>
        private string GetResponse_TypeJSON(string lacal_path)
        {
            string result;

            try
            {
                using StreamReader sr = new(lacal_path);
                return (result = sr.ReadToEnd());
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The configuration file could not be read:");
                Console.WriteLine(e.Message);

                return result = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">JSON content</param>
        /// <param name="jToken">JSON Node</param>
        /// <returns></returns>
        private string ParseJSON_SelectToken(string content, string jToken)
        {
            JObject obj = JObject.Parse(content);

            string data = string.Empty;

            try
            {
                data = obj.SelectToken(jToken).ToString();
            }
            catch
            {
                //do something
            }

            return data;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class IDENTITY_DATABASE
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        public bool? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("data", Required = Required.Always)]
        public List<DATA_Сomposition>? Data { get; set; }
    }

        /// <summary>
        /// 
        /// </summary>
    public partial class DATA_Сomposition
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Branch_A", Required = Required.Always)]
        public long[]? BranchA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Branch_B", Required = Required.Always)]
        public long[]? BranchB { get; set; }

    /// <summary>
    /// 
    /// </summary>
        [JsonProperty("Multiplier_A", Required = Required.Always)]
        public string? MultiplierA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Proposition { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    interface IConfigurationAppGet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsOK();
    }
}