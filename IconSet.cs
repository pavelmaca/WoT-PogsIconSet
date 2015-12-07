using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.IO;
using WotPogsIconSet.Layers;

namespace WotPogsIconSet
{
    public class IconSet
    {
        public IList<IconSet> Versions = new List<IconSet>();

        public IList<Delegate> Layers = new List<Delegate>();

        public string Name { get; protected set; }

        protected string OutputPath;

        public IconSet(string name)
        {
            Name = name;
        }

        public void SetOutputPath(string path)
        {
            OutputPath = path;
        }

        public string Generate(TankStats tankStats, string parentPath = null)
        {
            string outputFile = Path.Combine(OutputPath, tankStats.FileName);

            // create / load icon file
            using (Icon icon = parentPath == null ? new Icon() : new Icon(parentPath))
            {
                // aply layers
                foreach (Layer layer in Layers)
                {
                    icon.Apply(layer, tankStats);
                }

                // save to file
                icon.Save(outputFile);
            }

            return outputFile;
        }

        
    }
}
