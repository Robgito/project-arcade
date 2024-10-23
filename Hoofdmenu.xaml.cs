using loginscreen_games;
using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for Hoofdmenu.xaml
    /// </summary>
    public partial class Hoofdmenu : Window
    {
        private double gradientOffset = 0;
        private DispatcherTimer timer;
        private DispatcherTimer timerWheelOfFortune;
        private bool imgRadEnabled = true;
        private DateTime startTijd;
        Cooldown eenCooldown;
        private int tellerMuziek = 0;

        //test comment 
        public Hoofdmenu()
        {
            InitializeComponent();

            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Timer_Tick; ;
            timer.Start();

            timerWheelOfFortune = new DispatcherTimer(DispatcherPriority.Render);                //BART AANGEPAST
            timerWheelOfFortune.Interval = new TimeSpan(0, 0, 1);
            timerWheelOfFortune.Tick += TimerWheelOfFortune_Tick; ;
            timerWheelOfFortune.Start();
        }

        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        private void TimerWheelOfFortune_Tick(object sender, EventArgs e)
        {
            TimeSpan verlopenTijd = DateTime.Now - startTijd;
            TimeSpan resterendeTijd = new TimeSpan(1, 0, 0) - verlopenTijd;  //Hier tijd aanpassen om te testen! moet new TimeSpan(1,0,0) zijn

            if (resterendeTijd.TotalSeconds <= 0)
            {
                imgRadEnabled = true;
                btnRad.IsEnabled = true;
                timerWheelOfFortune.Stop();
                lblCooldownWheel.Content = string.Empty;
                btnRad.Opacity = 1;
            }
            else
            {
                lblCooldownWheel.Content = "Cooldown: " + resterendeTijd.ToString(@"hh\:mm\:ss");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gradientOffset += 0.02;
            if (gradientOffset > 1)
                gradientOffset = 0;

            DoubleAnimation animation1 = new DoubleAnimation
            {
                To = gradientOffset,
                Duration = TimeSpan.FromMilliseconds(500),
                EasingFunction = new QuadraticEase()
            };

            DoubleAnimation animation2 = new DoubleAnimation
            {
                To = gradientOffset,
                Duration = TimeSpan.FromMilliseconds(500),
                EasingFunction = new QuadraticEase()
            };

            BorderGradient.GradientStops[0].BeginAnimation(GradientStop.OffsetProperty, animation1);
            BorderGradient.GradientStops[1].BeginAnimation(GradientStop.OffsetProperty, animation2);
        }

        private void btnUitloggen_Click(object sender, RoutedEventArgs e)
        {
            //Coin geluidje
            Muziek.CoinGeluid();

            CooldownOpslaan();

            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            //Coin geluidje
            Muziek.CoinGeluid();

            BlurAchtergrond(20);

            Admin admin = new Admin();
            admin.IngelogdeGebruiker = this.IngelogdeGebruiker;
            admin.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void btnBeheerder_Click(object sender, RoutedEventArgs e)
        {
            //Coin geluidje
            Muziek.CoinGeluid();

            BlurAchtergrond(20);

            Beheerder beheerder = new Beheerder();
            beheerder.IngelogdeGebruiker = this.IngelogdeGebruiker;
            beheerder.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void btnGebruikerAanpassen_Click(object sender, RoutedEventArgs e)
        {
            //Coin geluidje
            Muziek.CoinGeluid();

            CreditStore store = new CreditStore();

            BlurAchtergrond(20);

            AanpassingGebruiker aanpassingGebruiker = new AanpassingGebruiker();
            aanpassingGebruiker.IngelogdeGebruiker = this.IngelogdeGebruiker;
            aanpassingGebruiker.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void btnStore_Click(object sender, RoutedEventArgs e)
        {
            //Coin geluidje
            Muziek.CoinGeluid();

            CreditStore store = new CreditStore();

            BlurAchtergrond(20);

            store.IngelogdeGebruiker = this.IngelogdeGebruiker;
            store.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void btnContactSupport_Click(object sender, RoutedEventArgs e)
        {
            //Coin geluidje
            Muziek.CoinGeluid();

            ContactSupport support = new ContactSupport();

            BlurAchtergrond(20);

            support.IngelogdeGebruiker = this.IngelogdeGebruiker;
            support.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CooldownOpslaan();

            Environment.Exit(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RolCheck();

            //Music theme
            string path = Environment.CurrentDirectory;
            SoundPlayer player = new SoundPlayer(path + "\\Sounds\\arcadeIntroMusic.wav");
            player.Play();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));

            imgAvatar.Source = new BitmapImage(new Uri(IngelogdeGebruiker.Avatar, UriKind.Relative));
            lblWelkom.Content = "WELKOM " + IngelogdeGebruiker.Gebruikersnaam.ToUpper() + "!" + Environment.NewLine + "CREDITS: " + IngelogdeGebruiker.Credits;

            //Check of er cooldown is voor wheel of fortune:
            eenCooldown = new Cooldown();
            eenCooldown = Cooldown.GetDatum(IngelogdeGebruiker.UserID);
            if (eenCooldown != null)
            {
                startTijd = eenCooldown.StartDatum;
                imgRadEnabled = false;
                btnRad.IsEnabled = false;
                btnRad.Opacity = 0.4;
                timerWheelOfFortune.Start();
            }
        }

        private void RolCheck()
        {
            btnAdmin.IsEnabled = false;
            btnAdmin.Visibility = Visibility.Hidden;
            btnBeheerder.IsEnabled = false;
            btnBeheerder.Visibility = Visibility.Hidden;

            if (IngelogdeGebruiker != null)
            {
                if (IngelogdeGebruiker.Admin == true)
                {
                    btnAdmin.IsEnabled = true;
                    btnAdmin.Visibility = Visibility.Visible;
                }
                if (IngelogdeGebruiker.Beheerder == true)
                {
                    btnBeheerder.IsEnabled = true;
                    btnBeheerder.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnRad_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (imgRadEnabled)
            {
                //Coin geluidje
                Muziek.CoinGeluid();

                BlurAchtergrond(20);

                WheelOfFortune wheelOfFortune = new WheelOfFortune();
                wheelOfFortune.IngelogdeGebruiker = this.IngelogdeGebruiker;
                wheelOfFortune.Closed += WheelOfFortune_Closed;
                wheelOfFortune.ShowDialog();

                //Intro muziek
                Muziek.MusicThemeIntro();
                imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
                tellerMuziek = 0;
            }
        }

        private void WheelOfFortune_Closed(object sender, EventArgs e)
        {
            imgRadEnabled = false;
            btnRad.IsEnabled = false;
            startTijd = DateTime.Now;
            btnRad.Opacity = 0.4;
            timerWheelOfFortune.Start();
            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void btnGame1_Click(object sender, RoutedEventArgs e)
        {
            Muziek.CoinGeluid();

            BlurAchtergrond(20);

            SpeelScherm tetris = new SpeelScherm();
            tetris.IngelogdeGebruiker = this.IngelogdeGebruiker;
            tetris.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void BlurAchtergrond(int radius)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = radius;

            GridHoofdmenu.Effect = blur;
        }

        private void btnGame2_Click(object sender, RoutedEventArgs e)
        {
            Muziek.CoinGeluid();

            BlurAchtergrond(20);

            SpeelSnake snake = new SpeelSnake();
            snake.IngelogdeGebruiker = this.IngelogdeGebruiker;
            snake.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Muziek.CoinGeluid();

            BlurAchtergrond(20);

            Gokautomaat gokautomaat = new Gokautomaat();
            gokautomaat.IngelogdeGebruiker = this.IngelogdeGebruiker;
            gokautomaat.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void RefreshLabelCredits()
        {
            IngelogdeGebruiker = Datamanager.GetGebruiker(IngelogdeGebruiker.UserID);
            lblWelkom.Content = "";
            imgAvatar.Source = new BitmapImage(new Uri(IngelogdeGebruiker.Avatar, UriKind.Relative));
            lblWelkom.Content = "WELKOM " + IngelogdeGebruiker.Gebruikersnaam.ToUpper() + "!" + Environment.NewLine + "CREDITS: " + IngelogdeGebruiker.Credits;
        }

        private void btnGame5_Click(object sender, RoutedEventArgs e)
        {
            //Coin geluidje
            Muziek.CoinGeluid();

            BlurAchtergrond(20);

            SpeelRunner speelRunner = new SpeelRunner();
            speelRunner.IngelogdeGebruiker = this.IngelogdeGebruiker;
            speelRunner.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void btnGame4_Click(object sender, RoutedEventArgs e)
        {
            Muziek.CoinGeluid();

            BlurAchtergrond(20);
            SpeelFlappyBird speelFlappyBird = new SpeelFlappyBird();
            speelFlappyBird.IngelogdeGebruiker = this.IngelogdeGebruiker;
            speelFlappyBird.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }

        private void btnGame3_Click(object sender, RoutedEventArgs e)
        {
            Muziek.CoinGeluid();

            BlurAchtergrond(20);

            SpeelZombieShooter speelZombieShooter = new SpeelZombieShooter();
            speelZombieShooter.IngelogdeGebruiker = this.IngelogdeGebruiker;
            speelZombieShooter.ShowDialog();

            //Intro muziek
            Muziek.MusicThemeIntro();
            imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
            tellerMuziek = 0;

            RefreshLabelCredits();
            BlurAchtergrond(0);
        }
        private void CooldownOpslaan()
        {
            if (startTijd != null)
            {
                bool checkCooldownBestaat = false;

                if (System.IO.File.Exists("Cooldowns.json"))
                {
                    List<Cooldown> lijstCooldowns = Cooldown.GetDatums();
                    foreach (Cooldown cooldown in lijstCooldowns)
                    {
                        if (cooldown.IDGebruiker == IngelogdeGebruiker.UserID)
                        {
                            checkCooldownBestaat = true;
                            break;
                        }
                    }
                    if (checkCooldownBestaat)
                    {
                        eenCooldown.StartDatum = startTijd;
                        Cooldown.UpdateDatum(eenCooldown);
                    }
                    else
                    {
                        eenCooldown = new Cooldown(IngelogdeGebruiker.UserID, startTijd);
                        Cooldown.InsertDatum(eenCooldown);
                    }
                }
                else
                {
                    eenCooldown = new Cooldown(IngelogdeGebruiker.UserID, startTijd);
                    Cooldown.InsertDatum(eenCooldown);
                }
            }
        }

        private void imgMute_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Music theme
                string path = Environment.CurrentDirectory;
                SoundPlayer player = new SoundPlayer(path + "\\Sounds\\arcadeIntroMusic.wav");

                if (tellerMuziek == 0)
                {
                    player.Stop();
                    imgMute.Source = new BitmapImage(new Uri("\\Images\\silent.png", UriKind.Relative));
                    tellerMuziek++;
                }
                else
                {
                    player.Play();
                    imgMute.Source = new BitmapImage(new Uri("\\Images\\sound.png", UriKind.Relative));
                    tellerMuziek--;
                }
            }
            catch
            {
                MessageBox.Show("Er ging iets mis bij het laden van het geluid.");
            }
        }
    }
}
