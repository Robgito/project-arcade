using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Effects;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for SpeelSnake.xaml
    /// </summary>
    public partial class SpeelSnake : Window
    {
        private Gebruiker _ingelogdeGebruiker;
        MessageBoxResult antwoordMessageBox;
        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }
        public SpeelSnake()
        {
            InitializeComponent();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Muziek.CoinGeluid();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het teruggaan naar het vorige menu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshHighscores();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Muziek.CoinGeluid();
                if (Credits.SnakeCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.SnakeCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);

                    BlurAchtergrond(20);

                    Snake snake = new Snake();
                    snake.IngelogdeGebruiker = this.IngelogdeGebruiker;
                    snake.ShowDialog();

                    RefreshHighscores();
                    BlurAchtergrond(0);
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van Snake.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.Close();

            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het teruggaan naar het vorige menu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshHighscores()
        {
            lbHighscores.ItemsSource = null;
            List<ScoreSnake> scores = Datamanager.GetScoresSnake();

            if (scores != null)
            {
                var gesorteerdeScores = scores.OrderByDescending(score => score.Score).ToList();
                lbHighscores.ItemsSource = gesorteerdeScores;
            }
            else
            {
                MessageBox.Show("Geen highscores gevonden");
            }
        }
        private void BlurAchtergrond(int radius)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = radius;

            GridMain.Effect = blur;
        }
    }
}
