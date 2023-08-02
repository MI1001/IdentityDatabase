// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nuget.IdentityDatabase
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = Path.Combine("indices.dat");
            string configurationPath = Path.Combine("Config.json");

            Сomposition comp1 = new()
            {
                Multiplier_A = "Blender 1",
                Multiplier_B = "Blender 2"
            };

            string json = JsonConvert.SerializeObject(comp1, Formatting.Indented);

            // Copyright
            Console.WriteLine("{0} | Identity Database | INFO | IDB | InitCore() Copyright © 2023 WG Element.",
                new DateTime(2023, 8, 2, 0, 0, 0, DateTimeKind.Utc));

            try
            {
                if (File.Exists(path) && File.Exists(configurationPath))
                {
                    Console.WriteLine("{0} | Identity Database | INFO | IDB | Файл *.dat ({1}) уже существует. Конфигурационный файл ({2}) присутствует.",
                        DateTime.UtcNow, path, configurationPath);

                    // Continue...
                }
                else
                {
                    StreamWriter sw = File.CreateText(path);

                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }

            try
            {
                using (StreamReader sr = new(configurationPath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The configuration file could not be read:");
                Console.WriteLine(e.Message);
            }            

            Console.WriteLine(json);
            Console.ReadLine();
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