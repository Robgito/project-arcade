using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using loginscreen_games;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for Registreren.xaml
    /// </summary>
    public partial class Registreren : Window
    {
        public Registreren()
        {
            InitializeComponent();
        }

        Gebruiker gebruiker = new Gebruiker();
        List<Gebruiker> lstGebruikers = new List<Gebruiker>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtUsername.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtRePassword.Text = string.Empty;
                imgProfiel.Source = new BitmapImage(new Uri(@"\Images\DefaultAvatar.png", UriKind.Relative));
                txtUsername.Focus();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CharacterSelectie characterSelectie = new CharacterSelectie();
                characterSelectie.Close();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                if (txtUsername.Text != "" && txtUsername.Text != null && txtPassword.Text != "" && txtPassword.Text != null)
                {
                    if (IsValidEmail(txtEmail.Text))
                    {
                        gebruiker.Gebruikersnaam = txtUsername.Text;
                        gebruiker.Email = txtEmail.Text;
                        gebruiker.Paswoord = txtPassword.Text;
                        //gebruiker.Avatar = imgProfiel.Source.ToString();

                        //ImgPath ophalen:
                        if (imgProfiel.Source != null && imgProfiel.Source is BitmapImage)
                        {
                            string originalUri = ((BitmapImage)imgProfiel.Source).UriSource.OriginalString;

                            string fileName = System.IO.Path.GetFileName(originalUri);
                            gebruiker.Avatar = @"Images\" + fileName;
                        }


                        lstGebruikers = Datamanager.GetGebruikers();
                        bool checkGebruikersnaamBestaat = false;
                        bool checkEmailBestaat = false;

                        if (lstGebruikers != null)
                        {
                            foreach (Gebruiker gebruiker1 in lstGebruikers)
                            {
                                if (gebruiker1.Gebruikersnaam == txtUsername.Text)
                                {
                                    checkGebruikersnaamBestaat = true;
                                    lblError.Content = "Opslaan mislukt: Gebruikersnaam is reeds in gebruik!";
                                    break;
                                }
                                else if (gebruiker1.Email == txtEmail.Text)
                                {
                                    checkEmailBestaat = true;
                                    lblError.Content = "Opslaan mislukt: E-mailadres reeds in gebruik!";
                                    break;
                                }
                            }
                            if (checkGebruikersnaamBestaat == false && checkEmailBestaat == false)
                            {
                                if (txtPassword.Text == txtRePassword.Text)
                                {
                                    gebruiker.Credits = 10;
                                    Datamanager.InsertGebruiker(gebruiker);
                                    MainWindow login = new MainWindow();
                                    login.Show();
                                    this.Close();
                                }
                                else
                                {
                                    txtPassword.Clear();
                                    txtRePassword.Clear();
                                    lblError.Content = "Opslaan mislukt: Wachtwoorden komen niet overeen!";
                                }
                            }
                        }
                        else
                        {
                            if (txtPassword.Text == txtRePassword.Text)
                            {
                                gebruiker.Credits = 10;
                                Datamanager.InsertGebruiker(gebruiker);
                                MainWindow login = new MainWindow();
                                login.Show();
                                this.Close();
                            }
                            else
                            {
                                txtPassword.Clear();
                                txtRePassword.Clear();
                                lblError.Content = "Opslaan mislukt: Wachtwoorden komen niet overeen!";
                            }
                        }
                    }
                    else
                    {
                        lblError.Content = "Opslaan mislukt: Ongeldig e-mailadres!";
                    }
                }
                else
                {
                    lblError.Content = "Opslaan mislukt: Gelieve verplichte velden in te vullen!";
                }
            }
            catch
            {
                lblError.Content = "Opslaan mislukt: Er is een fout opgetreden bij het opslaan!";
            }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                CharacterSelectie characterSelectie = new CharacterSelectie();
                MainWindow login = new MainWindow();
                login.Show();
                characterSelectie.Close();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het annuleren.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Email validatie
        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&’*+\/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&’*+\/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEmail.Clear();
            txtEmail.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BA55D3"));
        }

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                CharacterSelectie characterSelectie = new CharacterSelectie();

                double left = this.Left + this.Width; // Rechts plaatsen van het registratie scherm
                double top = this.Top;

                characterSelectie.Left = left;
                characterSelectie.Top = top;

                var result = characterSelectie.ShowDialog();
                if (result == true)
                {
                    imgProfiel.Source = new BitmapImage(new Uri(characterSelectie.SelectedImage, UriKind.Relative));
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van het avatarscherm.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
