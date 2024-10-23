using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for EndlessRunner.xaml
    /// </summary>
    public partial class EndlessRunner : Window
    {
        MessageBoxResult antwoordMessageBox;
        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }
        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Render);

        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        bool jumping;
        int force = 20;
        int speed = 5;
        Random random = new Random();
        bool gameOver;
        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] obstaclePosition = { 320, 310, 300, 305, 315 };
        int score = 0;
        public EndlessRunner()
        {
            InitializeComponent();
            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/background.gif"));

            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            StartGame();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            try
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);

                if (Canvas.GetLeft(background) < -1262)
                {
                    Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
                }
                if (Canvas.GetLeft(background2) < -1262)
                {
                    Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
                }

                Canvas.SetTop(player, Canvas.GetTop(player) + speed);
                Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

                scoreText.Content = "Score: " + score;

                playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
                obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
                groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

                if (playerHitBox.IntersectsWith(groundHitBox))
                {
                    speed = 0;
                    Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                    jumping = false;
                    spriteIndex += 0.5;
                    if (spriteIndex > 8)
                    {
                        spriteIndex = 1;
                    }
                    RunSprite(spriteIndex);
                }
                if (jumping == true)
                {
                    speed = -9;
                    force--;

                }
                else
                {
                    speed = 12;
                }
                if (force < 0)
                {
                    jumping = false;
                }

                if (Canvas.GetLeft(obstacle) < -50)
                {
                    Canvas.SetLeft(obstacle, 950);
                    Canvas.SetTop(obstacle, obstaclePosition[random.Next(0, obstaclePosition.Length)]);


                    score += 5;

                }
                if (playerHitBox.IntersectsWith(obstacleHitBox))
                {
                    gameOver = true;
                    gameTimer.Stop();
                }
                if (gameOver == true)
                {
                    obstacle.Stroke = Brushes.Black;
                    obstacle.StrokeThickness = 1;

                    player.Stroke = Brushes.Red;
                    player.StrokeThickness = 1;

                    FinalScoreText.Text = "Score: " + score;

                    GameOverMenu.Visibility = Visibility.Visible;
                    Muziek.GameOverRun();

                    ScoreEndlessRunner huidigeScore = Datamanager.GetScoreEndlessRunnerGebruiker(IngelogdeGebruiker.UserID);
                    if (huidigeScore != null)
                    {
                        if (huidigeScore.Score < score)
                        {
                            huidigeScore.Score = Convert.ToInt32(score);
                            Datamanager.UpdateScoreEndlessRunner(huidigeScore);
                        }
                    }
                    else
                    {
                        ScoreEndlessRunner runnerScore = new ScoreEndlessRunner();

                        int nieuweScore = Convert.ToInt32(score);

                        runnerScore.Score = nieuweScore;


                        //ScoreID
                        List<ScoreEndlessRunner> scoreEndlessRunner = Datamanager.GetScoresEndlessRunner();

                        if (scoreEndlessRunner != null)
                        {
                            runnerScore.ScoreID = scoreEndlessRunner.Count + 1;
                        }
                        else
                        {
                            runnerScore.ScoreID = 1;
                        }

                        runnerScore.FKGebruiker = IngelogdeGebruiker.UserID;
                        Datamanager.InsertScoreEndlessRunner(runnerScore);
                    }

                }
                else
                {
                    player.StrokeThickness = 0;
                    obstacle.StrokeThickness = 0;
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden in de game engine.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartGame()
        {
            try
            {
                Canvas.SetLeft(background, 0);
                Canvas.SetLeft(background2, 1262);

                Canvas.SetLeft(player, 110);
                Canvas.SetTop(player, 140);

                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, 310);

                RunSprite(1);

                obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/obstacle.png"));
                obstacle.Fill = obstacleSprite;

                jumping = false;
                gameOver = false;
                score = 0;
                scoreText.Content = "Score: " + score;

                gameTimer.Start();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij start game.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RunSprite(double i)
        {
            try
            {
                switch (i)
                {
                    case 1:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_01.gif"));
                        break;
                    case 2:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_02.gif"));
                        break;
                    case 3:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_03.gif"));
                        break;
                    case 4:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_04.gif"));
                        break;
                    case 5:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_05.gif"));
                        break;
                    case 6:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_06.gif"));
                        break;
                    case 7:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_07.gif"));
                        break;
                    case 8:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_08.gif"));
                        break;
                }
                player.Fill = playerSprite;
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden in de Runsprite.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Space && jumping == false && Canvas.GetTop(player) > 260)
                {
                    jumping = true;
                    force = 15;
                    speed = -12;

                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ImagesRun/newRunner_02.gif"));
                    Muziek.Jumping();
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij springen.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Credits.EndlessRunnerCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.EndlessRunnerCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);

                    if (gameOver == true)
                    {
                        GameOverMenu.Visibility = Visibility.Hidden;
                        StartGame();
                    }
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van de game.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
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

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
