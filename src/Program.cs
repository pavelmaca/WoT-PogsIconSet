using System;
using System.Globalization;
using System.IO;
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

        
            // Clear outpout dir
            Console.WriteLine("Clearing output dir...");
               if (Directory.Exists(Properties.Settings.getOutputLocation()))
               {
                   var dir = new DirectoryInfo(Properties.Settings.getOutputLocation());
                Console.WriteLine("Are you sure to clear direcotry \""+ dir + "\" ? Y\\N" );
                String answer = Console.ReadLine();
                if (!answer.ToUpper().Equals("Y"))
                {
                    return;
                }
                dir.Delete(true);
               }
            Directory.CreateDirectory(Properties.Settings.getOutputLocation());

            // Run icon generator
            // load stats
            Generator generator = new Generator();

            // configure icon sets
            generator.AddIconSets(Configuration.getSet());

            //generator.PrepareOutputFolders();

            // render icons to output directory
            generator.CreateIcons(); //new string[] {"usa-A63_M46_Patton_KR.png"});

            generator.CreateAtlases();

            // create zip archives
            generator.CreatePackagest();

            Console.ReadLine();

        }
    }
}
