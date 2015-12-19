using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.IO;
using WotPogsIconSet.Layers;

namespace WotPogsIconSet
{
    public class IconSet
    {
        protected IList<IconSet> Versions;

        protected IList<Layer> Layers;

        public string Name { get; protected set; }

        protected string OutputPath;

        public IconSet(string name)
        {
            Name = name;
            Versions = new List<IconSet>();
            Layers = new List<Layer>();
        }

        public void SetOutputPath(string path)
        {
            OutputPath = path;
        }

        public string Generate(TankStats tankStats, string parentPath = null)
        {
            Console.WriteLine("Creating set:" + this.Name);
            Console.WriteLine("parent:" + parentPath);
            string outputFile = Path.Combine(OutputPath, tankStats.FileName);
            Console.WriteLine("output:" + outputFile);

            // create / load icon file
            using (Icon icon = parentPath == null ? new Icon() : new Icon(parentPath))
            {
                // aply layers
                foreach (Layer layer in Layers)
                {
                    icon.Apply(layer, tankStats);
                }

                // save to file
               // Console.WriteLine(outputFile);
                icon.Save(outputFile);
            }

            return outputFile;
        }

        public void addLayer(Layer layer)
        {
            Layers.Add(layer);
        }

        public void addVersion(IconSet version)
        {
            Versions.Add(version.Clone());
        }

        public IList<Layer> getLayers()
        {
            return new List<Layer>(Layers);
        }

        public IList<IconSet> getVersions()
        {
            return new List<IconSet>(Versions);
        }


        protected IconSet Clone()
        {
            IconSet clone = new IconSet(Name);
            clone.OutputPath = null;
            clone.Layers = new List<Layer>(Layers);

            // clone all versions
            foreach(IconSet version in Versions)
            {
                clone.Versions.Add(version.Clone());
            }

            return clone;
        }

        
    }
}
