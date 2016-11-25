using Newtonsoft.Json;
using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.IO;

namespace WotPogsIconSet.Utils
{
    class CustomNames
    {
        static Dictionary<string, string> nameList;

        static CustomNames()
        {
            // Load custom names
            nameList = new Dictionary<string, string>();

            using (StreamReader r = new StreamReader(Path.Combine(Properties.Settings.Default.resourcesLocation, "shortNames.json")))
            {
                string json = r.ReadToEnd();

                nameList = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }

        public static string findName(TankStats tank, string fallBackName)
        {
            string key = tank.Nation + ":" + tank.Id;
            if (nameList.ContainsKey(key))
            {
                return nameList[key];
            }

            Console.WriteLine("No custom name for key: " + key + "\n");
            //Console.WriteLine("Enter short name:");
            saveNewShortNames(key, fallBackName);

            return null;
        }

        public static string findName(TankStats tank)
        {
            string key = tank.Nation + ":" + tank.Id;
            if (nameList.ContainsKey(key))
            {
                return nameList[key];
            }
            return null;
        }

        public static void saveNewShortNames(string key, string name)
        {
            nameList.Add(key, name);

            using (StreamWriter wr = new StreamWriter(Path.Combine(Properties.Settings.Default.resourcesLocation, "shortNames.json")))
            {
                string json = JsonConvert.SerializeObject(nameList, Formatting.Indented);
                wr.WriteLine(json);
            }
        }

    }
}
