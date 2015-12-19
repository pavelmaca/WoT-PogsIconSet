using System;
using System.Globalization;
using System.Threading;


namespace WotPogsIconSet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fix Phobos.WoT Shell.cs error
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            // Run icon generator
            // load stats
            Generator generator = new Generator();

            // configure icon sets
            generator.AddIconSets(Configuration.getPogsMaxFSRVRRld());

            // render icons
            generator.CreateIcons();

            generator.CreatePackagest();

            Console.ReadLine();

        }
    }
}
