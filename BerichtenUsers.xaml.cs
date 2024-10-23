using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Windows.Media;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for BerichtenUsers.xaml
    /// </summary>
    public partial class BerichtenUsers : Window
    {
        string directory = Environment.CurrentDirectory + @"\BerichtenAdmin";
        string geselecteerdeBestand;
        string bestandPad;
        string geselecteerdeItem;
        string[] bestanden;
        string[] gesplitst;
        List<BestandInfo> lijstBestandenMetDatum = new List<BestandInfo>();

        DispatcherTimer errorTimer;

        public BerichtenUsers()
        {
            InitializeComponent();
            errorTimer = new DispatcherTimer();
            errorTimer.Interval = TimeSpan.FromSeconds(4);
            errorTimer.Tick += ErrorTimer_Tick;
        }

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
                lblError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden in de timer.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bestanden = Directory.GetFiles(directory);
            try
            {
                foreach (string bestand in bestanden)
                {
                    DateTime opmaakDatum = File.GetCreationTime(bestand);
                    lijstBestandenMetDatum.Add(new BestandInfo { Naam = Path.GetFileName(bestand), OpmaakDatum = opmaakDatum });
                }

                RefreshListbox();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van de mailbox.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstMailbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstMailbox.SelectedIndex != -1)
                {
                    if (lstMailbox.SelectedIndex != -1)
                    {
                        geselecteerdeItem = lstMailbox.SelectedItem.ToString();
                        gesplitst = geselecteerdeItem.Split(new[] { "  -  " }, StringSplitOptions.RemoveEmptyEntries);

                        if (gesplitst.Length == 2)
                        {
                            geselecteerdeBestand = gesplitst[0];
                            string bericht = File.ReadAllText(Path.Combine(directory, geselecteerdeBestand));

                            txtTitel.Text = geselecteerdeBestand;
                            txtBericht.Text = bericht;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het laden van het bericht.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnVerwijderBericht_Click(object sender, RoutedEventArgs e)
        {
            Muziek.CoinGeluid();

            if (lstMailbox.SelectedIndex != -1)
            {
                geselecteerdeItem = lstMailbox.SelectedItem.ToString();
                gesplitst = geselecteerdeItem.Split(new[] { "  -  " }, StringSplitOptions.RemoveEmptyEntries);

                if (gesplitst.Length == 2)
                {
                    geselecteerdeBestand = gesplitst[0];
                    bestandPad = Path.Combine(directory, geselecteerdeBestand);
                }
                try
                {
                    MessageBoxResult antwoordMessageBox = MessageBox.Show("Bent u zeker dat u dit bericht wil verwijderen?", "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (antwoordMessageBox == MessageBoxResult.Yes)
                    {
                        File.Delete(bestandPad);

                        foreach (var bestand in lijstBestandenMetDatum)
                        {
                            if (bestand.Naam.Equals(txtTitel.Text))
                            {
                                lijstBestandenMetDatum.Remove(bestand);
                                break;
                            }
                        }

                        RefreshListbox();

                        lblError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Green"));
                        lblError.Content = "Bericht is succesvol verwijderd!";

                        txtTitel.Text = string.Empty;
                        txtBericht.Text = string.Empty;

                        errorTimer.Start();
                    }
                    else
                    {
                        lblError.Content = "Bericht is NIET verwijderd!";
                        errorTimer.Start();
                    }
                }
                catch
                {
                    MessageBox.Show("Er is een fout opgetreden bij het verwijderen van het bericht.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                lblError.Content = "Gelieve eerst een bericht te selecteren!";
                errorTimer.Start();
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void RefreshListbox()
        {
            lijstBestandenMetDatum.Sort((a, b) => b.OpmaakDatum.CompareTo(a.OpmaakDatum)); //Bart: sorteren bestanden op datum

            lstMailbox.ItemsSource = null;
            lstMailbox.ItemsSource = lijstBestandenMetDatum;
        }
    }
}
