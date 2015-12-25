using NGettext;
using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace WotPogsIconSet.Utils
{
    class Translator
    {
        static LinkedList<ICatalog> catalogs = new LinkedList<ICatalog>();

        static Regex subfixes = new Regex(@"_[(training)(action)]$");

        static Translator()
        {

            string[] files = new string[] { "czech", "france", "gb", "germany", "china", "igr", "japan", "usa", "ussr" };

            string file = Path.Combine(@"D:\Repositories\wot.icons2\sources", @"texts\{0}_vehicles.mo");

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
               // tank.Name + "_short",
                tank.Id,
                fixedTankName,
               // tank.Name
            };

            //
            // string tankName = tank.Name.Replace(' ', '_') + "_short";

            /*  Regex rx = new Regex(@"^([A-Z]{1,2}[0-9]{2}_)");
               string tankName = rx.Replace(tank.Id, "") + "_short" ;*/

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
