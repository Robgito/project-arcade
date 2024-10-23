using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for FlappyBirdGame.xaml
    /// </summary>
    public partial class FlappyBirdGame : Window
    {

        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Render);

        double currentScore;
        int gravity = 4;
        bool gameOver;
        Rect flappyBirdHitBox;

        int teller = 0;
        int[] arrVariation = new int[14] { 0, 0, -10, -10, 10, 10, -20, -20, 20, 20, -30, -30, 30, 30 };


        public FlappyBirdGame()
        {
            InitializeComponent();
            btnSpeel.Content = $"Start ({Credits.FlappyBirdCost} credits)";
            gameTimer.Tick += MainEventTimer;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        private void MainEventTimer(object sender, EventArgs e)
        {
            txtScore.Content = "Score: " + currentScore;

            flappyBirdHitBox = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 5, flappyBird.Height);

            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);

            if (Canvas.GetTop(flappyBird) < -10 || Canvas.GetTop(flappyBird) > 458)
            {
                EndGame();
            }

            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3" || (string)x.Tag == "obs4" || (string)x.Tag == "obs5")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);

                    if (Canvas.GetLeft(x) < -100)
                    {
                        Canvas.SetLeft(x, 1400);
                        Canvas.SetTop(x, Canvas.GetTop(x) + arrVariation[teller]);
                        teller++;
                        if (teller >= 14) teller = 0;
                        currentScore += .5;
                    }

                    Rect pipeHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (flappyBirdHitBox.IntersectsWith(pipeHitbox))
                    {
                        EndGame();
                    }
                }
                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 1);

                    if (Canvas.GetLeft(x) < -250)
                    {
                        Canvas.SetLeft(x, 550);
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
                gravity = -10;
                string path = Environment.CurrentDirectory;
                SoundPlayer player = new SoundPlayer(path + "\\SoundsFlappy\\sfx_wing.wav");
                player.Play();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            gravity = 4;
        }

        private void StartGame()
        {
            MyCanvas.Focus();

            int temp = 300;

            currentScore = 0;
            gameOver = false;

            Canvas.SetTop(flappyBird, 190);

            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 500);
                }
                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 800);
                }
                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 1100);
                }
                if ((string)x.Tag == "obs4")
                {
                    Canvas.SetLeft(x, 1400);
                }
                if ((string)x.Tag == "obs5")
                {
                    Canvas.SetLeft(x, 1700);
                }

                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, 300 + temp);
                    temp = 800;
                }
            }

            gameTimer.Start();
        }

        private void EndGame()
        {
            ScoreFlappyBird huidigeScore = Datamanager.GetScoreFlappyBirdGebruiker(IngelogdeGebruiker.UserID);
            if (huidigeScore != null)
            {
                if (huidigeScore.Score < currentScore)
                {
                    huidigeScore.Score = Convert.ToInt32(currentScore);
                    Datamanager.UpdateScoreFlappyBird(huidigeScore);
                }
            }
            else
            {
                ScoreFlappyBird flappyScore = new ScoreFlappyBird();

                int nieuweScore = Convert.ToInt32(currentScore);

                flappyScore.Score = nieuweScore;


                //ScoreID
                List<ScoreFlappyBird> scoreFlappyBirds = Datamanager.GetScoresFlappyBird();

                if (scoreFlappyBirds != null)
                {
                    flappyScore.ScoreID = scoreFlappyBirds.Count + 1;
                }
                else
                {
                    flappyScore.ScoreID = 1;
                }

                flappyScore.FKGebruiker = IngelogdeGebruiker.UserID;
                Datamanager.InsertScoreFlappyBird(flappyScore);
            }

            gameTimer.Stop();
            gameOver = true;

            try
            {
                var GetAllScores = Datamanager.GetScoresFlappyBird();
                if (GetAllScores != null)
                {

                }
                else
                {
                    ScoreFlappyBird newScore = new ScoreFlappyBird();
                    newScore.FKGebruiker = IngelogdeGebruiker.UserID;
                    newScore.Score = (int)currentScore;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            spMenu.Visibility = Visibility.Visible;
            spMenu.IsEnabled = true;
            Panel.SetZIndex(spMenu, 2);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rdoBird.IsChecked = true;
        }


        private void btnAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSpeel_Click(object sender, RoutedEventArgs e)
        {
            if (IngelogdeGebruiker.Credits - Credits.FlappyBirdCost >= 0)
            {
                IngelogdeGebruiker.Credits -= Credits.FlappyBirdCost;
                Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                spMenu.Visibility = Visibility.Hidden;
                spMenu.IsEnabled = false;
                if (rdoHans.IsChecked == true)
                {
                    flappyBird.Source = new BitmapImage(new Uri(@"ImagesFlappy\flappyHans.png", UriKind.Relative));
                }
                else
                {
                    flappyBird.Source = new BitmapImage(new Uri(@"ImagesFlappy\flappyBird.png", UriKind.Relative));

                }
                Panel.SetZIndex(spMenu, 0);
                StartGame();
            }
            else
            {
                MessageBox.Show("Niet genoeg credits! Koop credits in de credits store!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
            }
        }
    }
}

