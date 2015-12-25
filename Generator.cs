using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace WotPogsIconSet
{
    public class Generator
    {
        const string GAME_VERSION = "0.9.13";

        protected List<TankStats> Stats;

        protected IList<IconSet> IconSets = new List<IconSet>();

        public Generator()
        {
            // load tank stats using Phobos.WoT lib
            Console.WriteLine("Reading WoT stats...");
            String itemDefLocation = Properties.Settings.Default.itemDefLocation;
            Stats = ItemDatabase.GetTankStats(itemDefLocation).ToList();
            Console.WriteLine("Found " + Stats.Count + " vehicles.");
        }

        public void AddIconSets(IList<IconSet> iconSets)
        {
            IconSets = IconSets.Concat(iconSets).ToList();
        }

        protected void PrepareOutputFolder(IconSet iconSet, String prefix = null )
        {
            // create directories for sub versions
            if (prefix == null)
            {
                prefix = iconSet.Name;
            }
            else
            {
                prefix += "_"+iconSet.Name;
            }
            

            string outputPath = Path.Combine(Properties.Settings.Default.outputLocation, String.Format(@"{0}\", prefix));
            if (!Directory.Exists(outputPath))
            {
                Console.WriteLine("Creating ouput directory: " + outputPath);
                Directory.CreateDirectory(outputPath);
            }
            iconSet.SetOutputPath(outputPath, prefix);

            // handle versions
            foreach (IconSet version in iconSet.getVersions())
            {
               // List<string> prefixClone = prefix.ToArray().ToList(); // TODO: better clone this object
                PrepareOutputFolder(version, prefix);
            }
        }

        public void CreateIcons()
        {
            // prepare output folders
            Console.WriteLine("Preparings output derectories...");
            foreach (IconSet iconSet in IconSets)
            {
                PrepareOutputFolder(iconSet);
            }

            // generates icons
            Console.WriteLine("Generating icons...");
            foreach (TankStats tankStats in Stats)
            {
                //Console.WriteLine("vehicle: " + tankStats.FileName);

                /*/ Test
                if (tankStats.FileName != "czech-Cz01_Skoda_T40.png")
                {
                    continue;
                }
                *///

               // if (tankStats.Nation != "germany") continue;

                // traverse all main sets
                foreach (IconSet iconSet in IconSets)
                {
                    CreateIconSet(iconSet, tankStats);
                }
            }
        }

        protected void CreateIconSet(IconSet iconSet, TankStats tankStats, string parentPath = null)
        {
            //Console.WriteLine("version: " + iconSet.Name);

            // generate base icon for set, and thne pass it to all other versions
            string iconPath = iconSet.Generate(tankStats, parentPath);

            foreach (IconSet iconVersion in iconSet.getVersions())
            {
                CreateIconSet(iconVersion, tankStats, iconPath);
            }
        }

        public void CreatePackagest()
        {
            Console.WriteLine("Creating packagest...");

            // TODO create readme
            foreach (IconSet iconSet in IconSets)
            {
                CreatePackage(iconSet);
            }
            Console.WriteLine("done");
        }

        public void CreatePackage(IconSet iconSet)
        {
            foreach(IconSet version in iconSet.getVersions())
            {
                CreatePackage(version);
            }

            using (FileStream zipToOpen = new FileStream(Path.Combine(Properties.Settings.Default.outputLocation, GAME_VERSION + "_"+iconSet.FullName+".zip"), FileMode.OpenOrCreate))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    /*ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        writer.WriteLine("Information about this package.");
                        writer.WriteLine("========================");
                    }*/

                    // add each file
                    foreach (TankStats tankStats in Stats)
                    {
                        string inputPath = Path.Combine(iconSet.OutputPath, tankStats.FileName);
                        string innerPath = String.Format(@"res_mods\{0}\gui\maps\icons\vehicle\contour\{1}", GAME_VERSION, tankStats.FileName);
                        archive.CreateEntryFromFile(inputPath, innerPath);
                    }
                   
                }
            }
        }

    }
}