namespace Project_3___Arcade
{
    public partial class ScoreEndlessRunner
    {
        public override string ToString()
        {
            return $"{Datamanager.GetGebruiker(FKGebruiker).Gebruikersnaam.PadRight(50)} {Score}";
        }
    }
}
