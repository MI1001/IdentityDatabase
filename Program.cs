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
            Сomposition comp1 = new()
            {
                Multiplier_A = "Socrates is a man",
                Multiplier_B = "All men are mortal", // man are mortal
                Premise = { }
            };

            TrueStatement trueStatement = new()
            {
                Proposition = "Socrates is mortal"
            };

            string json = JsonConvert.SerializeObject(comp1, Formatting.Indented);            

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

            Сomposition config_defaultGet = JsonConvert.DeserializeObject<Сomposition>(json);
            Сomposition configGet = JsonConvert.DeserializeObject<Сomposition>(contentJSON.Get_JSON(configurationPath, ""));

            Console.WriteLine(config_defaultGet.Multiplier_A);
            Console.WriteLine(configGet.Multiplier_A);
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
    public class Сomposition
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Multiplier_A { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Multiplier_B { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TrueStatement? Premise { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TrueStatement
    {
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