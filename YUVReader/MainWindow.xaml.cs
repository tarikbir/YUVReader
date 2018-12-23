using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace YUVReader
{
    public partial class MainWindow : Window
    {
        int sizeHeight, sizeWidth;

        public MainWindow()
        {
            InitializeComponent();
            sizeHeight = 0;
            sizeWidth = 0;
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "YUV";
            openFileDialog.Filter = "YUV files (*.yuv)|*.yuv";
            openFileDialog.Title = "Select a valid YUV file...";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            byte[] fileData;

            if (openFileDialog.ShowDialog() == true)
            {
                //MessageBox.Show(openFileDialog.FileName);
                lblNoFileSelectedError.Visibility = Visibility.Hidden;
                mainGrid.Background.Opacity = 0.4;
                btnReadFile.IsEnabled = true;
                btnPlay.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No file has been selected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                fileData = File.ReadAllBytes(openFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File cannot be read!\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            int width = 176, height = 144;

            if (chOption444.IsChecked)
            {
                var a = RGBConvert.ConvertRGB(fileData, width, height, YUV.YUVFormat.YUV444);
            }
            else if (chOption422.IsChecked)
            {
                var a = RGBConvert.ConvertRGB(fileData, width, height, YUV.YUVFormat.YUV422);
            }
            else if (chOption420.IsChecked)
            {
                var a = RGBConvert.ConvertRGB(fileData, width, height, YUV.YUVFormat.YUV420);
            }
            else
                MessageBox.Show("Unkown error while selecting the format!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //var bmp = YuvFormatter.YuvConverter.SourceFromYuv(bytes, 300, 300);


                //mediaViewer.Source = openFileDialog.OpenFile();
                //mediaViewer.Visibility = Visibility.Visible;
                //mediaViewer.LoadedBehavior = MediaState.Manual;
                //mediaViewer.Play();
            }


        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuFormat_Checked(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menuFormat.Items)
            {
                if (sender != item)
                {
                    item.Unchecked -= MenuFormat_Unchecked;
                    item.IsChecked = false;
                    item.Unchecked += MenuFormat_Unchecked;
                }
            }
        }

        private void MenuFormat_Unchecked(object sender, RoutedEventArgs e)
        {
            var item = (sender as MenuItem);
            item.Checked -= MenuFormat_Checked;
            item.IsChecked = true;
            item.Checked += MenuFormat_Checked;
            e.Handled = true;
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "*.bmp";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp";
            saveFileDialog.Title = "Select a path to save bitmap";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            else
            {
                Bitmap bitmap = new Bitmap(saveFileDialog.FileName);
                //bitmap.Save(saveFileDialog.InitialDirectory + "//file//bit" + FRAME + ".bmp");
            }
            
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}

