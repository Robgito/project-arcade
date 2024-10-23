using System;
using System.Media;

namespace Project_3___Arcade
{
    public static class Muziek
    {
        public static void GameOverRun()
        {
            try
            {
                string path = Environment.CurrentDirectory;
                SoundPlayer player = new SoundPlayer(path + "\\SoundsRun\\RunOver.wav");
                player.Play();
            }
            catch (Exception)
            {
                Console.WriteLine("Er is een fout opgetreden bij het laden van het geluid.");
            }
        }
        public static void Jumping()
        {
            try
            {
                string path = Environment.CurrentDirectory;
                SoundPlayer player = new SoundPlayer(path + "\\SoundsRun\\Jumping.wav");
                player.Play();
            }
            catch (Exception)
            {
                Console.WriteLine("Er is een fout opgetreden bij het laden van het geluid.");
            }
        }
        public static void CoinGeluid()
        {
            try
            {
                string path = Environment.CurrentDirectory;
                SoundPlayer player = new SoundPlayer(path + "\\Sounds\\RetroGameCoinSoundEffect.wav");
                player.Play();
            }
            catch (Exception)
            {
                Console.WriteLine("Er is een fout opgetreden bij het laden van het geluid.");
            }
        }
        public static void MusicThemeIntro()
        {
            try
            {
                string path = Environment.CurrentDirectory;
                SoundPlayer player = new SoundPlayer(path + "\\Sounds\\arcadeIntroMusic.wav");
                player.Play();
            }
            catch (Exception)
            {
                Console.WriteLine("Er is een fout opgetreden bij het laden van het geluid.");
            }
        }
    }
}
