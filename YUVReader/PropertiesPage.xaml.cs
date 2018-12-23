using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YUVReader
{
    public partial class PropertiesPage : Window
    {
        public RGBVideo OpenedVideo { get; set; }

        public PropertiesPage()
        {
            InitializeComponent();
        }

        public PropertiesPage(string title) : this()
        {
            Title = "Properties of " + title;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            YUV.YUVFormat format = (cbYUV444.IsSelected) ? YUV.YUVFormat.YUV444 : (cbYUV422.IsSelected) ? YUV.YUVFormat.YUV422 : YUV.YUVFormat.YUV420;
            int width = 0, height = 0;

            if (!Int32.TryParse(txtWidth.Text, out width))
            {
                MessageBox.Show("There was an error while parsing width value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Int32.TryParse(txtHeight.Text, out height))
            {
                MessageBox.Show("There was an error while parsing height value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            OpenedVideo = new RGBVideo(format, width, height);
            DialogResult = true;
        }

        private void BtnHeightDefault_Click(object sender, RoutedEventArgs e)
        {
            txtHeight.Text = Config.DefaultHeight.ToString();
        }

        private void BtnWidthDefault_Click(object sender, RoutedEventArgs e)
        {
            txtWidth.Text = Config.DefaultWidth.ToString();
        }

        private void TxtWidth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9]+").IsMatch(e.Text);
        }

        private void TxtHeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9]+").IsMatch(e.Text);
        }
    }
}
