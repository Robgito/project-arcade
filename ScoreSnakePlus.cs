namespace Project_3___Arcade
{
    public partial class ScoreSnake
    {
        public override string ToString()
        {
            return $"{Datamanager.GetGebruiker(FKGebruiker).Gebruikersnaam.PadRight(50)} {Score}";
        }
    }
}
