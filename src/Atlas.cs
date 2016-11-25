using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace WotPogsIconSet
{
    public class Atlas
    {
        public const String VEHICLE_MARKER_ATLAS = "vehicleMarkerAtlas";
        public const String BATTLE_ATLAS = "battleAtlas";

        protected String file;

        protected String tmpDir;
        protected String xmlTempFile;

        protected List<TankStats> stats;

        protected List<SubTexture> ImageList;

        protected struct SubTexture : IEquatable<SubTexture>
        {
            public string name;
            public Rectangle dimension;
            public int maxDimension;


            public SubTexture(string name, Rectangle dimension, int maxDimension)
            {
                this.name = name;
                this.dimension = dimension;
                this.maxDimension = maxDimension;
            }

            public bool Equals(SubTexture that)
            {
                return this.name == that.name;
            }
        }

        public Atlas(String file, List<TankStats> stats)
        {
            this.file = file;
            this.stats = stats;

            prepare();

            RadixSort();
            Arrangement();

            xmlTempFile = Path.Combine(tmpDir, file + ".xml");
            createXML(xmlTempFile);
        }

        protected void prepare()
        {
            Console.WriteLine("Preparing files from atlas " + file);

            ImageList = new List<SubTexture>();

            // Add tank icons with generated icon width and hieght
            foreach (TankStats tankData in stats)
            {
                SubTexture SubTexture = new SubTexture();
                SubTexture.name = Path.GetFileNameWithoutExtension(tankData.FileName);
                SubTexture.dimension = new Rectangle(0, 0, Icon.WIDTH, Icon.HEIGHT);
                SubTexture.maxDimension = Math.Max(Icon.WIDTH, Icon.HEIGHT);
                ImageList.Add(SubTexture);
            }

            //CreateImageList();

            String atlasXmlFile = Path.Combine(Properties.Settings.getResourcesLocation(), @"atlases", file + ".xml");
            String atlasPngFile = Path.Combine(Properties.Settings.getResourcesLocation(), @"atlases", file + ".png");

            tmpDir = Path.Combine(Properties.Settings.getOutputLocation(), "atlases", file);
            Directory.CreateDirectory(tmpDir);

            XmlDocument atlasXml = new XmlDocument();
            atlasXml.Load(atlasXmlFile);

            Image altasImage = Image.FromFile(atlasPngFile);

            foreach (XmlNode node in atlasXml.DocumentElement.ChildNodes)
            {
                // read data from XML
                String iconName = node.SelectSingleNode("name").InnerText.Trim();
                int iconX = Convert.ToInt32(node.SelectSingleNode("x").InnerText);
                int iconY = Convert.ToInt32(node.SelectSingleNode("y").InnerText);
                int iconWidth = Convert.ToInt32(node.SelectSingleNode("width").InnerText);
                int iconHeight = Convert.ToInt32(node.SelectSingleNode("height").InnerText);

                // extract image from sprite and save to tmp dir
                Rectangle cropArea = new Rectangle(iconX, iconY, iconWidth, iconHeight);

                using (Image cropedImage = Utils.ImageTools.cropImage(altasImage, cropArea))
                {
                    String iconFile = Path.Combine(tmpDir, iconName + ".png");
                    cropedImage.Save(iconFile);
                }

                // add subTexture into list
                SubTexture subTexture = new SubTexture(
                    iconName,
                    new Rectangle(0, 0, iconWidth, iconHeight),
                    Math.Max(iconWidth, iconHeight)
                );

                if (!ImageList.Contains(subTexture))
                {
                    ImageList.Add(subTexture);
                }

            }

            altasImage.Dispose();
        }


        public void generate(IconSet iconSet)
        {
            Directory.CreateDirectory(iconSet.OutputPathAtlas);

            String atlasPngFile = Path.Combine(iconSet.OutputPathAtlas, file + ".png");
            Console.WriteLine("Generating altas " + atlasPngFile);

            createPng(atlasPngFile, iconSet);

            //Copy tmp XML to outputDir
            String atlasXmlFile = Path.Combine(iconSet.OutputPathAtlas, file + ".xml");
            File.Copy(xmlTempFile, atlasXmlFile);
        }


        /// <summary>
        /// @source https://bitbucket.org/rstarkov/tankiconmaker
        /// </summary>
        private void RadixSort()
        {
            int CInd;
            int[] C = new int[10];
            List<SubTexture> ImageListTemp = new List<SubTexture>(ImageList);
            int t = 1;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < 10; j++)
                    C[j] = 0;
                for (int j = 0; j < ImageList.Count; j++)
                {
                    CInd = (ImageList[j].maxDimension % (t * 10)) / t;
                    C[CInd] = C[CInd] + 1;
                }
                for (int j = 8; j >= 0; j--)
                    C[j] = C[j + 1] + C[j];
                for (int j = ImageList.Count - 1; j >= 0; j--)
                {
                    CInd = (ImageList[j].maxDimension % (t * 10)) / t;
                    ImageListTemp[C[CInd] - 1] = ImageList[j];
                    C[CInd] = C[CInd] - 1;
                }
                t *= 10;
                ImageList = new List<SubTexture>(ImageListTemp);
            }
        }



        /// <summary>
        /// @source https://bitbucket.org/rstarkov/tankiconmaker
        /// </summary>
        private void Arrangement()
        {
            List<System.Drawing.Rectangle> TakePlaceList = new List<System.Drawing.Rectangle>();
            SubTexture SubTexture;
            System.Drawing.Rectangle Rct, TakeRct;
            const int TextureHeight = 2048, TextureWidth = 2048;
            int CurrentY, j, k;
            TakePlaceList.Add(ImageList[0].dimension);
            for (int i = 1; i < ImageList.Count; i++)
            {
                SubTexture = ImageList[i];
                Rct = SubTexture.dimension;
                CurrentY = TextureHeight;
                j = 0;
                while (j < TakePlaceList.Count)
                    if (TakePlaceList[j].IntersectsWith(Rct))
                    {
                        Rct.Location = new System.Drawing.Point(TakePlaceList[j].Right + 1, Rct.Y);
                        if (TakePlaceList[j].Bottom > Rct.Y)
                            CurrentY = Math.Min(CurrentY, TakePlaceList[j].Bottom - Rct.Y + 1);
                        if (Rct.Right > TextureWidth)
                        {
                            Rct.Location = new System.Drawing.Point(0, Rct.Y + CurrentY);
                            CurrentY = TextureHeight;
                        }
                        j = TakePlaceList.Count - 1;
                        while ((j > 0) && (TakePlaceList[j].Bottom > Rct.Y))
                            j--;
                    }
                    else
                        j++;
                if (Rct.Bottom > TextureHeight)
                    return; //необходимо обработать данную ситуацию
                j = TakePlaceList.Count - 1;
                while ((j >= 0) && (TakePlaceList[j].Bottom > Rct.Bottom))
                    j--;
                k = j;
                while ((k >= 0) && (TakePlaceList[k].Bottom == Rct.Bottom))
                    if ((Rct.X == TakePlaceList[k].Right + 1) && (Rct.Y == TakePlaceList[k].Y))
                    {
                        TakeRct = TakePlaceList[k];
                        TakeRct.Width += Rct.Width + 1;
                        TakePlaceList[k] = TakeRct;
                        k = -1;
                        j = -2;
                    }
                    else
                        k--;
                if (j > -2)
                {
                    j++;
                    TakePlaceList.Insert(j, Rct);
                }
                SubTexture.dimension = Rct;
                ImageList[i] = SubTexture;

            }
        }

        private void createPng(string outputFile, IconSet iconSet)
        {
            Bitmap AtlasPNG = new Bitmap(2048, 2048);
            AtlasPNG.SetResolution(96.0F, 96.0F);
            foreach (SubTexture subTexture in ImageList)
            {
                //load icon
                String iconPath = Path.Combine(iconSet.OutputPathIcon, subTexture.name + ".png");
                if (!File.Exists(iconPath))
                {
                    iconPath = Path.Combine(tmpDir, subTexture.name + ".png");
                }

                using (Graphics gPNG = Graphics.FromImage(AtlasPNG))
                {
                    Bitmap PNG = Image.FromFile(iconPath) as Bitmap;
                    PNG.SetResolution(96.0F, 96.0F);

                    gPNG.DrawImage(PNG, (int)subTexture.dimension.X, (int)subTexture.dimension.Y);
                    PNG.Dispose();
                }

            }
            AtlasPNG.Save(outputFile);
            AtlasPNG.Dispose();
        }

        private void createXML(string outputFile)
        {
            FileStream fileXML = new FileStream(outputFile, FileMode.Create);
            StreamWriter writer = new StreamWriter(fileXML);
            writer.WriteLine("<root>");
            foreach (var SubTexture in ImageList)
            {
                writer.WriteLine("  <SubTexture>");
                writer.WriteLine("    <name> " + SubTexture.name + " </name>");
                writer.WriteLine("    <x> " + (int)SubTexture.dimension.X + " </x>");
                writer.WriteLine("    <y> " + (int)SubTexture.dimension.Y + " </y>");
                writer.WriteLine("    <width> " + (int)SubTexture.dimension.Width + " </width>");
                writer.WriteLine("    <height> " + (int)SubTexture.dimension.Height + " </height>");
                writer.WriteLine("  </SubTexture>");
            }
            writer.Write("</root>");
            writer.Close();
        }


    }
}
