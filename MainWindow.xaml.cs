using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace LassebqMapGen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DialogResult result;

        public static bool isWorldSelected;

        public static string selectedpath;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isWorldSelected)
            {
                if (!MapGenerator.previewGenerated)
                {
                    MapGenerator.Generate();
                }
                Preview preview = new Preview();
                preview.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.DefaultExt = ".wld";
                dialog.Filter = "Terraria worlds (.wld)|*.wld";
                dialog.InitialDirectory = MapGenerator.WorldPath;
                result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    MapGenerator.OpenWorld(dialog.FileName);
                    button1.Content = MapGenerator.worldName;
                }
                isWorldSelected = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    selectedpath = dialog.SelectedPath;
                    button2.Content = dialog.SelectedPath;
                }
            }
        }
    }
}
