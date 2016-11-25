using NGettext;
using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace WotPogsIconSet.Utils
{
    /// <summary>
    /// Translate tank name using .mo files from game
    /// </summary>
    class WgTranslator
    {
        /// <summary>
        /// Loaded .mo files
        /// </summary>
        static LinkedList<ICatalog> catalogs = new LinkedList<ICatalog>();

        static Regex subfixes = new Regex(@"_[(training)(action)]$");

        static WgTranslator()
        {
            // Load translation files into objects using NGettext.Catalog

            string[] files = new string[] { "czech", "france", "gb", "germany", "china", "igr", "japan", "usa", "ussr" };

            string file = Path.Combine(Properties.Settings.Default.srcLocation, @"texts\{0}_vehicles.mo");

            foreach (string nation in files)
            {
                string filePath = String.Format(file, nation);
                using (FileStream r = File.Open(filePath, FileMode.Open))
                {
                    ICatalog catalog = new Catalog(r, CultureInfo.CreateSpecificCulture("en-US"));
                    catalogs.AddLast(catalog);
                }

            }
        }

        /// <summary>
        /// Find translted vehicle name. Return null, if nothing is found.
        /// </summary>
        /// <param name="tank"></param>
        /// <returns></returns>
        public static string findName(TankStats tank)
        {
            string fixedTankName = TankNameHelper.IDRegex.Replace(tank.Name, "").Replace(' ', '_');

            if (subfixes.IsMatch(fixedTankName))
            {
                fixedTankName = subfixes.Replace(fixedTankName, "");
            }

            // 1 search for tankID_short
            // 2 search for tankName_short
            // 3 search for tankID
            // 4 search for tankName

            string[] search = new string[] {
                tank.Id + "_short",
                fixedTankName + "_short",
                tank.Id,
                fixedTankName,
            };

            foreach (Catalog catalog in catalogs)
            {
                foreach (string key in search)
                {
                    string translation = catalog.GetStringDefault(key, null);
                    if (translation != null)
                    {
                        return translation;
                    }

                }
            }

            return null;
        }

    }
}
