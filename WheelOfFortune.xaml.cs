using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Project_3___Arcade
{
    public partial class WheelOfFortune : Window
    {
        private Random random;
        private bool isAnimating;
        private int targetNummer;
        private int huidigNummer;
        private int animatieDuur;
        private int animationIncrement;
        private SolidColorBrush goldBrush;
        private DispatcherTimer timer;

        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        public WheelOfFortune()
        {
            InitializeComponent();
            random = new Random();
            isAnimating = false;
            animatieDuur = 10000;
            animationIncrement = 25;
            goldBrush = new SolidColorBrush(Colors.Gold);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnClaim.Visibility = Visibility.Hidden;
            btnClaim.IsEnabled = false;

            //mediaElement.Play();
            await StartAnimation();
        }

        private async Task StartAnimation()
        {
            isAnimating = true;
            targetNummer = random.Next(1, 11);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(animationIncrement);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            huidigNummer = random.Next(1, 11);
            tbRandomNummer.Text = huidigNummer.ToString();
            animatieDuur -= animationIncrement;

            if (animatieDuur <= 0)
            {
                timer.Stop();
                tbRandomNummer.Text = targetNummer.ToString();
                tbRandomNummer.Foreground = goldBrush;
                isAnimating = false;

                _ingelogdeGebruiker.Credits += targetNummer;

                btnClaim.Visibility = Visibility.Visible;
                btnClaim.IsEnabled = true;
            }
        }

        private void btnClaim_Click(object sender, RoutedEventArgs e)
        {
            Datamanager.UpdateGebruiker(IngelogdeGebruiker);
            this.Close();
        }
    }
}
