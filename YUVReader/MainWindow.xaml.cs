using Microsoft.Win32;
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

namespace YUVReader
{
    public partial class MainWindow : Window
    {
        string File;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "YUV files (*.yuv)|*.yuv";
            openFileDialog.Title = "Select a valid YUV file...";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() != true) return;

            if (OpenFile(openFileDialog.FileName))
            {
                lblNoFileSelectedError.Visibility = Visibility.Hidden;
                mainGrid.Background.Opacity = 0.4;
            }
        }

        private bool OpenFile(string fileName)
        {
            File = fileName;
            return true;
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
