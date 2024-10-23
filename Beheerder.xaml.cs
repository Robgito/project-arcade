using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using loginscreen_games;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for Beheerder.xaml
    /// </summary>
    public partial class Beheerder : Window
    {
        List<Gebruiker> lstGebruikers;
        MessageBoxResult antwoordMessageBox;
        DispatcherTimer errorTimer;

        public Beheerder()
        {
            InitializeComponent();
            errorTimer = new DispatcherTimer();
            errorTimer.Interval = TimeSpan.FromSeconds(5);
            errorTimer.Tick += ErrorTimer_Tick;
        }

        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshComboBoxGebruikers();
                DisableVelden();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het inladen van de gebruikers.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                errorTimer.Stop();
                lblError.Content = string.Empty;
                lblError2.Content = string.Empty;
                lblError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));
                lblError2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden in de timer.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();
                lblError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));

                if (cmbGebruikers.SelectedItem != null)
                {
                    //Gebruiker geselecteerdeGebruiker = new Gebruiker();
                    Gebruiker geselecteerdeGebruiker = (Gebruiker)cmbGebruikers.SelectedItem;

                    if (!string.IsNullOrEmpty(txtGebruikersnaam.Text) && !string.IsNullOrEmpty(txtPaswoord.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                    {
                        bool checkGebruikersnaamBestaat = false;
                        bool checkEmailBestaat = false;
                        foreach (var g in lstGebruikers)
                        {
                            if (g.Gebruikersnaam == txtGebruikersnaam.Text && g != geselecteerdeGebruiker)
                            {
                                checkGebruikersnaamBestaat = true;
                                lblError.Content = "Opslaan mislukt:";
                                lblError2.Content = "Gebruikersnaam is reeds in gebruik!";
                                errorTimer.Start();
                                break;
                            }
                            else if (g.Email == txtEmail.Text && g != geselecteerdeGebruiker)
                            {
                                checkEmailBestaat = true;
                                lblError.Content = "Opslaan mislukt:";
                                lblError2.Content = "E-mailadres reeds in gebruik!";
                                errorTimer.Start();
                                break;
                            }
                        }
                        if (checkGebruikersnaamBestaat == false && checkEmailBestaat == false)
                        {
                            if (IsValidEmail(txtEmail.Text))
                            {
                                antwoordMessageBox = MessageBox.Show("Bent u zeker dat u de wijzigingen wil opslaan?", "Vraag", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                if (antwoordMessageBox == MessageBoxResult.Yes)
                                {
                                    geselecteerdeGebruiker.Gebruikersnaam = txtGebruikersnaam.Text;
                                    geselecteerdeGebruiker.Paswoord = txtPaswoord.Text;
                                    geselecteerdeGebruiker.Email = txtEmail.Text.ToLower();
                                    geselecteerdeGebruiker.Credits = Convert.ToInt32(nudCredits.Value);

                                    //ImgPath ophalen:
                                    if (imgAvatar.Source != null && imgAvatar.Source is BitmapImage)
                                    {
                                        string originalUri = ((BitmapImage)imgAvatar.Source).UriSource.OriginalString;

                                        string fileName = System.IO.Path.GetFileName(originalUri);
                                        geselecteerdeGebruiker.Avatar = @"Images\" + fileName;
                                    }

                                    if (geselecteerdeGebruiker.UserID == IngelogdeGebruiker.UserID)
                                    {
                                        IngelogdeGebruiker = geselecteerdeGebruiker;
                                    }
                                    Datamanager.UpdateGebruiker(geselecteerdeGebruiker);

                                    RefreshComboBoxGebruikers();

                                    cmbGebruikers.SelectedIndex = -1;
                                    TXTLeegmaken();
                                    DisableVelden();

                                    lblError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Green"));
                                    lblError.Content = "De wijzigingen werden opgeslaan!";
                                    errorTimer.Start();
                                }
                                else
                                {
                                    cmbGebruikers.SelectedIndex = -1;
                                    TXTLeegmaken();
                                    lblError.Content = "De wijzigingen werden NIET opgeslaan!";
                                    errorTimer.Start();
                                }
                            }
                            else
                            {
                                lblError.Content = "Opslaan mislukt:";
                                lblError2.Content = "Ongeldig e-mailadres!";
                                errorTimer.Start();
                            }
                        }
                    }
                    else
                    {
                        lblError.Content = "Opslaan mislukt:";
                        lblError2.Content = "Gelieve verplichte velden in te vullen!";
                        errorTimer.Start();
                    }
                }
                else
                {
                    lblError.Content = "Opslaan mislukt:";
                    lblError2.Content = "Gelieve eerst een gebruiker te selecteren!";
                    errorTimer.Start();
                }
            }
            catch
            {
                lblError.Content = "Opslaan mislukt:";
                lblError2.Content = "Er is een fout opgetreden bij het opslaan!";
                errorTimer.Start();
            }
        }

        private void btnHoofdmenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van het hoofdmenu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAvatar_Click(object sender, RoutedEventArgs e)
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
                    imgAvatar.Source = new BitmapImage(new Uri(characterSelectie.SelectedImage, UriKind.Relative));
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van het avatarscherm.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void nudCredits_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (nudCredits.Value == null)
                {
                    nudCredits.Value = 0;
                    lblError.Content = "Het veld credits mag niet leeg zijn.";
                }
                else
                {
                    lblError.Content = string.Empty;
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbGebruikers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                lblError.Content = string.Empty;

                if (cmbGebruikers.SelectedItem != null)
                {

                    Gebruiker geselecteerdeGebruiker = (Gebruiker)cmbGebruikers.SelectedItem;

                    if (geselecteerdeGebruiker.Beheerder == false)
                    {
                        txtRol.Text = "Gebruiker";
                    }
                    else
                    {
                        txtRol.Text = "Beheerder";
                    }
                    txtGebruikersnaam.Text = geselecteerdeGebruiker.Gebruikersnaam;
                    txtPaswoord.Text = geselecteerdeGebruiker.Paswoord;
                    txtEmail.Text = geselecteerdeGebruiker.Email;
                    nudCredits.Value = geselecteerdeGebruiker.Credits;

                    if (geselecteerdeGebruiker.Avatar != null)
                    {
                        imgAvatar.Source = new BitmapImage(new Uri(geselecteerdeGebruiker.Avatar, UriKind.Relative));
                    }
                    else
                    {
                        imgAvatar.Source = new BitmapImage(new Uri(@"Images\DefaultAvatar.png", UriKind.Relative));
                    }

                    EnableVelden();
                    txtGebruikersnaam.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het weergeven van de gegevens.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void RefreshComboBoxGebruikers()
        {
            try
            {
                lstGebruikers = Datamanager.GetGebruikers();
                List<Gebruiker> lstTempGebruikers = new List<Gebruiker>();

                cmbGebruikers.ItemsSource = null;
                if (lstGebruikers != null)
                {
                    foreach (Gebruiker g in lstGebruikers)
                    {
                        if (g.Admin == false)
                        {
                            lstTempGebruikers.Add(g);
                        }
                    }
                    lstTempGebruikers = lstTempGebruikers.OrderBy(Gebruiker => Gebruiker.Gebruikersnaam).ToList();

                    cmbGebruikers.ItemsSource = lstTempGebruikers;
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het inladen van de lijst met gebruikers.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TXTLeegmaken()
        {
            try
            {
                txtRol.Text = string.Empty;
                txtGebruikersnaam.Text = string.Empty;
                txtPaswoord.Text = string.Empty;
                txtEmail.Text = string.Empty;
                nudCredits.Value = 0;
                imgAvatar.Source = new BitmapImage(new Uri(@"Images\DefaultAvatar.png", UriKind.Relative));
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het leegmaken van de velden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+\/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
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

        private void EnableVelden()
        {
            txtGebruikersnaam.IsEnabled = true;
            txtPaswoord.IsEnabled = true;
            txtEmail.IsEnabled = true;
            nudCredits.IsEnabled = true;
            btnAvatar.IsEnabled = true;
            btnOpslaan.IsEnabled = true;
        }

        private void DisableVelden()
        {
            txtGebruikersnaam.IsEnabled = false;
            txtPaswoord.IsEnabled = false;
            txtEmail.IsEnabled = false;
            nudCredits.IsEnabled = false;
            btnAvatar.IsEnabled = false;
            btnOpslaan.IsEnabled = false;
        }

    }
}
