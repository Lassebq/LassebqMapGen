using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LassebqMapGen
{
    public partial class Preview : Window
    {
        public Preview()
        {
            InitializeComponent();
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
            BitmapImage viewimg = ToBitmapImage(MapGenerator.preview);
            this.image.BeginInit();
            this.image.Source = viewimg;
            this.image.EndInit();
        }
        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            System.Windows.Point p = e.MouseDevice.GetPosition(image);

            Matrix m = image.RenderTransform.Value;
            if (e.Delta > 0)
                m.ScaleAtPrepend(1.1, 1.1, p.X, p.Y);
            else
                m.ScaleAtPrepend(1 / 1.1, 1 / 1.1, p.X, p.Y);

            image.RenderTransform = new MatrixTransform(m);
        }

        System.Windows.Point start;
        System.Windows.Point origin;
        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (image.IsMouseCaptured) return;
            image.CaptureMouse();

            start = e.GetPosition(border);
            origin.X = image.RenderTransform.Value.OffsetX;
            origin.Y = image.RenderTransform.Value.OffsetY;
        }
        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point p = e.MouseDevice.GetPosition(border);

            Matrix m = image.RenderTransform.Value;
            m.OffsetX = origin.X + (p.X - start.X);
            m.OffsetY = origin.Y + (p.Y - start.Y);
            //tooltip.Visibility = Visibility.Collapsed;
            var effectiveScale = (m.M11 / m.M22) / 2;
            int hovertilex = (int)((p.X - m.OffsetX) / effectiveScale);
            int hovertiley = (int)((p.Y - m.OffsetY) / effectiveScale);
            if (hovertilex >= 0 && hovertiley >= 0 && hovertilex < MapGenerator.maxTilesX && hovertiley < MapGenerator.maxTilesY)
            {
                if (MapGenerator.tile[hovertilex, hovertiley].active)
                {
                    //tooltip.Content = MapGenerator.tileNames[MapGenerator.tile[hovertilex, hovertiley].type];
                }
                else
                {
                    //tooltip.Content = "Air";
                }
            }
            else
            {
                //tooltip.Content = "Out of bounds";
            }
            if (!image.IsMouseCaptured) return;

            image.RenderTransform = new MatrixTransform(m);
        }
        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            image.ReleaseMouseCapture();
        }
        private BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
