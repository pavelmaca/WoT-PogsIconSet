using Ionic.Zip;
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
        protected List<TankStats> Stats;

        protected IList<IconSet> IconSets = new List<IconSet>();

        protected bool isOutputPrepared = false;

        public Generator()
        {
            // load tank stats using Phobos.WoT lib
            Console.WriteLine("Reading WoT stats...");
            String itemDefLocation = Properties.Settings.Default.itemDefLocation;
            Stats = ItemDatabase.GetTankStats(itemDefLocation).ToList();
            Console.WriteLine("Found " + Stats.Count + " vehicles.\n");
        }

        public void AddIconSets(IList<IconSet> iconSets)
        {
            IconSets = IconSets.Concat(iconSets).ToList();
        }

        protected void PrepareOutputFolder(IconSet iconSet, String prefix = null)
        {
            // create directories for sub versions
            if (prefix == null)
            {
                prefix = iconSet.Name;
            }
            else
            {
                prefix += "_" + iconSet.Name;
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
                PrepareOutputFolder(version, prefix);
            }
        }

        public void PrepareOutputFolders()
        {
            // prepare output folders
            Console.WriteLine("Preparings output derectories...");
            foreach (IconSet iconSet in IconSets)
            {
                PrepareOutputFolder(iconSet);
            }

            isOutputPrepared = true;
        }

        public void CreateIcons(string[] filterFiles = null, string[] filterNation = null)
        {
            if (!isOutputPrepared)
            {
                PrepareOutputFolders();
            }

            // generates icons
            Console.WriteLine("Generating icons...");
            foreach (TankStats tankStats in Stats)
            {
                // Debug, process only given vehicles, by icon file name.
                if (filterFiles != null && !filterFiles.Contains(tankStats.FileName))
                {
                    continue;
                }
                // Debug, process only ehicles, for given nation.
                if (filterNation != null && !filterNation.Contains(tankStats.Nation))
                {
                    continue;
                }

                // traverse all main sets
                foreach (IconSet iconSet in IconSets)
                {
                    CreateIconSet(iconSet, tankStats);
                }
            }

            Console.WriteLine("done");
        }

        protected void CreateIconSet(IconSet iconSet, TankStats tankStats, string parentPath = null)
        {
            // generate base icon for set, and thne pass it to all other versions
            string iconPath = iconSet.Generate(tankStats, parentPath);

            foreach (IconSet iconVersion in iconSet.getVersions())
            {
                CreateIconSet(iconVersion, tankStats, iconPath);
            }
        }

        public void CreatePackagest()
        {
            if (!isOutputPrepared)
            {
                PrepareOutputFolders();
            }

            Console.WriteLine("Creating packagest...");

            foreach (IconSet iconSet in IconSets)
            {
                CreatePackage(iconSet);
            }
            Console.WriteLine("done");
        }

        protected void CreatePackage(IconSet iconSet)
        {
            foreach (IconSet version in iconSet.getVersions())
            {
                CreatePackage(version);
            }

            string zipFileName = Properties.Settings.Default.gameVersion + "_" + iconSet.FullName + ".zip";
            string zipPath = Path.Combine(Properties.Settings.Default.outputLocation, zipFileName);

            using (ZipFile archive = new ZipFile())
            {
                string innerPathIcons = String.Format(@"res_mods\{0}\gui\maps\icons\vehicle\contour\", Properties.Settings.Default.gameVersion);
                archive.AddDirectory(iconSet.OutputPathIcon, innerPathIcons);

                string innerPathAtlases = String.Format(@"res_mods\{0}\gui\flash\atlases\", Properties.Settings.Default.gameVersion);
                archive.AddDirectory(iconSet.OutputPathAtlas, innerPathAtlases);
                

                archive.Save(zipPath);
            }
        }

        public void CreateAtlases()
        {
            if (!isOutputPrepared)
            {
                PrepareOutputFolders();
            }

            Console.WriteLine("Creating atlases...");

            // Read original atlases
            Atlas battleAtlas = new Atlas(Atlas.BATTLE_ATLAS, Stats);
            Atlas markerAtlas = new Atlas(Atlas.VEHICLE_MARKER_ATLAS, Stats);

            List<Atlas> atlases = new List<Atlas>();
            atlases.Add(battleAtlas);
            atlases.Add(markerAtlas);

            // Write modifiead atlases

            foreach (IconSet iconSet in IconSets)
            {
                CreateAtlas(iconSet, atlases);
            }
            Console.WriteLine("done");
        }

        protected void CreateAtlas(IconSet iconSet, IList<Atlas> atlases)
        {
            foreach (IconSet version in iconSet.getVersions())
            {
                CreateAtlas(version, atlases);
            }

            foreach(Atlas atlas in atlases)
            {
                atlas.generate(iconSet);
            }

        }

    }
}