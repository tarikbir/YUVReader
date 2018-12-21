using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using Accord.Video.FFMPEG;
using YuvFormatter;
using System.IO;
using System.Drawing;
using System.Windows.Media;

namespace YUVReader
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "YUV";
            openFileDialog.Filter = "YUV files (*.yuv)|*.yuv";
            openFileDialog.Title = "Select a valid YUV file...";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(openFileDialog.FileName);
                lblNoFileSelectedError.Visibility = Visibility.Hidden;
                mainGrid.Background.Opacity = 0.4;
                btnReadFile.IsEnabled = true;
                btnPlay.IsEnabled = true;
            }
            else
            {
                return;
            }
            var bytes = File.ReadAllBytes(openFileDialog.FileName);

            int width = 176, height = 144, pixelCount = width * height, frame;
            if (chOption444.IsChecked)
            {
                frame = bytes.Length / (pixelCount * 3);
                int yCount = pixelCount * 1 / 3, uCount = yCount, vCount = uCount;
                for (int f = 0; f < frame; f++)
                {
                    int i = 0, ymax = ymax = f * pixelCount + yCount, umax = ymax + uCount, vmax = umax + vCount;
                    byte[] y = new byte[yCount], u = new byte[uCount], v = new byte[vCount];
                    for (int p = f * pixelCount; p < ymax; p++)
                    {
                        y[i++] = bytes[p];
                    }
                    i = 0;
                    for (int p = ymax; p < umax; p++)
                    {
                        u[i++] = bytes[p];
                    }
                    i = 0;
                    for (int p = umax; p < vmax; p++)
                    {
                        v[i++] = bytes[p];
                    }

                }
            }
            else if (chOption422.IsChecked)
            {
                frame = bytes.Length / (pixelCount * 2);
                int yCount = pixelCount * 2 / 3, uCount = yCount / 8, vCount = uCount;
                for (int f = 0; f < frame; f++)
                {
                    int i = 0, ymax = f * pixelCount + yCount, umax = ymax + uCount, vmax = umax + vCount;
                    byte[] y = new byte[yCount], u = new byte[uCount], v = new byte[vCount];
                    for (int p = f * pixelCount; p < ymax; p++)
                    {
                        y[i++] = bytes[p];
                    }
                    i = 0;
                    for (int p = ymax; p < umax; p++)
                    {
                        u[i++] = bytes[p];
                    }
                    i = 0;
                    for (int p = umax; p < vmax; p++)
                    {
                        v[i++] = bytes[p];
                    }

                }
            }
            else if (chOption420.IsChecked)
            {
                frame = (bytes.Length * 2) / (pixelCount * 3);
                int yCount = pixelCount * 2 / 3, uCount = yCount / 4, vCount = uCount; 
                for (int f = 0; f < frame; f++)
                {
                    int i = 0, ymax = f * pixelCount + yCount, umax = ymax + uCount, vmax = umax + vCount;
                    byte[] y = new byte[yCount], u = new byte[uCount], v = new byte[vCount];
                    for (int p = f * pixelCount; p < ymax; p++)
                    {
                        y[i++] = bytes[p];
                    }
                    i = 0;
                    for (int p = ymax; p < umax; p++)
                    {
                        u[i++] = bytes[p];
                    }
                    i = 0;
                    for (int p = umax; p < vmax; p++)
                    {
                        v[i++] = bytes[p];
                    }
                }
            }
            else
                MessageBox.Show("Error");
            


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
            //seciliobje = (sender as MenuItem).Header.ToString();
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
            if (saveFileDialog.ShowDialog() != true) return;
            //btmp.Save(saveFileDialog.FileName);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (Image.Source != null)
            {
                
            }

        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (Image.Source != null)
            {
              
            }

        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}

