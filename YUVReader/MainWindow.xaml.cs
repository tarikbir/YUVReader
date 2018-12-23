using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace YUVReader
{
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        RGBVideo openedVideo;
        int frameShowing = 0;

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
            byte[] fileData;

            if (openFileDialog.ShowDialog() == true)
            {
                PropertiesPage propertySet = new PropertiesPage(openFileDialog.FileName);
                if (!propertySet.ShowDialog() ?? false) return;
                openedVideo = new RGBVideo(propertySet.OpenedVideo);
                lblNoFileSelectedError.Visibility = Visibility.Hidden;
                mainGrid.Background.Opacity = 0.4;
                btnImage.Visibility = Visibility.Visible;
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

            int width = openedVideo.Width, height = openedVideo.Height;
            openedVideo = RGBConvert.ConvertRGB(fileData, width, height, openedVideo.Format);
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (frameShowing >= openedVideo.Frame) frameShowing = 0;
            MemoryStream ms = new MemoryStream();
            openedVideo.Source[frameShowing].Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Byte[] bytes = ms.ToArray();
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = ms;
            imageSource.EndInit();
            imageOpened.Source = imageSource;
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            if (openedVideo == null || openedVideo.Source == null) return;
            if (timer != null)
            {
                if (timer.IsEnabled) timer.Stop();
                else timer.Start();
            }
            else
            {
                timer = new System.Windows.Threading.DispatcherTimer();
                timer.Tick += TimerUpdate;
                timer.Interval = new TimeSpan(300000);
                timer.Start();
            }
        }

        private void TimerUpdate(object sender, EventArgs e)
        {
            UpdateImage();
            frameShowing++;
            lbatyar.Content = "FRAME SHOWING: " + frameShowing;
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
    }

}

