using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_3___Arcade
{
    /// <summary>
    /// Interaction logic for Snake.xaml
    /// </summary>
    public partial class Snake : Window
    {
        MessageBoxResult antwoordMessageBox;
        private Gebruiker _ingelogdeGebruiker;

        public Gebruiker IngelogdeGebruiker
        {
            get { return _ingelogdeGebruiker; }
            set { _ingelogdeGebruiker = value; }
        }

        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new Dictionary<GridValue, ImageSource>()
        {
            { GridValue.Empty, Images.Empty },
            {GridValue.Snake, Images.Body },
            {GridValue.Food, Images.Food }
        };

        private readonly Dictionary<Direction, int> dirToRotation = new Dictionary<Direction, int>()
        {
            { Direction.Left, 270 },
            { Direction.Right, 90 },
            { Direction.Up, 0 },
            { Direction.Down, 180 }
        };
        private readonly int rows = 15, columns = 15;
        private readonly Image[,] gridImages;
        private GameState1S gameState1S;
        private bool gameRunning;
        public Snake()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState1S = new GameState1S(rows, columns);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }
            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState1S.GameOver)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Left:
                    gameState1S.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    gameState1S.ChangeDirection(Direction.Right);
                    break;
                case Key.Up:
                    gameState1S.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    gameState1S.ChangeDirection(Direction.Down);
                    break;
            }
        }
        private async Task RunGame()
        {
            Draw();
            await GameLoop();
            await ShowGameOver();
            gameState1S = new GameState1S(rows, columns);
        }
        private async Task GameLoop()
        {
            while (!gameState1S.GameOver)
            {
                await Task.Delay(200);
                gameState1S.Move();
                Draw();
            }
            //Overlay.Visibility = Visibility.Visible;
        }

        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, columns];
            GameGrid.Rows = rows;
            GameGrid.Columns = columns;
            GameGrid.Width = GameGrid.Height * (columns / (double)rows);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5)
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }

        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"SCORE {gameState1S.Score}";
        }
        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    GridValue gridVal = gameState1S.Grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridVal];
                    gridImages[r, c].RenderTransform = Transform.Identity;
                }
            }
        }
        private void DrawSnakeHead()
        {
            Position1S headPos = gameState1S.HeadPosition();
            Image image = gridImages[headPos.Row, headPos.Column];
            image.Source = Images.Head;

            int rotation = dirToRotation[gameState1S.Dir];
            image.RenderTransform = new RotateTransform(rotation);
        }
        private async Task DrawDeadSnake()
        {
            List<Position1S> positions = new List<Position1S>(gameState1S.SnakePositions());

            for (int i = 0; i < positions.Count; i++)
            {
                Position1S pos = positions[i];
                // if
                ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody;

                gridImages[pos.Row, pos.Column].Source = source;
                await Task.Delay(50);
            }
        }
        private async Task ShowGameOver()
        {
            await DrawDeadSnake();
            await Task.Delay(500);
            Overlay.Visibility = Visibility.Visible;

            ScoreSnake huidigeScore = Datamanager.GetScoreSnakeGebruiker(IngelogdeGebruiker.UserID);
            if (huidigeScore != null)
            {
                if (huidigeScore.Score < gameState1S.Score)
                {
                    huidigeScore.Score = Convert.ToInt32(gameState1S.Score);
                    Datamanager.UpdateScoreSnake(huidigeScore);
                }
            }
            else
            {
                ScoreSnake snakeScore = new ScoreSnake();

                int nieuweScore = Convert.ToInt32(gameState1S.Score);

                snakeScore.Score = nieuweScore;


                //ScoreID
                List<ScoreSnake> scoreSnake = Datamanager.GetScoresSnake();

                if (scoreSnake != null)
                {
                    snakeScore.ScoreID = scoreSnake.Count + 1;
                }
                else
                {
                    snakeScore.ScoreID = 1;
                }

                snakeScore.FKGebruiker = IngelogdeGebruiker.UserID;
                Datamanager.InsertScoreSnake(snakeScore);
            }
        }
        private async void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Credits.SnakeCost <= IngelogdeGebruiker.Credits)
                {
                    IngelogdeGebruiker.Credits -= Credits.SnakeCost;
                    Datamanager.UpdateGebruiker(IngelogdeGebruiker);
                    gameState1S = new GameState1S(rows, columns);
                    Overlay.Visibility = Visibility.Hidden;
                    await RunGame();
                }
                else
                {
                    MessageBox.Show("Niet genoeg credits! Koop credits in de creditstore!", "Onvoldoende credits", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het opstarten van Snake.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gameState1S.GameOver = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het teruggaan naar het vorige menu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTERUG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gameState1S.GameOver = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Er is een fout opgetreden bij het teruggaan naar het vorige menu.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
