using Phobos.WoT;
using System;
using System.Text.RegularExpressions;
using WotPogsIconSet.Fonts;

namespace WotPogsIconSet.Utils
{
    class TankNameHelper
    {
        public static Regex IDRegex = new Regex(@"^([A-z]{1,2}[0-9]{2}_)");

        public static string findShortName(TankStats tank, int maxWidth)
        {
            // 1 use regular tank name
            string name = tank.Name;

            string forceCustom = forceCustomName(tank);
            if (forceCustom != null)
            {
                if (isTextToLong(forceCustom, maxWidth))
                {
                    //Console.WriteLine("Forced custom name is too long: " + forceCustom);
                }else
                {
                    //Console.WriteLine("Using forced custom name: " + forceCustom);
                    return forceCustom;
                }
            }

            // check if name is id (containd F11_ etc...)  if not, continue, else use translated name
            if (startWithId(name))
            {
                //Console.WriteLine("Invalid name: " + tank.Name);
                return translatedName(tank, maxWidth, name + "_invalid");
            }

            // if regular name is too long, use transalted name
            if (isTextToLong(name, maxWidth))
            {
                //Console.WriteLine("Name is to long: " + tank.Name);
                return translatedName(tank, maxWidth, name + "_to_long");
            }

            return name;
        }

        protected static string translatedName(TankStats tank, int maxWidth, string fallBackName)
        {
            // 2 transalted name

            string name = WgTranslator.findName(tank);

            // if no translated name found, use custom name
            if (name == null)
            {
                //Console.WriteLine("No translation for: " + tank.Id);
                return customName(tank, maxWidth, fallBackName + "_no_translation");
            }

            // if translated name is too long, use custom name
            if (isTextToLong(name, maxWidth))
            {
                //Console.WriteLine("Translated name is to long: " + tank.Id);
                return customName(tank, maxWidth, name + "_transaltion_to_long");
            }

            return name;
        }

        protected static string customName(TankStats tank, int maxWidth, string fallBackName)
        {
            // 3 custom name

            // if custom name doesnt exist, create it and set it to prevuisly name, od ID
            string name = CustomNames.findName(tank, fallBackName);
            if (name == null)
            {
                return "";
            }

            // if custom name is too long, show warning        
            if (isTextToLong(name, maxWidth))
            {
                Console.WriteLine("Custom name is too long: " + name);
                name = "";
            }

            return name;
        }

        protected static string forceCustomName(TankStats tank)
        {
            return  CustomNames.findName(tank);
        }


        protected static bool isTextToLong(string text, int maxWidth)
        {
            return PogsFontRenderer.getTextWidth(PogsFonts.font3, PogsFonts.font4, text) > maxWidth;
        }

        protected static bool startWithId(string name)
        {
            return IDRegex.IsMatch(name);
        }
    }
}
