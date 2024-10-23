using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;
using Zombie_shooter;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for SpeelZombieShooter.xaml
    /// </summary>
    public partial class SpeelZombieShooter : Window
    {
        public SpeelZombieShooter()
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
            lbHighscores.ItemsSource = null;
            List<ScoreZombieShooter> scores = Datamanager.GetScoresZombieShooter();

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

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Muziek.CoinGeluid();

                    BlurAchtergrond(20);

                    ZombieShooterGame zombieShooterGame = new ZombieShooterGame();
                    zombieShooterGame.IngelogdeGebruiker = this.IngelogdeGebruiker;
                    zombieShooterGame.ShowDialog();
                    RefreshHighscores();

                    BlurAchtergrond(0);
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van Zombieshooter.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void RefreshHighscores()
        {
            lbHighscores.ItemsSource = null;
            List<ScoreZombieShooter> scores = Datamanager.GetScoresZombieShooter();

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
