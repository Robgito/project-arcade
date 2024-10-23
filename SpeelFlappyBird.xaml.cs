using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for SpeelFlappyBird.xaml
    /// </summary>
    public partial class SpeelFlappyBird : Window
    {
        public SpeelFlappyBird()
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
            RefreshScoreboard();
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Muziek.CoinGeluid();
            BlurAchtergrond(20);
            FlappyBirdGame flappyBirdGame = new FlappyBirdGame();
            flappyBirdGame.IngelogdeGebruiker = this.IngelogdeGebruiker;
            flappyBirdGame.ShowDialog();
            RefreshScoreboard();
            BlurAchtergrond(0);
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
            List<ScoreFlappyBird> scores = Datamanager.GetScoresFlappyBird();

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
