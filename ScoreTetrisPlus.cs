namespace Project_3___Arcade
{
    public partial class ScoreTetris
    {
        public override string ToString()
        {
            return $"{Datamanager.GetGebruiker(FKGebruiker).Gebruikersnaam.PadRight(50)} {Score}";
        }
    }
}
