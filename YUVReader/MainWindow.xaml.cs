using Microsoft.Win32;
using System;
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
        bool fileOpened = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetWindow()
        {
            lblNoFileSelectedError.Visibility = Visibility.Visible;
            mainGrid.Background.Opacity = 0.7;
            grdFileInfo.Visibility = Visibility.Hidden;
            openedVideo = null;
            frameShowing = 0;
            timer = null;
            txtAttributes.Content = String.Empty;
            txtCreationTime.Content = String.Empty;
            txtCurrentFrame.Content = "1";
            txtFileName.Content = String.Empty;
            txtFrame.Content = String.Empty;
            txtHeight.Content = String.Empty;
            txtWidth.Content = String.Empty;
            fileOpened = false;
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            if (fileOpened) { ResetWindow(); }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "YUV files (*.yuv)|*.yuv";
            openFileDialog.Title = "Select a valid YUV file...";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            byte[] fileData;

            if (openFileDialog.ShowDialog() == true)
            {
                PropertiesPage propertySet = new PropertiesPage(openFileDialog.FileName);
                if (!propertySet.ShowDialog() ?? false) return;
                fileOpened = true;
                openedVideo = new RGBVideo(propertySet.OpenedVideo);
                lblNoFileSelectedError.Visibility = Visibility.Hidden;
                mainGrid.Background.Opacity = 0.4;
                grdFileInfo.Visibility = Visibility.Visible;
                txtFileName.Content = openFileDialog.FileName;
                txtHeight.Content = openedVideo.Height;
                txtWidth.Content = openedVideo.Width;
                txtCreationTime.Content = File.GetCreationTime(openFileDialog.FileName);
                txtAttributes.Content = File.GetAttributes(openFileDialog.FileName).ToString();
            }
            else
            {
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
            txtFrame.Content = openedVideo.Frame;

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
            txtCurrentFrame.Content = frameShowing+1;
            frameShowing++;
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (!fileOpened)
            {
                MessageBox.Show("No file opened to extract as bitmaps!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select a path to extract bitmaps";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() == true)
            {
                for (int i = 0; i < openedVideo.Frame; i++)
                {
                    openedVideo.Source[i].Save(saveFileDialog.FileName + " (" + (i + 1) + ").bmp");
                }
                MessageBox.Show("Extraction completed!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}

