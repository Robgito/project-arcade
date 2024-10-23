using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Effects;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for SpeelRunner.xaml
    /// </summary>
    public partial class SpeelRunner : Window
    {
        MessageBoxResult antwoordMessageBox;
        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }
        public SpeelRunner()
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
            RefreshScoreboard();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Muziek.CoinGeluid();
                if (Credits.EndlessRunnerCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.EndlessRunnerCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);

                    BlurAchtergrond(20);

                    EndlessRunner runner = new EndlessRunner();
                    runner.IngelogdeGebruiker = this.IngelogdeGebruiker;
                    runner.ShowDialog();
                    RefreshScoreboard();

                    BlurAchtergrond(0);
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van Endless Runner.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void BlurAchtergrond(int radius)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = radius;

            GridMain.Effect = blur;
        }

        private void RefreshScoreboard()
        {
            lbHighscores.ItemsSource = null;
            List<ScoreEndlessRunner> scores = Datamanager.GetScoresEndlessRunner();

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
    }
}
