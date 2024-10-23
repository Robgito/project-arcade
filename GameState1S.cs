using System;
using System.Collections.Generic;
using System.Media;

namespace Project_3___Arcade
{
    public class GameState1S
    {
        public int Rows { get; }
        public int Columns { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; set; }

        public SoundPlayer player2 = new SoundPlayer(Environment.CurrentDirectory + "\\SoundsSN\\hiss.wav");
        public SoundPlayer player1 = new SoundPlayer(Environment.CurrentDirectory + "\\SoundsSN\\SgameOver.wav");

        private readonly LinkedList<Direction> dirChanges = new LinkedList<Direction>();
        private readonly LinkedList<Position1S> snakePositions = new LinkedList<Position1S>();
        private readonly Random random = new Random();

        public GameState1S(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Grid = new GridValue[rows, columns];
            Dir = Direction.Right;

            AddSnake();
            AddFood();
        }
        private void AddSnake()
        {
            int r = Rows / 2;
            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Position1S(r, c));
            }
        }
        private IEnumerable<Position1S> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position1S(r, c);
                    }
                }
            }

        }
        private void AddFood()
        {
            List<Position1S> empty = new List<Position1S>(EmptyPositions());
            if (empty.Count == 0)
            {
                return;
            }

            Position1S pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Column] = GridValue.Food;
        }
        public Position1S HeadPosition()
        {
            return snakePositions.First.Value;
        }
        public Position1S TailPosition()
        {
            return snakePositions.Last.Value;
        }
        public IEnumerable<Position1S> SnakePositions()
        {
            return snakePositions;
        }
        private void AddHead(Position1S pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Column] = GridValue.Snake;
        }
        private void RemoveTail()
        {
            Position1S tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Column] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        private Direction GetLastDirection()
        {
            if (dirChanges.Count == 0)
            {
                return Dir;
            }
            return dirChanges.Last.Value;
        }
        private bool CanChangeDirection(Direction newDir)
        {
            if (dirChanges.Count == 2)
            {
                return false;
            }
            Direction lastDir = GetLastDirection();
            return newDir != lastDir && newDir != lastDir.Opposite();
        }
        public void ChangeDirection(Direction direction)
        {
            if (CanChangeDirection(direction))
            {
                dirChanges.AddLast(direction);
            }
        }

        private bool OutsideGrid(Position1S pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns;
        }
        private GridValue WilHit(Position1S newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }
            if (newHeadPos == TailPosition())
            {
                return GridValue.Empty;
            }
            return Grid[newHeadPos.Row, newHeadPos.Column];
        }
        public void Move()
        {

            if (dirChanges.Count > 0)
            {
                Dir = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }
            Position1S newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit = WilHit(newHeadPos);
            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                player1.Play();
                GameOver = true;
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else if (hit == GridValue.Food)
            {
                player2.Play();
                AddHead(newHeadPos);
                Score += 5;
                AddFood();
            }
        }

    }
}
