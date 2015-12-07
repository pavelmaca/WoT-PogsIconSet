using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Globalization;
using System.IO;
using WotPogsIconSet.Icons;
using WotPogsIconSet.Icons.Layers;

namespace WotPogsIconSet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fix Phobos.WoT Shell.cs error
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            // load tank stats using Phobos.WoT lib
            String itemDefLocation = Properties.Settings.Default.itemDefLocation;
            List<TankStats> stats = ItemDatabase.GetTankStats(itemDefLocation).ToList();

            // generate Colored versions
            generateColored(stats);

            // generate Clear versions

            Console.ReadLine();
        }

        private static bool generateColored(List<TankStats> stats)
        {
            // TODO make basic version, then use it as source for other versions


            string outputPath = Properties.Settings.Default.outputLocation + @"\{0}\gui\maps\icons\vehicle\contour\";
            Console.WriteLine("Generating coor icons: ");

            /*   if (!Directory.Exists(outputPath))
               {
                   Directory.CreateDirectory(outputPath);
               }*/

            Configs[][] configs = new Configs[][] {
            // as default    new Configs[] { Configs.SIMPLE},
                new Configs[] { Configs.MAX, Configs.FSR},
                new Configs[] { Configs.MAX, Configs.FSR, Configs.RLD},
                new Configs[] { Configs.MAX, Configs.FSR, Configs.VR},
                new Configs[] { Configs.MAX, Configs.FSR, Configs.VR, Configs.RLD},
                new Configs[] { Configs.MAX, Configs.RLD},
                new Configs[] { Configs.DMG, Configs.ART },
                new Configs[] { Configs.DMG, Configs.ART, Configs.RLD },
                new Configs[] { Configs.DMG, Configs.ART, Configs.VR },
                new Configs[] { Configs.DMG, Configs.ART, Configs.VR, Configs.RLD },
            };


            foreach (TankStats tankStats in stats)
            {
                using (Icons.Icon icon = new Colored(tankStats))
                {
                    string template = icon.Save(CreateFolder(String.Format(outputPath, "SIMPLE")));

                    foreach(Configs[] config in configs)
                    {
                        string name = String.Join("_", config);
           
                        using (Icons.Icon subIcon = new Max(tankStats, template, config))
                        {
                            subIcon.Save(CreateFolder(String.Format(outputPath, name)));
                        }
                    }

                    using (Icons.Icon subIcon = new Tahti(tankStats, template))
                    {
                        subIcon.Save(CreateFolder(String.Format(outputPath, "TAHTI")));
                    }

                }
            }

            return false;
        }

        private static string CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private static bool generateClear(List<TankStats> stats)
        {
            // TODO make basic version, then use it as source for other versions
            return false;
        }


        /*   private static void GenerateModFiles(string name, string version, List<TankStats> stats, string description, IconGenerator ig)
           {
               string iconsDirectoryPath = appPath + baseIiconsDirectory + String.Format(@"\{1}\gui\maps\icons\vehicle\contour\", name, version);
               string modInfoFilePath = appPath + baseIiconsDirectory + String.Format(@"\readme-ContourIcons-{1}.txt", name, version);
               Console.WriteLine("Generating contour icons: " + name);

               if (!Directory.Exists(iconsDirectoryPath)) Directory.CreateDirectory(iconsDirectoryPath);
               File.WriteAllText(modInfoFilePath, String.Format(@"Ashbane's Contour Icons for World of Tanks " + version.Substring(2) + @" " + name + @"" + description + @"Extract the archive to your res_mods directory (eg. C:\Games\World of Tanks\res_mods)."));

               foreach (TankStats ts in stats) ig.Generate(iconsDirectoryPath, ts);
           }*/
    }
}
