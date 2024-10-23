using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for Tetris.xaml
    /// </summary>
    public partial class Tetris : Window
    {
        MessageBoxResult antwoordMessageBox;
        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
             new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
             new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
             new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
             new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
             new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
             new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
             new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
             new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };

        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 5;
        private GameState gameState = new GameState();

        public Tetris()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellsize = 25;
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageConrtol = new Image
                    {
                        Width = cellsize,
                        Height = cellsize
                    };

                    Canvas.SetTop(imageConrtol, (r - 2) * cellsize + 10);
                    Canvas.SetLeft(imageConrtol, c * cellsize);
                    GameCanvas.Children.Add(imageConrtol);
                    imageControls[r, c] = imageConrtol;
                }
            }
            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            try
            {
                for (int r = 0; r < grid.Rows; r++)
                {
                    for (int c = 0; c < grid.Columns; c++)
                    {
                        int id = grid[r, c];
                        imageControls[r, c].Opacity = 1;
                        imageControls[r, c].Source = tileImages[id];
                    }
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van het draw grid.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawBlock(Block block)
        {
            try
            {
                foreach (Position p in block.TilePositions())
                {
                    imageControls[p.Row, p.Column].Opacity = 1;
                    imageControls[p.Row, p.Column].Source = tileImages[block.Id];
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van het draw block.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            try
            {
                Block next = blockQueue.NextBlock;
                NextImage.Source = blockImages[next.Id];
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het aanmaken van het next draw block.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawHeldBlock(Block heldBlock)
        {
            try
            {
                if (heldBlock == null)
                {
                    HoldImage.Source = blockImages[0];
                }
                else
                {
                    HoldImage.Source = blockImages[heldBlock.Id];
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het aanmaken van het draw held block.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawGhostBlock(Block block)
        {
            try
            {
                int dropDistance = gameState.BlockDropDistance();

                foreach (Position p in block.TilePositions())
                {
                    imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                    imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het aanmaken van het draw ghost block.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Draw(GameState gameState)
        {
            try
            {
                DrawGrid(gameState.GameGrid);
                DrawGhostBlock(gameState.CurrentBlock);
                DrawBlock(gameState.CurrentBlock);
                DrawNextBlock(gameState.BlockQueue);
                DrawHeldBlock(gameState.HeldBlock);
                ScoreText.Text = $"Score: {gameState.Score}";
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task GameLoop()
        {
            try
            {
                Draw(gameState);
                while (!gameState.GameOver)
                {
                    int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                    await Task.Delay(delay);
                    gameState.MoveBlockDown();
                    Draw(gameState);
                }
                GameOverMenu.Visibility = Visibility.Visible;
                FinalScoreText.Text = $"Score: {gameState.Score}";
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden in de game loop.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();

            ScoreTetris huidigeScore = Datamanager.GetScoreTetrisGebruiker(IngelogdeGebruiker.UserID);
            if (huidigeScore != null)
            {
                if (huidigeScore.Score < gameState.Score)
                {
                    huidigeScore.Score = Convert.ToInt32(gameState.Score);
                    Datamanager.UpdateScoreTetris(huidigeScore);
                }
            }
            else
            {
                ScoreTetris tetrisScore = new ScoreTetris();

                int nieuweScore = Convert.ToInt32(gameState.Score);

                tetrisScore.Score = nieuweScore;


                //ScoreID
                List<ScoreTetris> scoreTetris = Datamanager.GetScoresTetris();

                if (scoreTetris != null)
                {
                    tetrisScore.ScoreID = scoreTetris.Count + 1;
                }
                else
                {
                    tetrisScore.ScoreID = 1;
                }

                tetrisScore.FKGebruiker = IngelogdeGebruiker.UserID;
                Datamanager.InsertScoreTetris(tetrisScore);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (gameState.GameOver)
                {
                    return;
                }
                switch (e.Key)
                {
                    case Key.Left:
                        gameState.MoveBlockLeft();
                        break;
                    case Key.Right:
                        gameState.MoveBlockRight();
                        break;
                    case Key.Down:
                        gameState.MoveBlockDown();
                        break;
                    case Key.Up:
                        gameState.RotateBlockCW();
                        break;
                    case Key.Z:
                        gameState.RotateBlockCCW();
                        break;
                    case Key.C:
                        gameState.HoldBlock();
                        break;
                    case Key.Space:
                        gameState.DropBlock();
                        break;
                    default:
                        return;
                }
                Draw(gameState);
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            //credits logica Tetris:
            try
            {
                if (Credits.TetrisCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.TetrisCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);

                    gameState = new GameState();
                    GameOverMenu.Visibility = Visibility.Hidden;
                    await GameLoop();
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van Tetris.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTERUG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gameState.GameOver = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het teruggaan naar het vorige menu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gameState.GameOver = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het teruggaan naar het vorige menu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
