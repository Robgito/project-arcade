using System;
using System.Collections.Generic;

namespace Project_3___Arcade
{
    public class Position1S
    {
        public int Row { get; }
        public int Column { get; }

        public Position1S(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Position1S Translate(Direction direction)
        {
            return new Position1S(Row + direction.RowOffset, Column + direction.ColumnOffset);
        }

        public override bool Equals(object obj)
        {
            return obj is Position1S position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position1S left, Position1S right)
        {
            return EqualityComparer<Position1S>.Default.Equals(left, right);
        }

        public static bool operator !=(Position1S left, Position1S right)
        {
            return !(left == right);
        }
    }
}
