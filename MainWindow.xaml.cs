using Project_3___Arcade;
using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace loginscreen_games
{
    public partial class MainWindow : Window
    {
        private double gradientOffset = 0;
        private DispatcherTimer timer;
        Gebruiker gebruiker = new Gebruiker();
        List<Gebruiker> lstGebruikers = new List<Gebruiker>();

        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        //Gradient animatie
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool correctIngelogd = false;
                bool paswoordCorrect = true;


                if (txtUsername.Text != "" && txtUsername.Text != null && txtPassword.Password != "" && txtPassword.Password != null)
                {
                    foreach (Gebruiker gebruiker1 in lstGebruikers)
                    {
                        if (txtUsername.Text == gebruiker1.Gebruikersnaam && txtPassword.Password == gebruiker1.Paswoord)
                        {
                            IngelogdeGebruiker = gebruiker1;
                            correctIngelogd = true;
                            break;
                        }
                        else if (txtUsername.Text == gebruiker1.Gebruikersnaam && txtPassword.Password != gebruiker1.Paswoord)
                        {
                            IngelogdeGebruiker = gebruiker1;
                            paswoordCorrect = false;
                        }
                    }

                    if (correctIngelogd == true)
                    {
                        try
                        {
                            string path = Environment.CurrentDirectory;

                            SoundPlayer player = new SoundPlayer(path + "\\Sounds\\RetroGameCoinSoundEffect.wav");
                            player.Play();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error playing sound: " + ex.Message);
                        }

                        IngelogdeGebruiker.PaswoordTeller = 0;

                        Hoofdmenu hoofdmenu = new Hoofdmenu();
                        hoofdmenu.IngelogdeGebruiker = this.IngelogdeGebruiker;
                        hoofdmenu.Show();
                        this.Close();
                    }
                    else if (paswoordCorrect == false)
                    {
                        if (IngelogdeGebruiker.Admin == false)
                        {
                            if (IngelogdeGebruiker.PaswoordTeller < 3)
                            {
                                txtPassword.Foreground = new SolidColorBrush(Colors.Red);
                                lblError.Content = "Gebruikers en/of wachtwoord is niet correct.";
                                IngelogdeGebruiker.PaswoordTeller++;
                                Datamanager.UpdateGebruiker(IngelogdeGebruiker);

                                txtUsername.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BA55D3"));
                                txtPassword.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BA55D3"));
                            }
                            else
                            {
                                lblError.Content = "U heeft te veel pogingen gedaan om in te loggen.\n\tGelieve de administrator te contacteren.";
                            }
                        }
                        else
                        {
                            txtPassword.Foreground = new SolidColorBrush(Colors.Red);
                            lblError.Content = "Gebruikers en/of wachtwoord is niet correct.";

                            txtUsername.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BA55D3"));
                            txtPassword.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BA55D3"));
                        }
                    }
                    else
                    {
                        lblError.Content = "Gebruikers en/of wachtwoord is niet correct.";
                    }

                    txtPassword.Clear();
                    txtUsername.Clear();
                    txtUsername.Focus();
                }
                else
                {
                    txtUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                    txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                    txtUsername.Foreground = new SolidColorBrush(Colors.Red);
                    lblError.Content = "Alle velden moeten ingevuld worden";
                    txtUsername.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BA55D3"));
                    txtUsername.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BA55D3"));
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het inloggen.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Registreren registreren = new Registreren();
                registreren.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het registreren.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                string path = Environment.CurrentDirectory;

                SoundPlayer player = new SoundPlayer(path + "\\Sounds\\RetroGameCoinSoundEffect.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lstGebruikers = Datamanager.GetGebruikers();

                txtUsername.Focus();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het inladen van de gebruikers.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                string path = Environment.CurrentDirectory;

                SoundPlayer player = new SoundPlayer(path + "\\Sounds\\arcadeIntroMusic.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }
        }

        private void lblClose_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
