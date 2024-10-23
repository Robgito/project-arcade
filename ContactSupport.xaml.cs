using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for ContactSupport.xaml
    /// </summary>
    public partial class ContactSupport : Window
    {
        MessageBoxResult antwoordMessageBox;
        DispatcherTimer errorTimer;

        public ContactSupport()
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
            txtTitel.Focus();
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

        private void btnVerzenden_Click(object sender, RoutedEventArgs e)
        {
            string directory = Environment.CurrentDirectory + @"\BerichtenAdmin";
            string pad = Environment.CurrentDirectory + @"\BerichtenAdmin\" + txtTitel.Text + "  - " + IngelogdeGebruiker.Gebruikersnaam + ".txt";
            lblError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));

            try
            {
                Muziek.CoinGeluid();

                if (!string.IsNullOrEmpty(txtBericht.Text))
                {
                    if (!string.IsNullOrEmpty(txtTitel.Text))
                    {
                        if (!Directory.Exists(directory))               //maakt nieuwe map aan als deze nog niet bestaat
                        {
                            Directory.CreateDirectory(directory);
                        }
                        if (!System.IO.File.Exists(pad))                //check of bericht al bestaat (anders wordt dit overschreven)
                        {
                            antwoordMessageBox = MessageBox.Show("Bent u zeker dat u dit bericht wil verzenden?", "Vraag", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (antwoordMessageBox == MessageBoxResult.Yes)
                            {
                                using (StreamWriter mijnWriter = new StreamWriter(pad))
                                {
                                    mijnWriter.WriteLine(txtBericht.Text + Environment.NewLine);
                                }

                                lblError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Green"));
                                lblError.Content = "Bericht werd verzonden!";
                                TXTLeegmaken();
                                txtTitel.Focus();
                                errorTimer.Start();
                            }
                            else
                            {
                                lblError.Content = "Bericht werd NIET verzonden!";
                                errorTimer.Start();
                            }
                        }
                        else
                        {
                            lblError.Content = "Verzenden mislukt!:";
                            lblError2.Content = "Reeds bericht met dezelfde titel verzonden.";
                            errorTimer.Start();
                        }
                    }
                    else
                    {
                        lblError.Content = "Verzenden mislukt!:";
                        lblError2.Content = "Gelieve een titel in te vullen.";
                        errorTimer.Start();
                    }
                }
                else
                {
                    lblError.Content = "Verzenden mislukt!:";
                    lblError2.Content = "U kan geen leeg bericht verzenden.";
                    errorTimer.Start();
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het verzenden van het bericht.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het afsluiten van het scherm.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TXTLeegmaken()
        {
            txtBericht.Text = string.Empty;
            txtTitel.Text = string.Empty;
        }
    }
}
