using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using loginscreen_games;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private Gebruiker _ingelogdeGebruiker;
        private List<Gebruiker> listGebruikers;
        private Gebruiker selectedGebruiker = new Gebruiker();

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }
        public Admin()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listGebruikers = Datamanager.GetGebruikers();
            lbGebruikers.Items.Clear();
            lbGebruikers.ItemsSource = listGebruikers;
            imgAvatar.Source = new BitmapImage(new Uri(@"Images\DefaultAvatar.png", UriKind.Relative));

        }

        private void btnHoofdmenu_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();
            this.Close();
        }

        private void lbGebruikers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lbGebruikers.SelectedIndex != -1)
            {
                selectedGebruiker = lbGebruikers.SelectedItem as Gebruiker;


                txtID.Text = selectedGebruiker.UserID.ToString();
                txtNaam.Text = selectedGebruiker.Gebruikersnaam;
                txtPaswoord.Password = selectedGebruiker.Paswoord;
                txtEmail.Text = selectedGebruiker.Email;
                nudCredits.Value = selectedGebruiker.Credits;
                btnWijzigen.IsEnabled = true;

                if (selectedGebruiker.Admin) rdoAdmin.IsChecked = true;
                else if (selectedGebruiker.Beheerder) rdoBeheerder.IsChecked = true;
                else rdoGebruiker.IsChecked = true;

                imgAvatar.Source = new BitmapImage(new Uri(selectedGebruiker.Avatar, UriKind.Relative));
                if (selectedGebruiker.UserID != IngelogdeGebruiker.UserID)
                {
                    btnVerwijderen.IsEnabled = true;
                    rdoAdmin.IsEnabled = true;
                    rdoBeheerder.IsEnabled = true;
                    rdoGebruiker.IsEnabled = true;
                }
                else
                {
                    btnVerwijderen.IsEnabled = false;
                    rdoAdmin.IsEnabled = false;
                    rdoBeheerder.IsEnabled = false;
                    rdoGebruiker.IsEnabled = false;
                }
            }
            else
            {
                selectedGebruiker = null;
                btnVerwijderen.IsEnabled = false;
                btnWijzigen.IsEnabled = false;

            }
        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                var zeker = MessageBox.Show("Bent u zeker dat u deze user wil aanmaken?", "Aanmaken user", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    if (!IsValidEmail(txtEmail.Text))
                        throw new System.Exception("Email is niet in juiste formaat.");
                    if (string.IsNullOrEmpty(txtNaam.Text))
                    {
                        throw new System.Exception("Naam is niet ingevuld");
                    }
                    if (string.IsNullOrEmpty(txtPaswoord.Password))
                    {
                        throw new System.Exception("Paswoord is niet ingevuld");
                    }

                    foreach (Gebruiker gebruiker in listGebruikers)
                    {
                        if (gebruiker.Gebruikersnaam == txtNaam.Text || gebruiker.Email == txtEmail.Text)
                            throw new System.Exception("Email en/of gebruikersnaam al in gebruik.");
                    }
                    Gebruiker nieuweGebruiker = new Gebruiker();
                    nieuweGebruiker.Credits = (int)nudCredits.Value;
                    nieuweGebruiker.Gebruikersnaam = txtNaam.Text;
                    nieuweGebruiker.Paswoord = txtPaswoord.Password;
                    nieuweGebruiker.Email = txtEmail.Text;
                    if (rdoAdmin.IsChecked == true)
                    {
                        nieuweGebruiker.Beheerder = false;
                        nieuweGebruiker.Admin = true;
                    }
                    else if (rdoBeheerder.IsChecked == true)
                    {
                        nieuweGebruiker.Beheerder = true;
                        nieuweGebruiker.Admin = false;
                    }
                    else
                    {
                        nieuweGebruiker.Beheerder = false;
                        nieuweGebruiker.Admin = false;
                    }
                    //avatar logica toevoegen
                    if (imgAvatar.Source != null && imgAvatar.Source is BitmapImage)
                    {
                        string originalUri = ((BitmapImage)imgAvatar.Source).UriSource.OriginalString;

                        string fileName = System.IO.Path.GetFileName(originalUri);
                        nieuweGebruiker.Avatar = @"Images\" + fileName;
                    }
                    else { nieuweGebruiker.Avatar = @"Images\DefaultAvatar.png"; }

                    if (Datamanager.InsertGebruiker(nieuweGebruiker))
                    {
                        MessageBox.Show("De user is succesvol toegevoegd!", "Succes!", MessageBoxButton.OK);
                        VakjesLegenEnzo();
                    }
                    else
                    {
                        throw new Exception("De gebruiker werd niet toegevoegd, contacteer de beheerder.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error bij het aanmaken van een user", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                var zeker = MessageBox.Show("Bent u zeker dat u deze user wil aanpassen?", "Aanpassen User", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    if (!IsValidEmail(txtEmail.Text))
                        throw new System.Exception("Email is niet in juiste formaat.");
                    if (string.IsNullOrEmpty(txtNaam.Text))
                    {
                        throw new System.Exception("Naam is niet ingevuld");
                    }
                    if (string.IsNullOrEmpty(txtPaswoord.Password))
                    {
                        throw new System.Exception("Paswoord is niet ingevuld");
                    }

                    foreach (Gebruiker gebruiker in listGebruikers)
                    {
                        if ((gebruiker.Gebruikersnaam == txtNaam.Text || gebruiker.Email == txtEmail.Text) && gebruiker.UserID != selectedGebruiker.UserID)
                            throw new System.Exception("Email en/of gebruikersnaam al in gebruik.");
                    }
                    selectedGebruiker.Credits = (int)nudCredits.Value;
                    selectedGebruiker.Gebruikersnaam = txtNaam.Text;
                    selectedGebruiker.Paswoord = txtPaswoord.Password;
                    selectedGebruiker.Email = txtEmail.Text;
                    if (rdoAdmin.IsChecked == true)
                    {
                        selectedGebruiker.Beheerder = false;
                        selectedGebruiker.Admin = true;
                    }
                    else if (rdoBeheerder.IsChecked == true)
                    {
                        selectedGebruiker.Beheerder = true;
                        selectedGebruiker.Admin = false;
                    }
                    else
                    {
                        selectedGebruiker.Beheerder = false;
                        selectedGebruiker.Admin = false;
                    }
                    //avatar logica toevoegen
                    if (imgAvatar.Source != null && imgAvatar.Source is BitmapImage)
                    {
                        string originalUri = ((BitmapImage)imgAvatar.Source).UriSource.OriginalString;

                        string fileName = System.IO.Path.GetFileName(originalUri);
                        selectedGebruiker.Avatar = @"Images\" + fileName;
                    }
                    else { selectedGebruiker.Avatar = @"Images\DefaultAvatar.png"; }

                    if (Datamanager.UpdateGebruiker(selectedGebruiker))
                    {
                        MessageBox.Show("De user is succesvol geüpdate!", "Succes!", MessageBoxButton.OK);
                        if(selectedGebruiker.UserID == IngelogdeGebruiker.UserID)
                        {
                            IngelogdeGebruiker.Avatar = selectedGebruiker.Avatar;
                            IngelogdeGebruiker.Credits = selectedGebruiker.Credits;
                            IngelogdeGebruiker.Paswoord = selectedGebruiker.Paswoord;
                            IngelogdeGebruiker.Email = selectedGebruiker.Email;
                            IngelogdeGebruiker.Gebruikersnaam = selectedGebruiker.Gebruikersnaam;
                        }
                        VakjesLegenEnzo();
                    }
                    else
                    {
                        throw new Exception("De gebruiker werd niet geüpdate, contacteer de beheerder.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error bij het updaten van een user", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                var zeker = MessageBox.Show("Bent u zeker dat u deze user wilt verwijderen?", "Verwijderen User", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    ScoreTetris tetrisScore = new ScoreTetris();
                    tetrisScore = Datamanager.GetScoreTetrisGebruiker(selectedGebruiker.UserID);
                    if (tetrisScore != null)
                    {
                        Datamanager.DeleteScoreTetris(tetrisScore);
                    }

                    ScoreEndlessRunner endlessRunnerScore = new ScoreEndlessRunner();
                    endlessRunnerScore = Datamanager.GetScoreEndlessRunnerGebruiker(selectedGebruiker.UserID);
                    if (endlessRunnerScore != null)
                    {
                        Datamanager.DeleteScoreEndlessRunner(endlessRunnerScore);
                    }

                    ScoreFlappyBird flappyBirdScore = new ScoreFlappyBird();
                    flappyBirdScore = Datamanager.GetScoreFlappyBirdGebruiker(selectedGebruiker.UserID);
                    if (flappyBirdScore != null)
                    {
                        Datamanager.DeleteScoreFlappyBird(flappyBirdScore);
                    }

                    ScoreSnake snakeScore = new ScoreSnake();
                    snakeScore = Datamanager.GetScoreSnakeGebruiker(selectedGebruiker.UserID);
                    if (snakeScore != null)
                    {
                        Datamanager.DeleteScoreSnake(snakeScore);
                    }

                    ScoreZombieShooter zombieShooterScore = new ScoreZombieShooter();
                    zombieShooterScore = Datamanager.GetScoreZombieShooterGebruiker(selectedGebruiker.UserID);
                    if (zombieShooterScore != null)
                    {
                        Datamanager.DeleteScoreZombieShooter(zombieShooterScore);
                    }

                    if (Datamanager.DeleteGebruiker(selectedGebruiker))
                    {
                        MessageBox.Show("De user is succesvol gedelete!", "Succes!", MessageBoxButton.OK);
                        VakjesLegenEnzo();
                    }
                    else
                    {
                        throw new Exception("De gebruiker werd niet verwijderd, contacteer de beheerder.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error bij het updaten van een user", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnResetTeller_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CoinGeluid();

                var zeker = MessageBox.Show("Bent u zeker dat u de teller wilt restten?", "Reset Teller", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (zeker == MessageBoxResult.Yes)
                {
                    selectedGebruiker.PaswoordTeller = 0;
                    if (Datamanager.UpdateGebruiker(selectedGebruiker))
                    {
                        MessageBox.Show("De teller is gereset!", "Succes!", MessageBoxButton.OK);
                        VakjesLegenEnzo();
                    }
                    else
                    {
                        throw new Exception("De teller werd niet gereset, contacteer de beheerder.");
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error bij het resetten van een teller", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();
            //WordExcelStatic.PrintExcel(listGebruikers);

        }

        private void btnWord_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();
            try
            {
                //WordExcelStatic.PrintHighScore(selectedGebruiker, "Tetris", Datamanager.GetScoreTetrisGebruiker(selectedGebruiker.UserID)?.Score.ToString());
                //WordExcelStatic.PrintHighScore(selectedGebruiker, "Snake", Datamanager.GetScoreSnakeGebruiker(selectedGebruiker.UserID)?.Score.ToString());
                //WordExcelStatic.PrintHighScore(selectedGebruiker, "Flappy Bird", Datamanager.GetScoreFlappyBirdGebruiker(selectedGebruiker.UserID)?.Score.ToString());
                //WordExcelStatic.PrintHighScore(selectedGebruiker, "Endless Runner", Datamanager.GetScoreEndlessRunnerGebruiker(selectedGebruiker.UserID)?.Score.ToString());
                //WordExcelStatic.PrintHighScore(selectedGebruiker, "Zombie Shooter", Datamanager.GetScoreZombieShooterGebruiker(selectedGebruiker.UserID)?.Score.ToString());
                MessageBox.Show("Uw highscores werden succesvol weggeschreven!", "Melding:", MessageBoxButton.OK);
            }
            catch 
            {
                MessageBox.Show("Er liep iets mis bij het wegschrijven van de highscores.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&’*+\/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&’*+\/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        private void VakjesLegenEnzo()
        {
            txtEmail.Text = "";
            txtID.Text = "";
            txtNaam.Text = "";
            txtPaswoord.Password = "";
            nudCredits.Value = 0;
            imgAvatar.Source = null;
            rdoGebruiker.IsChecked = true;

            listGebruikers = Datamanager.GetGebruikers();
            lbGebruikers.ItemsSource = null;
            lbGebruikers.ItemsSource = listGebruikers;
            imgAvatar.Source = new BitmapImage(new Uri(@"Images\DefaultAvatar.png", UriKind.Relative));
        }

        private void btnAvatar_Click(object sender, RoutedEventArgs e)
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

        private void lblClose_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
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

        private void btnBerichtenUsers_Click(object sender, RoutedEventArgs e)
        {
            CoinGeluid();

            BlurAchtergrond(20);

            BerichtenUsers berichten = new BerichtenUsers();
            berichten.IngelogdeGebruiker = this.IngelogdeGebruiker;
            berichten.ShowDialog();

            BlurAchtergrond(0);
        }
        private void BlurAchtergrond(int radius)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = radius;

            GridAdmin.Effect = blur;
        }
    }
}
