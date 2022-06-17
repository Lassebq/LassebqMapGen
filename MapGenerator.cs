using System.Drawing;
using System.IO;
using System;

namespace LassebqMapGen
{
    class MapGenerator
    {

        public static string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Terraria";

        public static string WorldPath = SavePath + "\\Worlds";

        public static string worldName = "World";

        public static int maxTilesX;

        public static int maxTilesY;

        public static double worldSurface;

        public static double rockLayer;

        public static Tile[,] tile = new Tile[maxTilesX, maxTilesY];

        public static float leftWorld = 0f;

        public static float rightWorld = 134400f;

        public static float topWorld = 0f;

        public static float bottomWorld = 38400f;

        public static bool[] tileFrameImportant = new bool[80];

        public static bool previewGenerated = false;

        public static Bitmap preview;
        public static void OpenWorld(string world)
        {
            previewGenerated = false;
            tileFrameImportant[3] = true;
            tileFrameImportant[5] = true;
            tileFrameImportant[10] = true;
            tileFrameImportant[11] = true;
            tileFrameImportant[12] = true;
            tileFrameImportant[13] = true;
            tileFrameImportant[14] = true;
            tileFrameImportant[15] = true;
            tileFrameImportant[16] = true;
            tileFrameImportant[17] = true;
            tileFrameImportant[18] = true;
            tileFrameImportant[20] = true;
            tileFrameImportant[21] = true;
            tileFrameImportant[24] = true;
            tileFrameImportant[26] = true;
            tileFrameImportant[27] = true;
            tileFrameImportant[28] = true;
            tileFrameImportant[29] = true;
            tileFrameImportant[31] = true;
            tileFrameImportant[33] = true;
            tileFrameImportant[34] = true;
            tileFrameImportant[35] = true;
            tileFrameImportant[36] = true;
            tileFrameImportant[42] = true;
            tileFrameImportant[50] = true;
            tileFrameImportant[55] = true;
            tileFrameImportant[61] = true;
            tileFrameImportant[71] = true;
            tileFrameImportant[72] = true;
            tileFrameImportant[73] = true;
            tileFrameImportant[74] = true;
            tileFrameImportant[77] = true;
            tileFrameImportant[78] = true;
            tileFrameImportant[79] = true;
            using (FileStream input = new FileStream(world, FileMode.Open))
            using (BinaryReader binaryReader = new BinaryReader(input))
                try
                {
                    int num = binaryReader.ReadInt32();
                    bool is1_0_0 = false;
                    bool isBeta = false;
                    if (num <= 3)
                    {
                        is1_0_0 = true;
                    }
                    if (num == 38)
                    {
                        isBeta = true;
                    }
                    if(!is1_0_0 && !isBeta)
                    {
                        binaryReader.Close();
                        return;
                    }
                    worldName = binaryReader.ReadString();
                    if(!isBeta) binaryReader.ReadInt32();
                    leftWorld = binaryReader.ReadInt32();
                    rightWorld = binaryReader.ReadInt32();
                    topWorld = binaryReader.ReadInt32();
                    bottomWorld = binaryReader.ReadInt32();
                    maxTilesY = binaryReader.ReadInt32();
                    maxTilesX = binaryReader.ReadInt32();
                    tile = new Tile[maxTilesX, maxTilesY];
                    for (int k = 0; k < maxTilesX; k++)
                    {
                        float num2 = (float)k / (float)maxTilesX;
                        for (int l = 0; l < maxTilesY; l++)
                        {
                            tile[k, l] = new Tile();
                        }
                    }
                    binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                    worldSurface = binaryReader.ReadDouble();
                    rockLayer = binaryReader.ReadDouble();
                    binaryReader.ReadDouble();
                    binaryReader.ReadBoolean();
                    binaryReader.ReadInt32();
                    binaryReader.ReadBoolean();
                    binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                    binaryReader.ReadBoolean();
                    binaryReader.ReadBoolean();
                    binaryReader.ReadBoolean();
                    binaryReader.ReadBoolean();
                    if (!isBeta) binaryReader.ReadBoolean();
                    if (!isBeta) binaryReader.ReadByte();
                    binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                    binaryReader.ReadDouble();
                    for (int i = 0; i < maxTilesX; i++)
                    {
                        float num2 = (float)i / (float)maxTilesX;
                        for (int j = 0; j < maxTilesY; j++)
                        {
                            tile[i, j].active = binaryReader.ReadBoolean();
                            if (tile[i, j].active)
                            {
                                tile[i, j].type = binaryReader.ReadByte();
                                if (tileFrameImportant[tile[i, j].type])
                                {
                                    binaryReader.ReadInt16();
                                    binaryReader.ReadInt16();
                                }
                            }
                            binaryReader.ReadBoolean();
                            if (binaryReader.ReadBoolean())
                            {
                                tile[i, j].wall = binaryReader.ReadByte();
                            }
                            if (binaryReader.ReadBoolean())
                            {
                                tile[i, j].liquid = binaryReader.ReadByte();
                                tile[i, j].lava = binaryReader.ReadBoolean();
                            }
                        }
                    }
                    binaryReader.Close();
                }
                catch
                {
                    try
                    {
                        binaryReader.Close();
                    }
                    catch { }
                    return;
                }
        }
        public static void Generate()
        {
            Bitmap map = new Bitmap(maxTilesX, maxTilesY);
            Color[] colors = new Color[80];
            for (int i = 0; i < 80; i++)
            {
                colors[i] = Color.Black;
            }
            Color color = Color.FromArgb(151, 107, 75);
            colors[0] = color;
            colors[5] = color;
            colors[30] = color;
            color = Color.FromArgb(128, 128, 128);
            colors[1] = color;
            colors[38] = color;
            colors[48] = color;
            colors[2] = Color.FromArgb(28, 216, 94);
            color = Color.FromArgb(26, 196, 84);
            colors[3] = color;
            colors[73] = Color.FromArgb(27, 197, 109);
            colors[52] = Color.FromArgb(23, 177, 76);
            colors[20] = Color.FromArgb(163, 116, 81);
            colors[6] = Color.FromArgb(140, 101, 80);
            color = Color.FromArgb(150, 67, 22);
            colors[7] = color;
            colors[47] = color;
            color = Color.FromArgb(185, 164, 23);
            colors[8] = color;
            colors[45] = color;
            color = Color.FromArgb(185, 194, 195);
            colors[9] = color;
            colors[46] = color;
            color = Color.FromArgb(98, 95, 167);
            colors[22] = color;
            colors[23] = Color.FromArgb(141, 137, 223);
            colors[24] = Color.FromArgb(122, 116, 218);
            colors[25] = Color.FromArgb(109, 90, 128);
            colors[37] = Color.FromArgb(104, 86, 84);
            colors[39] = Color.FromArgb(181, 62, 59);
            colors[40] = Color.FromArgb(146, 81, 68);
            colors[41] = Color.FromArgb(66, 84, 109);
            colors[43] = Color.FromArgb(84, 100, 63);
            colors[44] = Color.FromArgb(107, 68, 99);
            colors[53] = Color.FromArgb(186, 168, 84);
            colors[54] = Color.FromArgb(200, 246, 254);
            colors[56] = Color.FromArgb(43, 40, 84);
            colors[75] = Color.FromArgb(26, 26, 26);
            colors[57] = Color.FromArgb(68, 68, 76);
            color = Color.FromArgb(142, 66, 66);
            colors[58] = color;
            colors[76] = color;
            color = Color.FromArgb(92, 68, 73);
            colors[59] = color;
            colors[60] = Color.FromArgb(143, 215, 29);
            colors[61] = Color.FromArgb(135, 196, 26);
            colors[74] = Color.FromArgb(96, 197, 27);
            colors[62] = Color.FromArgb(121, 176, 24);
            colors[63] = Color.FromArgb(110, 140, 182);
            colors[64] = Color.FromArgb(196, 96, 114);
            colors[65] = Color.FromArgb(56, 150, 97);
            colors[66] = Color.FromArgb(160, 118, 58);
            colors[67] = Color.FromArgb(140, 58, 166);
            colors[68] = Color.FromArgb(125, 191, 197);
            colors[70] = Color.FromArgb(93, 127, 255);
            color = Color.FromArgb(182, 175, 130);
            colors[71] = color;
            colors[72] = color;
            colors[4] = Color.FromArgb(253, 221, 3);
            color = Color.FromArgb(253, 221, 3);
            colors[33] = color;
            color = Color.FromArgb(119, 105, 79);
            colors[11] = color;
            colors[10] = color;
            color = Color.FromArgb(191, 142, 111);
            colors[14] = color;
            colors[15] = color;
            colors[18] = color;
            colors[19] = color;
            colors[55] = color;
            colors[79] = color;
            colors[12] = Color.FromArgb(174, 24, 69);
            colors[13] = Color.FromArgb(133, 213, 247);
            color = Color.FromArgb(144, 148, 144);
            colors[17] = color;
            colors[16] = Color.FromArgb(140, 130, 116);
            colors[26] = Color.FromArgb(119, 101, 125);
            colors[36] = Color.FromArgb(230, 89, 92);
            colors[28] = Color.FromArgb(151, 79, 80);
            colors[29] = Color.FromArgb(175, 105, 128);
            colors[51] = Color.FromArgb(192, 202, 203);
            colors[31] = Color.FromArgb(141, 120, 168);
            colors[32] = Color.FromArgb(151, 135, 183);
            colors[42] = Color.FromArgb(251, 235, 127);
            colors[50] = Color.FromArgb(170, 48, 114);
            colors[69] = Color.FromArgb(190, 150, 92);
            colors[77] = Color.FromArgb(238, 85, 70);
            colors[78] = Color.FromArgb(121, 110, 97);
            colors[49] = Color.FromArgb(89, 201, 255);
            colors[35] = Color.FromArgb(226, 145, 30);
            colors[34] = Color.FromArgb(235, 166, 135);
            colors[21] = Color.FromArgb(174, 129, 92);
            colors[27] = Color.FromArgb(54, 154, 54);
            Color[] colors2 = new Color[14];
            for (int i = 0; i < 14; i++)
            {
                colors2[i] = Color.Black;
            }
            color = Color.FromArgb(52, 52, 52);
            colors2[1] = color;
            colors2[5] = color;
            colors2[2] = Color.FromArgb(88, 61, 46);
            colors2[3] = Color.FromArgb(61, 58, 78);
            colors2[4] = Color.FromArgb(73, 51, 36);
            colors2[6] = Color.FromArgb(91, 30, 30);
            colors2[7] = Color.FromArgb(27, 31, 42);
            colors2[8] = Color.FromArgb(31, 39, 26);
            colors2[9] = Color.FromArgb(41, 28, 36);
            colors2[10] = Color.FromArgb(74, 62, 12);
            colors2[11] = Color.FromArgb(46, 56, 59);
            colors2[12] = Color.FromArgb(75, 32, 11);
            colors2[13] = Color.FromArgb(67, 37, 37);
            double num31 = maxTilesY - 230;
            double num32 = (int)((num31 - worldSurface) / 6.0) * 6;
            num31 = worldSurface + num32 - 5.0 + 37;
            for (int Xcount = 0; Xcount < maxTilesX; Xcount++)
            {
                for (int Ycount = 0; Ycount < maxTilesY; Ycount++)
                {
                    if (tile[Xcount, Ycount].active)
                    {
                        map.SetPixel(Xcount, Ycount, colors[tile[Xcount, Ycount].type]);
                    }
                    else if (tile[Xcount, Ycount].liquid > 0)
                    {
                        if (tile[Xcount, Ycount].lava)
                        {
                            map.SetPixel(Xcount, Ycount, Color.FromArgb(253, 32, 3));
                        }
                        else
                        {
                            map.SetPixel(Xcount, Ycount, Color.FromArgb(9, 61, 191));
                        }
                    }
                    else if (tile[Xcount, Ycount].wall != 0)
                    {
                        map.SetPixel(Xcount, Ycount, colors2[tile[Xcount, Ycount].wall]);
                    }
                    else
                    {
                        if (Ycount > num31)
                        {
                            map.SetPixel(Xcount, Ycount, Color.FromArgb(46, 45, 42));
                        }
                        else if (Ycount > rockLayer + 37)
                        {
                            map.SetPixel(Xcount, Ycount, Color.FromArgb(74, 67, 60));
                        }
                        else if (Ycount > worldSurface - 1)
                        {
                            map.SetPixel(Xcount, Ycount, Color.FromArgb(88, 61, 46));
                        }
                        else
                        {
                            map.SetPixel(Xcount, Ycount, Color.FromArgb(132, 170, 248));
                        }
                    }
                }
            }
            preview = map;
            if (MainWindow.selectedpath == null)
            {
                map.Save(worldName + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                map.Save(MainWindow.selectedpath + "\\" + worldName + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
            previewGenerated = true;
        }
    }
}
