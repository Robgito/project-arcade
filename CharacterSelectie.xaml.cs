using System.Media;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for CharacterSelectie.xaml
    /// </summary>
    public partial class CharacterSelectie : Window
    {
        private string _selectedImage;
        public string SelectedImage
        {
            get { return _selectedImage; }
            set { _selectedImage = value; }
        }

        public CharacterSelectie()
        {
            InitializeComponent();
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel1.png";
            ResetImageBorders();
            border1.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();
            this.DialogResult = true;
            this.Close();
        }

        private void imgProfiel2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel2.png";
            ResetImageBorders();
            border2.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel3.png";
            ResetImageBorders();
            border3.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel4.png";
            ResetImageBorders();
            border4.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel5.png";
            ResetImageBorders();
            border5.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel6.png";
            ResetImageBorders();
            border6.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel7.png";
            ResetImageBorders();
            border7.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel8.png";
            ResetImageBorders();
            border8.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel9.png";
            ResetImageBorders();
            border9.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel10_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel10.png";
            ResetImageBorders();
            border10.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel11.png";
            ResetImageBorders();
            border11.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void imgProfiel12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedImage = @"\Images\profiel12.jpg";
            ResetImageBorders();
            border12.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA55D3");
        }

        private void ResetImageBorders()
        {
            border1.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border2.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border3.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border4.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border5.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border6.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border7.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border8.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border9.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border10.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border11.BorderBrush = new SolidColorBrush(Colors.Transparent);
            border12.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void CoinGeluid()
        {
            try
            {
                string path = Environment.CurrentDirectory;
                SoundPlayer player = new SoundPlayer(path + "\\Sounds\\RetroGameCoinSoundEffect.wav");
                player.Play();
            }
            catch (Exception)
            {
                Console.WriteLine("Er is een fout opgetreden bij het laden van het coin geluid.");
            }
        }

    }
}
