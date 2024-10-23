//using Microsoft.Office.Interop.Excel;

namespace Project_3___Arcade
{
    public partial class ScoreZombieShooter
    {
        public override string ToString()
        {
            return $"{Datamanager.GetGebruiker(FKGebruiker).Gebruikersnaam.PadRight(50)} {Score}";
        }
    }
}
