using Project_3___Arcade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Zombie_shooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ZombieShooterGame : Window
    {
        DispatcherTimer timer;

        public ZombieShooterGame()
        {
            InitializeComponent();
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer.Tick += Timer_Tick;
        }

        bool goLeft = false, goRight = false, goUp = false, goDown = false, gameOver = false, hansMode = false;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        int kills;
        Random random = new Random();

        List<Image> zombiesList = new List<Image>();

        MessageBoxResult antwoordMessageBox;
        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                hpBar.Value = playerHealth;
                stckHansMode.Visibility = Visibility.Hidden;
                cbHansMode.IsEnabled = false;
                btnPlay.IsEnabled = false;
                btnTerugMain.IsEnabled = false;
                btnPlay.Visibility = Visibility.Hidden;
                btnTerugMain.Visibility = Visibility.Hidden;
                btnPlayAgain.IsEnabled = false;
                btnTerug.IsEnabled = false;
                btnPlayAgain.Visibility = Visibility.Hidden;
                btnTerug.Visibility = Visibility.Hidden;
            }
            else
            {
                gameOver = true;
                imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\dead.png", UriKind.Relative));
                timer.Stop();
                BlurAchtergrond(5);
                stckHansMode.Visibility = Visibility.Visible;
                cbHansMode.IsEnabled = true;
                btnPlayAgain.IsEnabled = true;
                btnTerug.IsEnabled = true;
                btnPlayAgain.Visibility = Visibility.Visible;
                btnTerug.Visibility = Visibility.Visible;
                //score updaten
                ScoreZombieShooter huidigeScore = Datamanager.GetScoreZombieShooterGebruiker(IngelogdeGebruiker.UserID);
                if (huidigeScore != null)
                {
                    if (huidigeScore.Score < kills)
                    {
                        huidigeScore.Score = Convert.ToInt32(kills);
                        Datamanager.UpdateScoreZombieShooter(huidigeScore);
                    }
                }
                else
                {
                    ScoreZombieShooter zombieShooterScore = new ScoreZombieShooter();

                    int nieuweScore = Convert.ToInt32(kills);

                    zombieShooterScore.Score = nieuweScore;

                    zombieShooterScore.FKGebruiker = IngelogdeGebruiker.UserID;
                    Datamanager.InsertScoreZombieShooter(zombieShooterScore);
                }
            }

            lblAmmo.Content = "Ammo: " + ammo;
            lblKills.Content = "Kills: " + kills;
            //player movement
            if (goLeft == true && Canvas.GetLeft(imgPlayer) > 0)
            {
                Canvas.SetLeft(imgPlayer, Canvas.GetLeft(imgPlayer) - speed);
            }
            if (goRight == true && Canvas.GetLeft(imgPlayer) + imgPlayer.Width < canvas.Width)
            {
                Canvas.SetLeft(imgPlayer, Canvas.GetLeft(imgPlayer) + speed);
            }
            if (goUp == true && Canvas.GetTop(imgPlayer) > 0)
            {
                Canvas.SetTop(imgPlayer, Canvas.GetTop(imgPlayer) - speed);
            }
            if (goDown == true && Canvas.GetTop(imgPlayer) + imgPlayer.Height < canvas.Height)
            {
                Canvas.SetTop(imgPlayer, Canvas.GetTop(imgPlayer) + speed);
            }


            List<UIElement> itemsToRemove = new List<UIElement>();
            foreach (var x in canvas.Children.OfType<Image>())
            {
                //ammo drops
                if ((string)x.Tag == "ammo")
                {
                    Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);
                    Rect ammoHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitbox.IntersectsWith(ammoHitbox))
                    {
                        itemsToRemove.Add(x);
                        ammo += 5;
                    }
                }
                //zombie naar player bewegen
                if ((string)x.Tag == "zombie")
                {
                    if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                        x.Source = new BitmapImage(new Uri(@"ZombieImages\zleft.png", UriKind.Relative));
                    }
                    if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                        x.Source = new BitmapImage(new Uri(@"ZombieImages\zright.png", UriKind.Relative));
                    }
                    if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                        x.Source = new BitmapImage(new Uri(@"ZombieImages\zup.png", UriKind.Relative));
                    }
                    if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                        x.Source = new BitmapImage(new Uri(@"ZombieImages\zdown.png", UriKind.Relative));
                    }

                    //wanneer player zombie hit
                    Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

                    if (playerHitbox.IntersectsWith(zombieHitbox))
                    {
                        playerHealth -= 1;
                    }
                }
                if ((string)x.Tag == "Bernd")
                {
                    MoveZombieBernd(x);
                }
                if ((string)x.Tag == "Bart")
                {
                    MoveZombieBart(x);
                }
                if ((string)x.Tag == "Tycho")
                {
                    MoveZombieTycho(x);
                }
                if ((string)x.Tag == "Abdul")
                {
                    MoveZombieAbdul(x);
                }
                if ((string)x.Tag == "Robin")
                {
                    MoveZombieRobin(x);
                }
                if ((string)x.Tag == "Illya")
                {
                    MoveZombieIllya(x);
                }
                if ((string)x.Tag == "Cedric")
                {
                    MoveZombieCedric(x);
                }
                if ((string)x.Tag == "Gilles")
                {
                    MoveZombieGilles(x);
                }
                if ((string)x.Tag == "Jeffrey")
                {
                    MoveZombieJeffrey(x);
                }

                foreach (Ellipse a in canvas.Children.OfType<Ellipse>())
                {
                    if ((string)x.Tag == "zombie")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Bernd")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Bart")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Tycho")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Abdul")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Robin")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Illya")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Cedric")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Gilles")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                    if ((string)x.Tag == "Jeffrey")
                    {
                        Rect bulletHitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Width, a.Height);
                        Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (bulletHitbox.IntersectsWith(zombieHitbox))
                        {
                            kills++;
                            itemsToRemove.Add(x);
                            itemsToRemove.Add(a);
                            zombiesList.Remove(x);
                        }
                    }
                }
            }

            foreach (UIElement element in itemsToRemove)
            {
                canvas.Children.Remove(element);
            }
            if (zombiesList.Count < 3)
            {
                if (hansMode)
                {
                    MaakRandomZombie();
                }
                else
                {
                    MakeZombies();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameOver == true)
            {
                return;
            }

            if (hansMode)
            {
                if (e.Key == Key.Left)
                {
                    goLeft = true;
                    facing = "left";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\leftHans.png", UriKind.Relative));
                }

                if (e.Key == Key.Right)
                {
                    goRight = true;
                    facing = "right";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\rightHans.png", UriKind.Relative));
                }

                if (e.Key == Key.Up)
                {
                    goUp = true;
                    facing = "up";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\upHans.png", UriKind.Relative));
                }

                if (e.Key == Key.Down)
                {
                    goDown = true;
                    facing = "down";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\downHans.png", UriKind.Relative));
                }
            }
            else
            {
                if (e.Key == Key.Left)
                {
                    goLeft = true;
                    facing = "left";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\left.png", UriKind.Relative));
                }

                if (e.Key == Key.Right)
                {
                    goRight = true;
                    facing = "right";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\right.png", UriKind.Relative));
                }

                if (e.Key == Key.Up)
                {
                    goUp = true;
                    facing = "up";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\up.png", UriKind.Relative));
                }

                if (e.Key == Key.Down)
                {
                    goDown = true;
                    facing = "down";
                    imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\down.png", UriKind.Relative));
                }
            }
            
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }

            if (e.Key == Key.Right)
            {
                goRight = false;
            }

            if (e.Key == Key.Up)
            {
                goUp = false;
            }

            if (e.Key == Key.Down)
            {
                goDown = false;
            }

            if (e.Key == Key.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                ShootBullet(facing);

                if (ammo < 1)
                {
                    DropAmmo();
                }
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = Canvas.GetLeft(imgPlayer) + (imgPlayer.Width / 2);
            shootBullet.bulletTop = Canvas.GetTop(imgPlayer) + (imgPlayer.Height / 2);
            shootBullet.MakeBullet(canvas);
        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Credits.ZombieShooterCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.ZombieShooterCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                    if (cbHansMode.IsChecked == true)
                    {
                        hansMode = true;
                    }
                    else
                    {
                        hansMode = false;
                    }
                    RestartGame();
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van Zombieshooter.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MakeZombies()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdown.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "zombie";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void DropAmmo()
        {
            Image ammo = new Image();
            ammo.Width = 50;
            ammo.Height = 50;
            BitmapImage ammoBitmap = new BitmapImage(new Uri(@"ZombieImages\ammo-image.png", UriKind.RelativeOrAbsolute));
            ammo.Source = ammoBitmap;
            ammo.Tag = "ammo";
            Canvas.SetLeft(ammo, random.Next(10, Convert.ToInt32(canvas.Width - ammo.Width)));
            Canvas.SetTop(ammo, random.Next(0, Convert.ToInt32(canvas.Height - ammo.Height)));
            canvas.Children.Add(ammo);
            Canvas.SetZIndex(ammo, 1);
        }


        private void RestartGame()
        {
            imgPlayer.Source = new BitmapImage(new Uri(@"ZombieImages\up.png", UriKind.Relative));
            List<UIElement> zombiesToRemove = new List<UIElement>();

            foreach (Image zombie in zombiesList)
            {
                zombiesToRemove.Add(zombie);
            }
            foreach (UIElement zombie in zombiesToRemove)
            {
                canvas.Children.Remove(zombie);
            }
            zombiesList.Clear();

            for (int i = 0; i < 3; i++)
            {
                if (hansMode)
                {
                    MaakRandomZombie();
                }
                else
                {
                    MakeZombies();
                }
            }

            goUp = false;
            goLeft = false;
            goDown = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            kills = 0;
            ammo = 10;

            BlurAchtergrond(0);

            timer.Start();
        }

        private void BlurAchtergrond(int i)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = i;

            canvas.Effect = blur;
        }

        private void MaakZombieBernd()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownBernd.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Bernd";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieBart()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownBart.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Bart";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieAbdul()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownAbdul.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Abdul";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieJeffrey()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownJeffrey.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Jeffrey";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieIllya()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownIllya.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Illya";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieTycho()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownTycho.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Tycho";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieGilles()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownGilles.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Gilles";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieRobin()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownRobin.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Robin";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakZombieCedric()
        {
            Image zombie = new Image();
            zombie.Width = imgPlayer.Width;
            zombie.Height = imgPlayer.Height;
            BitmapImage zombieBitmap = new BitmapImage(new Uri(@"ZombieImages\zdownCedric.png", UriKind.RelativeOrAbsolute));
            zombie.Source = zombieBitmap;
            zombie.Tag = "Cedric";
            Canvas.SetLeft(zombie, random.Next(0, 900));
            Canvas.SetTop(zombie, random.Next(0, 800));
            canvas.Children.Add(zombie);
            zombiesList.Add(zombie);
            Canvas.SetZIndex(imgPlayer, 1);
        }

        private void MaakRandomZombie()
        {
            Random randomZombie = new Random();
            int randomZombieMaken = random.Next(1, 10);

            switch (randomZombieMaken)
            {
                case 1:
                    MaakZombieAbdul();
                    break;
                case 2:
                    MaakZombieBart();
                    break;
                case 3:
                    MaakZombieBernd();
                    break;
                case 4:
                    MaakZombieCedric();
                    break;
                case 5:
                    MaakZombieGilles();
                    break;
                case 6:
                    MaakZombieIllya();
                    break;
                case 7:
                    MaakZombieJeffrey();
                    break;
                case 8:
                    MaakZombieRobin();
                    break;
                case 9:
                    MaakZombieTycho();
                    break;
            }
        }

        private void MoveZombieBernd(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftBernd.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightBernd.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupBernd.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownBernd.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void MoveZombieBart(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftBart.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightBart.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupBart.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownBart.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void MoveZombieAbdul(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftAbdul.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightAbdul.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupAbdul.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownAbdul.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void MoveZombieJeffrey(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftJeffrey.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightJeffrey.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupJeffrey.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownJeffrey.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void MoveZombieIllya(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftIllya.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightIllya.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupIllya.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownIllya.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BlurAchtergrond(5);
            btnPlayAgain.IsEnabled = false;
            btnTerug.IsEnabled = false;
            btnPlay.IsEnabled = true;
            btnTerugMain.IsEnabled = true;
            btnPlay.Visibility = Visibility.Visible;
            btnTerugMain.Visibility = Visibility.Visible;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Credits.ZombieShooterCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.ZombieShooterCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                    if (cbHansMode.IsChecked == true)
                    {
                        hansMode = true;
                    }
                    else
                    {
                        hansMode = false;
                    }
                    RestartGame();
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
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

        private void btnTerugMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MoveZombieTycho(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftTycho.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightTycho.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupTycho.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownTycho.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void MoveZombieGilles(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftGilles.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightGilles.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupGilles.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownGilles.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void MoveZombieRobin(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftRobin.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightRobin.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupRobin.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownRobin.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }

        private void MoveZombieCedric(Image x)
        {
            if (Canvas.GetLeft(x) > Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zleftCedric.png", UriKind.Relative));
            }
            if (Canvas.GetLeft(x) < Canvas.GetLeft(imgPlayer))
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zrightCedric.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) > Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zupCedric.png", UriKind.Relative));
            }
            if (Canvas.GetTop(x) < Canvas.GetTop(imgPlayer))
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + zombieSpeed);
                x.Source = new BitmapImage(new Uri(@"ZombieImages\zdownCedric.png", UriKind.Relative));
            }

            //wanneer player zombie hit
            Rect zombieHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            Rect playerHitbox = new Rect(Canvas.GetLeft(imgPlayer), Canvas.GetTop(imgPlayer), imgPlayer.Width, imgPlayer.Height);

            if (playerHitbox.IntersectsWith(zombieHitbox))
            {
                playerHealth -= 1;
            }
        }
    }
}
