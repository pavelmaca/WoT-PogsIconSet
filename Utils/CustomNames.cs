using Newtonsoft.Json;
using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WotPogsIconSet.Fonts;

namespace WotPogsIconSet.Utils
{
    class CustomNames
    {
        static Dictionary<string, string> nameList;

        static CustomNames()
        {
            // Load custom names
            nameList = new Dictionary<string, string>();

            using (StreamReader r = new StreamReader(Path.Combine(@"D:\Repositories\wot.icons2\sources", "shortNames.json")))
            {
                string json = r.ReadToEnd();

                nameList = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }


      /*  public static string findShortName(TankStats tank, int maxLenght)
        {


            string translatedName = Translator.findShortName(tank);

            if (translatedName == null)
            {
                Console.WriteLine("No translation for: " + tank.Id);
                translatedName = useCustomName(tank, tank.Id+"_not_found");
            }
            else if (translatedName.Contains("_"))
            {
                Console.WriteLine("Invalid translation for: " + tank.Id + " \""+translatedName+"\"");
                translatedName = useCustomName(tank, translatedName+"_invalid");
            }
            else if (PogsFontRenderer.getTextWidth(PogsFonts.font4, translatedName) > maxLenght)
            {
                Console.WriteLine("Translated name is too long: " + translatedName);
                translatedName = useCustomName(tank, translatedName);

                if(PogsFontRenderer.getTextWidth(PogsFonts.font4, translatedName) > maxLenght)
                {
                    Console.WriteLine("Custom name is too long: " + translatedName);
                }
            }

            return translatedName == null ? "" : translatedName;
        }*/

        public static string findName(TankStats tank, string fallBackName)
        {
            string key = tank.Nation + ":" + tank.Id;
            if (nameList.ContainsKey(key))
            {
                return nameList[key];
            }

            Console.WriteLine("No custom name for key: " + key + "\n");
            Console.WriteLine("Enter short name:");
            saveNewShortNames(key, fallBackName);

            return null;
        }

       


        public static void saveNewShortNames(string key, string name)
        {
            nameList.Add(key, name);

            using (StreamWriter wr = new StreamWriter(Path.Combine(@"D:\Repositories\wot.icons2\sources", "shortNames.json")))
            {
                string json = JsonConvert.SerializeObject(nameList, Formatting.Indented);
                wr.WriteLine(json);
            }
        }

    }
}
