using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for AanpassingGebruiker.xaml
    /// </summary>
    public partial class AanpassingGebruiker : Window
    {
        DispatcherTimer errorTimer;

        public AanpassingGebruiker()
        {
            InitializeComponent();
            errorTimer = new DispatcherTimer();
            errorTimer.Interval = TimeSpan.FromSeconds(5);
            errorTimer.Tick += ErrorTimer_Tick;
        }

        Gebruiker huidigeGebruiker;
        List<Gebruiker> lstgebruikers;
        MessageBoxResult antwoordMessageBox;

        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lstgebruikers = new List<Gebruiker>();
                lstgebruikers = Datamanager.GetGebruikers();

                huidigeGebruiker = new Gebruiker();
                huidigeGebruiker = Datamanager.GetGebruiker(IngelogdeGebruiker.UserID);

                cmbGebruikers.Items.Add(huidigeGebruiker);
                cmbGebruikers.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het inladen van de gebruikers.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                if (!string.IsNullOrEmpty(txtGbruikersnaam.Text) && !string.IsNullOrEmpty(txtPaswoord.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    bool checkGebruikersnaamBestaat = false;
                    bool checkEmailBestaat = false;

                    foreach (var g in lstgebruikers)
                    {
                        if (g.Gebruikersnaam.ToLower() == txtGbruikersnaam.Text.ToLower() && g.Gebruikersnaam.ToLower() != IngelogdeGebruiker.Gebruikersnaam.ToLower())
                        {
                            checkGebruikersnaamBestaat = true;
                            lblError.Content = "Opslaan mislukt:";
                            lblError2.Content = "Gebruikersnaam is reeds in gebruik!";
                            errorTimer.Start();
                            break;
                        }
                        else if (g.Email.ToLower() == txtEmail.Text.ToLower() && g.Email.ToLower() != IngelogdeGebruiker.Email.ToLower())
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
                                huidigeGebruiker.Gebruikersnaam = txtGbruikersnaam.Text;
                                huidigeGebruiker.Paswoord = txtPaswoord.Text;
                                huidigeGebruiker.Email = txtEmail.Text;

                                //ImgPath ophalen:
                                if (imgProfile.Source != null && imgProfile.Source is BitmapImage)
                                {
                                    string originalUri = ((BitmapImage)imgProfile.Source).UriSource.OriginalString;

                                    string fileName = System.IO.Path.GetFileName(originalUri);
                                    huidigeGebruiker.Avatar = @"Images\" + fileName;
                                }
                                Datamanager.UpdateGebruiker(huidigeGebruiker);

                                MessageBox.Show("De wijzigingen werden opgeslaan!", "Melding", MessageBoxButton.OK, MessageBoxImage.Information);
                                
                                this.Close();
                            }
                            else
                            {
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
            catch
            {
                lblError.Content = "Opslaan mislukt:";
                lblError2.Content = "Er is een fout opgetreden bij het opslaan!";
                errorTimer.Start();
            }
        }

        private void btnFotoZoeken_Click(object sender, RoutedEventArgs e)
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
                    imgProfile.Source = new BitmapImage(new Uri(characterSelectie.SelectedImage, UriKind.Relative));
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van het avatarscherm.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTerugNaarHoofdmenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                Hoofdmenu hoofdmenu = new Hoofdmenu();
                hoofdmenu.IngelogdeGebruiker = this.IngelogdeGebruiker;
                hoofdmenu.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van het hoofdmenu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbGebruikers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbGebruikers.SelectedItem != null)
                {
                    Gebruiker geselecteerdeGebruiker = (Gebruiker)cmbGebruikers.SelectedItem;

                    txtGbruikersnaam.Text = geselecteerdeGebruiker.Gebruikersnaam;
                    txtPaswoord.Text = geselecteerdeGebruiker.Paswoord;
                    txtEmail.Text = geselecteerdeGebruiker.Email;

                    if (geselecteerdeGebruiker.Avatar != null)
                    {
                        imgProfile.Source = new BitmapImage(new Uri(geselecteerdeGebruiker.Avatar, UriKind.Relative));
                    }
                    else
                    {
                        imgProfile.Source = new BitmapImage(new Uri(@"Images\DefaultAvatar.png", UriKind.Relative));
                    }

                    txtGbruikersnaam.Focus();
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

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+\/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        //private bool BestaatAl(Gebruiker dezegebruiker)
        //{
        //    bool resultaat = false;
        //    lstgebruikers = Datamanager.GetGebruikers();

        //    foreach (Gebruiker gebruiker in lstgebruikers)
        //    {
        //        if (gebruiker.Equals(dezegebruiker) && gebruiker.UserID != IngelogdeGebruiker.UserID)
        //        {
        //            resultaat = true;
        //            lblError.Content = "Opslaan mislukt: Gebruikersnaam is reeds in gebruik!";
        //        }
        //        else
        //        {
        //            dezegebruiker.Gebruikersnaam = txtGbruikersnaam.Text;
        //            dezegebruiker.Paswoord = txtPaswoord.Text;
        //            dezegebruiker.Email = txtEmail.Text;

        //            //ImgPath ophalen:
        //            if (imgProfile.Source != null && imgProfile.Source is BitmapImage)
        //            {
        //                string originalUri = ((BitmapImage)imgProfile.Source).UriSource.OriginalString;

        //                string fileName = System.IO.Path.GetFileName(originalUri);
        //                huidigeGebruiker.Avatar = @"Images\" + fileName;
        //            }
        //            Datamanager.UpdateGebruiker(dezegebruiker);
        //        }
        //    }
        //    return resultaat;
        //}

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
