using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Globalization;
using WotPogsIconSet.Generators;
using System.IO;

namespace WotPogsIconSet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fix Phobos.WoT Shell.cs error
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;



            String itemDefLocation = PogsIcons.Properties.Settings.Default.itemDefLocation;

            List<TankStats> stats = ItemDatabase.GetTankStats(itemDefLocation).ToList();

            generateColored(stats);
        }

        private static bool generateColored(List<TankStats> stats)
        {
            // TODO make basic version, then use it as source for other versions

            IIConGenerator ig = new MaxFsrVrRld();

            string name = "color";
            string outputPath = PogsIcons.Properties.Settings.Default.outputLocation + String.Format(@"\{0}\gui\maps\icons\vehicle\contour\", name);
            Console.WriteLine("Generating contour icons: " + name);

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }


            foreach (TankStats tankStats in stats)
            {
                ig.Generate(outputPath, tankStats);
            }

            return false;
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
