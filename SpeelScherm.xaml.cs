using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Media.Effects;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for SpeelScherm.xaml
    /// </summary>
    public partial class SpeelScherm : Window
    {
        public SpeelScherm()
        {
            InitializeComponent();

        }

        MessageBoxResult antwoordMessageBox;
        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het teruggaan naar het vorige menu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshScoreboard();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();

            //credits logica Tetris:
            try
            {
                if (Credits.TetrisCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.TetrisCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);

                    BlurAchtergrond(20);

                    Tetris tetris = new Tetris();
                    tetris.IngelogdeGebruiker = this.IngelogdeGebruiker;
                    tetris.ShowDialog();

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
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van Tetris.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
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
            List<ScoreTetris> scores = Datamanager.GetScoresTetris();

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
