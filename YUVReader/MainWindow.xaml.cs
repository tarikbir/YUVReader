using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using Accord.Video.FFMPEG;
using YuvFormatter;
using System.IO;

namespace YUVReader
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //IsPlaying(false);
        }

        /*private void IsPlaying(bool controller)
        {
            btnControl.IsEnabled = controller;
        }*/

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "YUV";
            openFileDialog.Filter = "YUV files (*.yuv)|*.yuv";
            openFileDialog.Title = "Select a valid YUV file...";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //Nullable<bool> result = openFileDialog.ShowDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(openFileDialog.FileName);
                lblNoFileSelectedError.Visibility = Visibility.Hidden;
                mediaViewer.Source = new Uri(openFileDialog.FileName);
                mainGrid.Background.Opacity = 0.4;
                btnReadFile.IsEnabled = true;
                btnPlay.IsEnabled = true;
                mediaViewer.Visibility = Visibility.Visible;
            }
            else
            {
                return;
            }
            var bytes = File.ReadAllBytes(openFileDialog.FileName);

            var bmp = YuvFormatter.YuvConverter.SourceFromYuv(bytes, 300, 300);

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
                if(sender != item)
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
            if(mediaViewer.Source != null)
            {
                mediaViewer.Play();
                btnPause.IsEnabled = true;
            }
                
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (mediaViewer.CanPause)
            {
                mediaViewer.Pause();
            }
                
        }
    }

    }

