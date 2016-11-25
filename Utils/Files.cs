using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotPogsIconSet.Utils
{
    public class Files
    {

        public static void deleteDirectoryRecusivly(String path)
        {
            Console.WriteLine("Are you sure to delete " + path + " ?");
            Console.ReadLine();
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

        }
    }
}
