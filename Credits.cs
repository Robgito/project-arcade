namespace Project_3___Arcade
{
    public static class Credits
    {
        private static int _tetrisCost = 3;
        private static int _endlessRunnerCost = 3;
        private static int _snakeCost = 3;
        private static int _flappyBirdCost = 3;
        private static int _zombieShooterCost = 5;

        public static int TetrisCost { get { return _tetrisCost; } set { _tetrisCost = value; } }
        public static int EndlessRunnerCost { get { return _endlessRunnerCost; } set { _endlessRunnerCost = value; } }
        public static int SnakeCost { get { return _snakeCost; } set { _snakeCost = value; } }
        public static int FlappyBirdCost { get { return _flappyBirdCost; } set { _flappyBirdCost = value; } }
        public static int ZombieShooterCost {  get { return _zombieShooterCost;} set { _zombieShooterCost = value; }}
    }
}
