using loginscreen_games;
using System.Media;
using System;
using System.Windows;
using System.Windows.Input;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for CreditStore.xaml
    /// </summary>
    public partial class CreditStore : Window
    {
        public CreditStore()
        {
            InitializeComponent();
        }

        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblCurrentCredits.Content = "Je hebt momenteel " + IngelogdeGebruiker.Credits + " credits.";
        }

        private void btnHoofdmenu_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();
            
            this.Close();
        }

        private void btnCreds1_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();

            if (checkToestemming.IsChecked == true)
            {
                var zeker = MessageBox.Show("Bent u zeker dat u credits wil kopen?", "Credits kopen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    IngelogdeGebruiker.Credits += 4;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                    lblCurrentCredits.Content = "Je hebt momenteel " + IngelogdeGebruiker.Credits + " credits.";
                    MessageBox.Show("Een succesvolle aankoop!", "Succes!", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Gelieve eerst toestemming te geven.", "Geen toestemming", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreds2_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();

            if (checkToestemming.IsChecked == true)
            {
                var zeker = MessageBox.Show("Bent u zeker dat u credits wil kopen?", "Credits kopen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    IngelogdeGebruiker.Credits += 11;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                    lblCurrentCredits.Content = "Je hebt momenteel " + IngelogdeGebruiker.Credits + " credits.";
                    MessageBox.Show("Een succesvolle aankoop!", "Succes!", MessageBoxButton.OK);

                }
            }
            else
            {
                MessageBox.Show("Gelieve eerst toestemming te geven.", "Geen toestemming", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreds3_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();

            if (checkToestemming.IsChecked == true)
            {
                var zeker = MessageBox.Show("Bent u zeker dat u credits wil kopen?", "Credits kopen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    IngelogdeGebruiker.Credits += 23;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                    lblCurrentCredits.Content = "Je hebt momenteel " + IngelogdeGebruiker.Credits + " credits.";
                    MessageBox.Show("Een succesvolle aankoop!", "Succes!", MessageBoxButton.OK);

                }
            }
            else
            {
                MessageBox.Show("Gelieve eerst toestemming te geven.", "Geen toestemming", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreds4_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();

            if (checkToestemming.IsChecked == true)
            {
                var zeker = MessageBox.Show("Bent u zeker dat u credits wil kopen?", "Credits kopen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    IngelogdeGebruiker.Credits += 60;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                    lblCurrentCredits.Content = "Je hebt momenteel " + IngelogdeGebruiker.Credits + " credits.";
                    MessageBox.Show("Een succesvolle aankoop!", "Succes!", MessageBoxButton.OK);

                }
            }
            else
            {
                MessageBox.Show("Gelieve eerst toestemming te geven.", "Geen toestemming", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
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
