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

        enum YUVFormat
        {
            YUV444 = 444,
            YUV422 = 422,
            YUV420 = 420
        }

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
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            if (chOption444.IsChecked)
            {
                frame = bytes.Length / (pixelCount * 3);
                int yCount = pixelCount / 3, uCount = yCount, vCount = uCount;
                for(int f = 0; f < frame; f++)
                {

                }
            }
            else if (chOption422.IsChecked)
            {
                frame = bytes.Length / (pixelCount * 2);
                int yCount = pixelCount * 2 / 3, uCount = yCount / 2, vCount = uCount;
            }
            else if (chOption420.IsChecked)
            {
                frame = (bytes.Length * 2) / (pixelCount * 3);
                int yCount = pixelCount, uCount = pixelCount / 6, vCount = uCount;
                int byteCount = pixelCount * 3;
                for (int f = 0; f < frame; f++)
                {
                    int i = 0, j = 0;
                    byte[] y = new byte[yCount], u = new byte[uCount], v = new byte[vCount];
                    for (int p = 0; p < yCount; p++)
                    {
                        y[i++] = bytes[f * byteCount + j++];
                    }
                    i = 0;
                    for (int p = 0; p < uCount; p++)
                    {
                        u[i++] = bytes[f * byteCount + j++];
                    }
                    i = 0;
                    for (int p = 0; p < vCount; p++)
                    {
                        v[i++] = bytes[f * byteCount + j++];
                    }

                    byte[] data = new byte[yCount * 3];
                    for (int d = 0; d < yCount * 3; d++)
                    {
                        data[d] = y[d / 3];
                    }

                    BitmapData bmpData = bitmap.LockBits(
                       new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                       ImageLockMode.WriteOnly, bitmap.PixelFormat);
                    Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
                    bitmap.UnlockBits(bmpData);

                    bitmap.Save(openFileDialog.InitialDirectory + "//file//bit" + f + ".bmp");
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
                bitmap.Save("C:\\Kullanıcılar\\YasinEmir\\Source\\repos\\tarikbir\\YUVReader\\Image.png");
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

