using System;
using System.Data.SqlClient;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for Gokautomaat.xaml
    /// </summary>
    public partial class Gokautomaat : Window
    {
        private Random random = new Random();
        private readonly string[] arrAfbeeldingen = { "bell.png", "cherrys.png", "orange.png", "diamond.png", "cherrys.png", "orange.png", "bell.png", "bell.png", "cherrys.png", "diamond.png", "orange.png", "seven.png", "cherrys.png" };

        private DispatcherTimer timerAfbeelding1;
        private DispatcherTimer timerAfbeelding2;
        private DispatcherTimer timerAfbeelding3;
        private DispatcherTimer timerHeader;
        private DispatcherTimer hideGifTimer;

        MessageBoxResult antwoordMessageBox;

        private int aantalRotaties;
        private int aantalPulls;
        private int randomPullGetal;
        private int ingezetteCredits;
        private int gewonnenCredits;

        Random mijnRandom = new Random();

        private Gebruiker _ingelogdeGebruiker;

        MediaPlayer leverSound = new MediaPlayer();
        MediaPlayer wheelSound = new MediaPlayer();
        SoundPlayer jackpotSound = new SoundPlayer(Environment.CurrentDirectory + "\\SoundsSlotMachine\\slotMachineWin.wav");
        SoundPlayer smallWinSound = new SoundPlayer(Environment.CurrentDirectory + "\\SoundsSlotMachine\\smallWinNotification.wav");
        SoundPlayer loseGame = new SoundPlayer(Environment.CurrentDirectory + "\\SoundsSlotMachine\\loseCoins.wav");

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        public Gokautomaat()
        {
            InitializeComponent();
            InitializeTimers();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            aantalPulls = 0;
            RefreshCreditsLabel();

            //Om gebruikers random eens te laten winnen:
            randomPullGetal = mijnRandom.Next(4, 9);
        }

        private void btnPull_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ingezetteCredits = Convert.ToInt32(nudCredits.Value);

                if (ingezetteCredits <= IngelogdeGebruiker.Credits)
                {
                    aantalPulls++;

                    IngelogdeGebruiker.Credits -= ingezetteCredits;
                    RefreshCreditsLabel();

                    btnPull.IsEnabled = false;
                    lblResultaat.Content = string.Empty;
                    lblHeader.Content = "ZAL JE WINNEN?";

                    aantalRotaties = 0;
                    timerAfbeelding1.Start();
                    timerAfbeelding2.Start();
                    timerAfbeelding3.Start();
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeTimers()
        {
            try
            {
                timerAfbeelding1 = new DispatcherTimer();
                timerAfbeelding1.Interval = TimeSpan.FromSeconds(0.1);
                timerAfbeelding1.Tick += TimerAfbeedling1_Tick;

                timerAfbeelding2 = new DispatcherTimer();
                timerAfbeelding2.Interval = TimeSpan.FromSeconds(0.1);
                timerAfbeelding2.Tick += TimerAfbeelding2_Tick;

                timerAfbeelding3 = new DispatcherTimer();
                timerAfbeelding3.Interval = TimeSpan.FromSeconds(0.1);
                timerAfbeelding3.Tick += TimerAfbeelding3_Tick;

                timerHeader = new DispatcherTimer();
                timerHeader.Interval = TimeSpan.FromSeconds(4);
                timerHeader.Tick += TimerHeader_Tick;

                hideGifTimer = new DispatcherTimer();
                hideGifTimer.Interval = TimeSpan.FromSeconds(4);
                hideGifTimer.Tick += HideGifTimer_Tick;
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het initialiseren van de timer.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TimerAfbeedling1_Tick(object sender, EventArgs e)
        {
            try
            {
                SpinReel(img1);
                aantalRotaties++;

                if (aantalRotaties == 1)
                {
                    //Sounds slotmachine:
                    leverSound.Open(new Uri(Environment.CurrentDirectory + "\\SoundsSlotMachine\\machinePull.wav"));
                    leverSound.Play();
                    wheelSound.Open(new Uri(Environment.CurrentDirectory + "\\SoundsSlotMachine\\SpinningWheel.wav"));
                    wheelSound.Play();
                }
                if (aantalRotaties >= 40)
                {
                    timerAfbeelding1.Stop();
                    if (aantalPulls == randomPullGetal || aantalPulls % 13 == 0)
                    {
                        string imagePath = $"Images/cherrys.png";
                        BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        img1.Source = bitmapImage;
                    }

                    ////DIT IS ALLEEN OM TE TESTEN !!!!!!
                    //if (aantalPulls == 1)
                    //{
                    //    string imagePath = $"Images/seven.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img1.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 2)
                    //{
                    //    string imagePath = $"Images/bell.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img1.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 3)
                    //{
                    //    string imagePath = $"Images/diamond.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img1.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 4)
                    //{
                    //    string imagePath = $"Images/orange.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img1.Source = bitmapImage;
                    //}
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van afbeelding1.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TimerAfbeelding2_Tick(object sender, EventArgs e)
        {
            try
            {
                SpinReel(img2);
                aantalRotaties++;

                if (aantalRotaties >= 60)
                {
                    timerAfbeelding2.Stop();
                    if (aantalPulls == randomPullGetal || aantalPulls % 13 == 0)
                    {
                        string imagePath = $"Images/cherrys.png";
                        BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        img2.Source = bitmapImage;
                    }

                    ////DIT IS ALLEEN OM TE TESTEN !!!!!!
                    //if (aantalPulls == 1)
                    //{
                    //    string imagePath = $"Images/seven.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img2.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 2)
                    //{
                    //    string imagePath = $"Images/bell.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img2.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 3)
                    //{
                    //    string imagePath = $"Images/diamond.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img2.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 4)
                    //{
                    //    string imagePath = $"Images/orange.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img2.Source = bitmapImage;
                    //}
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van afbeelding2.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TimerAfbeelding3_Tick(object sender, EventArgs e)
        {
            try
            {
                SpinReel(img3);
                aantalRotaties++;

                if (aantalRotaties >= 80)
                {
                    timerAfbeelding3.Stop();
                    leverSound.Stop();
                    wheelSound.Stop();

                    if (aantalPulls == randomPullGetal || aantalPulls % 13 == 0)
                    {
                        string imagePath = $"Images/cherrys.png";
                        BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                        img3.Source = bitmapImage;
                    }

                    ////DIT IS ALLEEN OM TE TESTEN !!!!!!
                    //if (aantalPulls == 1)
                    //{
                    //    string imagePath = $"Images/seven.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img3.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 2)
                    //{
                    //    string imagePath = $"Images/bell.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img3.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 3)
                    //{
                    //    string imagePath = $"Images/diamond.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img3.Source = bitmapImage;
                    //}
                    //if (aantalPulls == 4)
                    //{
                    //    string imagePath = $"Images/orange.png";
                    //    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    //    img3.Source = bitmapImage;
                    //}

                    //DIT MOET WEL BLIJVEN STAAN !!!
                    CheckGewonnen();
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van afbeelding3.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TimerHeader_Tick(object sender, EventArgs e)
        {
            try
            {
                timerHeader.Stop();
                HeaderResetten();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het stoppen van de header timer.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HideGifTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                hideGifTimer.Stop();

                //Gifs stoppen:
                gifWinnaar.Visibility = Visibility.Collapsed;
                gifCoinRegen.Visibility = Visibility.Collapsed;
                gifJackpot.Visibility = Visibility.Collapsed;

                //Sound stoppen:
                jackpotSound.Stop();
                smallWinSound.Stop();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het stoppen van de gif timer.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SpinReel(Image afbeelding)
        {
            try
            {
                int randomIndex = random.Next(arrAfbeeldingen.Length);
                string imagePath = $"Images/{arrAfbeeldingen[randomIndex]}";
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                afbeelding.Source = bitmapImage;
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden in de spinreel.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckGewonnen()
        {
            try
            {
                gewonnenCredits = 0;

                if (img1.Source != null && img2.Source != null && img3.Source != null)
                {
                    string image1 = ((BitmapImage)img1.Source).UriSource.ToString();
                    string image2 = ((BitmapImage)img2.Source).UriSource.ToString();
                    string image3 = ((BitmapImage)img3.Source).UriSource.ToString();

                    if (image1 == image2 && image2 == image3)
                    {
                        if (image1 == "Images/cherrys.png")
                        {
                            //Gif starten:
                            gifWinnaar.Visibility = Visibility.Visible;
                            hideGifTimer.Start();

                            //Header aanpassen:
                            lblHeader.Foreground = Brushes.Red;
                            lblHeader.Content = "*** WINNAAR ***";
                            lblResultaat.Content = "Proficiat! inzet x2!";

                            //Credits bijtellen + label aanpassen:
                            gewonnenCredits = ingezetteCredits * 2;
                            IngelogdeGebruiker.Credits += gewonnenCredits;
                            RefreshCreditsLabel();

                            //Animatie + sound:
                            smallWinSound.Play();
                            AllImagesBlink();
                            BlinkingBorder(BorderForFlicker, 300, 10.00);
                            timerHeader.Start();
                        }
                        else if (image1 == "Images/orange.png")
                        {
                            //Gif starten:
                            gifWinnaar.Visibility = Visibility.Visible;
                            hideGifTimer.Start();

                            //Header aanpassen:
                            lblHeader.Foreground = Brushes.Red;
                            lblHeader.Content = "*** WINNAAR ***";
                            lblResultaat.Content = "Proficiat! inzet x3!";

                            //Credits bijtellen + label aanpassen:
                            gewonnenCredits = ingezetteCredits * 3;
                            IngelogdeGebruiker.Credits += gewonnenCredits;
                            RefreshCreditsLabel();

                            //Animatie + sound:
                            smallWinSound.Play();
                            AllImagesBlink();
                            BlinkingBorder(BorderForFlicker, 300, 10.00);
                            timerHeader.Start();
                        }
                        else if (image1 == "Images/bell.png")
                        {
                            //Gif starten:
                            gifWinnaar.Visibility = Visibility.Visible;
                            hideGifTimer.Start();

                            //Header aanpassen:
                            lblHeader.Foreground = Brushes.Red;
                            lblHeader.Content = "*** WINNAAR ***";
                            lblResultaat.Content = "Proficiat! Inzet x4!";

                            //Credits bijtellen + label aanpassen:
                            gewonnenCredits = ingezetteCredits * 4;
                            IngelogdeGebruiker.Credits += gewonnenCredits;
                            RefreshCreditsLabel();

                            //Animatie + sound:
                            smallWinSound.Play();
                            AllImagesBlink();
                            BlinkingBorder(BorderForFlicker, 300, 10.00);
                            timerHeader.Start();
                        }
                        else if (image1 == "Images/diamond.png")
                        {
                            //Gif starten:
                            gifWinnaar.Visibility = Visibility.Visible;
                            hideGifTimer.Start();

                            //Header aanpassen:
                            lblHeader.Foreground = Brushes.Red;
                            lblHeader.Content = "*** WINNAAR ***";
                            lblResultaat.Content = "*** PROFICIAT! INZET x8! ***";

                            //Credits bijtellen + label aanpassen:
                            gewonnenCredits = ingezetteCredits * 8;
                            IngelogdeGebruiker.Credits += gewonnenCredits;
                            RefreshCreditsLabel();

                            //Animatie + sound:
                            smallWinSound.Play();
                            AllImagesBlink();
                            BlinkingBorder(BorderForFlicker, 300, 10.00);
                            timerHeader.Start();
                        }
                        else if (image1 == "Images/seven.png")
                        {
                            //Gif starten:
                            gifCoinRegen.Visibility = Visibility.Visible;
                            gifJackpot.Visibility= Visibility.Visible;
                            hideGifTimer.Start();

                            //Header aanpassen:
                            lblHeader.Foreground = Brushes.Red;
                            lblHeader.Content = string.Empty;
                            lblResultaat.Content = "*** JACKPOT! INZET x10! ***";

                            //Credits bijtellen + label aanpassen:
                            gewonnenCredits = ingezetteCredits * 15;
                            IngelogdeGebruiker.Credits += gewonnenCredits;
                            RefreshCreditsLabel();

                            //Animatie + sound:
                            jackpotSound.Play();
                            AllImagesBlink();
                            BlinkingBorder(BorderForFlicker, 300, 10.00);
                            timerHeader.Start();
                        }
                    }
                    else
                    {
                        loseGame.Play();
                        lblResultaat.Content = "Jammer! Volgende keer meer geluk!";
                        HeaderResetten();
                    }
                }
                //Gebruiker updaten:
                Datamanager.UpdateGebruiker(IngelogdeGebruiker);
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden in de checkgewonnen methode.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void nudCredits_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(nudCredits.Value) <= 0)
                {
                    nudCredits.Value = 1;
                    lblResultaat.Content = "Minimum 1 credit inzetten.";
                }
                else if (Convert.ToInt32(nudCredits.Value) > 100)
                {
                    nudCredits.Value = 100;
                    lblResultaat.Content = "Maximum 100 credits inzetten.";
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Er is een fout opgetreden bij het laden van het hoofdmenu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BlinkingImage(Image afbeelding, int lengte, double repetitie)
        {
            try
            {
                DoubleAnimation dezeDoubleAnimatie = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(lengte)),
                    AutoReverse = true,
                    RepeatBehavior = new RepeatBehavior(repetitie)
                };
                Storyboard ditStoryboard = new Storyboard();
                ditStoryboard.Children.Add(dezeDoubleAnimatie);
                Storyboard.SetTarget(dezeDoubleAnimatie, afbeelding);
                Storyboard.SetTargetProperty(dezeDoubleAnimatie, new PropertyPath("Opacity"));
                ditStoryboard.Begin(afbeelding);
            }
            catch
            {
                MessageBox.Show("BlinkingImage werkt niet.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BlinkingBorder(Border eenBorder, int lengte, double repetitie)
        {
            try
            {
                DoubleAnimation dezeDoubleAnimatie = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(lengte)),
                    AutoReverse = true,
                    RepeatBehavior = new RepeatBehavior(repetitie)
                };
                Storyboard ditStoryboard = new Storyboard();
                ditStoryboard.Children.Add(dezeDoubleAnimatie);
                Storyboard.SetTarget(dezeDoubleAnimatie, eenBorder);
                Storyboard.SetTargetProperty(dezeDoubleAnimatie, new PropertyPath("Opacity"));
                ditStoryboard.Begin(eenBorder);
            }
            catch
            {
                MessageBox.Show("BlinkingBorder werkt niet.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AllImagesBlink()
        {
            BlinkingImage(img1, 300, 6);
            BlinkingImage(img2, 300, 6);
            BlinkingImage(img3, 300, 6);
        }

        public void HeaderResetten()
        {
            lblHeader.Foreground = Brushes.OrangeRed;
            lblHeader.Content = "TREK AAN DE HENDEL";
            btnPull.IsEnabled = true;
        }

        private void RefreshCreditsLabel()
        {
            lblCredits.Content = "CREDITS: " + IngelogdeGebruiker.Credits.ToString();
        }
    }
}
