//------------------------------------------------------------
// <copyright file="RuleBook.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the RuleBook class.</summary>
//------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="RuleBook"/> class, used to determine
    /// the legal moves of different types of chess pieces.
    /// </summary>
    public class RuleBook : IChessPieceVisitor
    {
        /// <summary>
        /// The <see cref="ChessBoard"/> as a reference for field availability.
        /// </summary>
        private readonly ChessBoard chessBoard;

        /// <summary>
        /// Initialises a new instance of the <see cref="RuleBook"/> class.
        /// </summary>
        /// <param name="chessBoard">The <see cref="ChessBoard"/> as a reference for field availability.</param>
        public RuleBook(ChessBoard chessBoard)
        {
            this.chessBoard = chessBoard ?? throw new ArgumentNullException("The specified value cannot be null.");
            this.LegalMoves = Enumerable.Empty<Field>();
        }

        /// <summary>
        /// Gets the legal moves of the last visited <see cref="ChessPiece"/>. Default value is empty.
        /// </summary>
        /// <value>The legal moves of the last visited <see cref="ChessPiece"/>.</value>
        public IEnumerable<Field> LegalMoves { get; private set; }

        /// <summary>
        /// Visits a <see cref="King"/> and retrieves its legal moves.
        /// </summary>
        /// <param name="king">The <see cref="King"/> to be visited.</param>
        public void Visit(King king)
        {
            if (!this.chessBoard.OccupiedFields.Any())
            {
                return;
            }

            Field sourceField = this.chessBoard.OccupiedFields.FirstOrDefault(x => x.Value == king).Key;
            var restrictedFields = this.GetRestrictedFields(king);

            PlayerMovesCollector playerMovesCollector = new PlayerMovesCollector(this);
            playerMovesCollector.Player = king.Player;

            this.chessBoard.OccupiedFields.Values.ToList().ForEach(x => x.Accept(playerMovesCollector));

            // One field straight or diagonal.
            this.LegalMoves = (from row in Enumerable.Range(sourceField.Top - 1, 3)
                               from column in Enumerable.Range(sourceField.Left - 1, 3)
                               where row >= 0 && row < this.chessBoard.Height
                               where column >= 0 && column < this.chessBoard.Width
                               select new Field(column, row)).Except(restrictedFields).Except(playerMovesCollector.TotalLegalMoves);
        }

        /// <summary>
        /// Visits a <see cref="Queen"/> and retrieves its legal moves.
        /// </summary>
        /// <param name="queen">The <see cref="Queen"/> to be visited.</param>
        public void Visit(Queen queen)
        {
            if (!this.chessBoard.OccupiedFields.Any())
            {
                return;
            }

            Field sourceField = this.chessBoard.OccupiedFields.FirstOrDefault(x => x.Value == queen).Key;
            var restrictedFields = this.GetRestrictedFields(queen);
            var enemyFields = this.GetEnemyFields(queen);

            // Straight or diagonal as long as no figure is blocking.
            this.LegalMoves = from row in Enumerable.Range(0, this.chessBoard.Height)
                              from column in Enumerable.Range(0, this.chessBoard.Width)
                              where row == sourceField.Top || column == sourceField.Left
                              || Math.Abs(sourceField.Left - column) == Math.Abs(sourceField.Top - row)
                              select new Field(column, row);
            this.LegalMoves = this.RemoveBlockedFields(this.LegalMoves, sourceField, restrictedFields, enemyFields);
        }

        /// <summary>
        /// Visits a <see cref="Bishop"/> and retrieves its legal moves.
        /// </summary>
        /// <param name="bishop">The <see cref="Bishop"/> to be visited.</param>
        public void Visit(Bishop bishop)
        {
            if (!this.chessBoard.OccupiedFields.Any())
            {
                return;
            }

            Field sourceField = this.chessBoard.OccupiedFields.FirstOrDefault(x => x.Value == bishop).Key;
            var restrictedFields = this.GetRestrictedFields(bishop);
            var enemyFields = this.GetEnemyFields(bishop);

            // Diagonal as long as no figure is blocking.
            this.LegalMoves = (from row in Enumerable.Range(0, this.chessBoard.Height)
                               from column in Enumerable.Range(0, this.chessBoard.Width)
                               where Math.Abs(sourceField.Left - column) == Math.Abs(sourceField.Top - row)
                               select new Field(column, row)).Except(restrictedFields);
            this.LegalMoves = this.RemoveDiagonalBlockedFields(this.LegalMoves, sourceField, restrictedFields, enemyFields);
        }

        /// <summary>
        /// Visits a <see cref="Rook"/> and retrieves its legal moves.
        /// </summary>
        /// <param name="rook">The <see cref="Rook"/> to be visited.</param>
        public void Visit(Rook rook)
        {
            if (!this.chessBoard.OccupiedFields.Any())
            {
                return;
            }

            Field sourceField = this.chessBoard.OccupiedFields.FirstOrDefault(x => x.Value == rook).Key;
            var restrictedFields = this.GetRestrictedFields(rook);
            var enemyFields = this.GetEnemyFields(rook);

            // Straight as long as no figure is blocking.
            this.LegalMoves = (from row in Enumerable.Range(0, this.chessBoard.Height)
                               from column in Enumerable.Range(0, this.chessBoard.Width)
                               where row == sourceField.Top || column == sourceField.Left
                               select new Field(column, row)).Except(restrictedFields);
            this.LegalMoves = this.RemoveStraightBlockedFields(this.LegalMoves, sourceField, restrictedFields, enemyFields);
        }

        /// <summary>
        /// Visits a <see cref="Knight"/> and retrieves its legal moves.
        /// </summary>
        /// <param name="knight">The <see cref="Knight"/> to be visited.</param>
        public void Visit(Knight knight)
        {
            if (!this.chessBoard.OccupiedFields.Any())
            {
                return;
            }

            Field sourceField = this.chessBoard.OccupiedFields.FirstOrDefault(x => x.Value == knight).Key;
            var restrictedFields = this.GetRestrictedFields(knight);
            var offsets = new int[] { 1, -1, 2, -2 };

            // Two fields straight then one two the side (moving in "L" shapes). Can jump over friendly chess pieces.
            this.LegalMoves = (from rowJump in offsets
                               from columnJump in offsets
                               where Math.Abs(rowJump) != Math.Abs(columnJump)
                               where sourceField.Left + columnJump >= 0 && sourceField.Left + columnJump < this.chessBoard.Width
                               where sourceField.Top + rowJump >= 0 && sourceField.Top + rowJump < this.chessBoard.Height
                               select new Field(sourceField.Left + columnJump, sourceField.Top + rowJump)).Except(restrictedFields);
        }

        /// <summary>
        /// Visits a <see cref="Pawn"/> and retrieves its legal moves.
        /// </summary>
        /// <param name="pawn">The <see cref="Pawn"/> to be visited.</param>
        public void Visit(Pawn pawn)
        {
            if (!this.chessBoard.OccupiedFields.Any())
            {
                return;
            }

            Field sourceField = this.chessBoard.OccupiedFields.FirstOrDefault(x => x.Value == pawn).Key;
            int moveOffset = this.GetMoveOffset(pawn.Player.FacingDirection);

            if (sourceField.Top + moveOffset < 0
                || sourceField.Top + moveOffset >= this.chessBoard.Height)
            {
                this.LegalMoves = Enumerable.Empty<Field>();
                return;
            }

            var restrictedFields = this.GetRestrictedFields(pawn);
            var enemyFields = this.GetEnemyFields(pawn);

            // Move: One field forward.
            this.LegalMoves = new Field[0];
            var straightField = new Field(sourceField.Left, sourceField.Top + moveOffset);

            if (!restrictedFields.Contains(straightField) && !enemyFields.Contains(straightField))
            {
                this.LegalMoves = this.LegalMoves.Append(straightField);
            }

            // Beat: One field diagonally forward.
            var diagonalFields = this.GetPawnBeatFields(sourceField, moveOffset);

            this.LegalMoves = this.LegalMoves.Concat(from enemyField in enemyFields
                                   from diagonalField in diagonalFields
                                   where enemyField == diagonalField
                                   select diagonalField);
        }

        /// <summary>
        /// Gets the row position offset based on the facing <see cref="Direction"/>.
        /// </summary>
        /// <param name="facingDirection">The facing <see cref="Direction"/>.</param>
        /// <returns>The row position offset. Either 1 or -1.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Is raised when the <see cref="Direction"/> is neither north nor south.</exception>
        private int GetMoveOffset(Direction facingDirection)
        {
            switch (facingDirection)
            {
                case Direction.North:
                    return 1;
                case Direction.South:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException($"The specified direction must be either {Direction.North} or {Direction.South}.");
            }
        }

        /// <summary>
        /// Gets the diagonal fields that a pawn can beat.
        /// </summary>
        /// <param name="sourceField">The reference field for the calculation.</param>
        /// <param name="moveOffset">The move offset indicating in which direction the pawn can move.</param>
        /// <returns>The collection of <see cref="Field"/> objects as an <see cref="IEnumerable{Field}"/>.</returns>
        private IEnumerable<Field> GetPawnBeatFields(Field sourceField, int moveOffset)
        {
            var diagonalFields = Enumerable.Empty<Field>();

            if (sourceField.Left - 1 >= 0)
            {
                diagonalFields = diagonalFields.Append(new Field(sourceField.Left - 1, sourceField.Top + moveOffset));
            }

            if (sourceField.Left + 1 < this.chessBoard.Width)
            {
                diagonalFields = diagonalFields.Append(new Field(sourceField.Left + 1, sourceField.Top + moveOffset));
            }

            return diagonalFields;
        }

        /// <summary>
        /// Gets the <see cref="Field"/> objects where a <see cref="ChessPiece"/> can definitely not move to.
        /// </summary>
        /// <param name="chessPiece">The given <see cref="ChessPiece"/>.</param>
        /// <returns>The collection of <see cref="Field"/> objects as an <see cref="IEnumerable{Field}"/>.</returns>
        private IEnumerable<Field> GetRestrictedFields(ChessPiece chessPiece)
        {
            return from occupation in this.chessBoard.OccupiedFields
                   where occupation.Value.Player == chessPiece.Player
                   select occupation.Key;
        }

        /// <summary>
        /// Gets the <see cref="Field"/> objects where enemy <see cref="ChessPiece"/> objects are placed.
        /// </summary>
        /// <param name="chessPiece">The <see cref="ChessPiece"/> for determining the enemy <see cref="Player"/>.</param>
        /// <returns>The collection of <see cref="Field"/> objects as an <see cref="IEnumerable{Field}"/>.</returns>
        private IEnumerable<Field> GetEnemyFields(ChessPiece chessPiece)
        {
            return from occupation in this.chessBoard.OccupiedFields
                   where occupation.Value.Player != chessPiece.Player
                   select occupation.Key;
        }

        /// <summary>
        /// Removes all blocked fields in straight and diagonal directions.
        /// </summary>
        /// <param name="legalMoves">The general legal moves of a chess piece.</param>
        /// <param name="sourceField">The source field of the chess piece.</param>
        /// <param name="friendlyFields">The fields where friendly chess pieces reside.</param>
        /// <param name="enemyFields">The fields where enemy chess pieces reside.</param>
        /// <returns>The trimmed collection, where the blocked fields got removed.</returns>
        private IEnumerable<Field> RemoveBlockedFields(IEnumerable<Field> legalMoves, Field sourceField, IEnumerable<Field> friendlyFields, IEnumerable<Field> enemyFields)
        {
            var horizontalMoves = this.RemoveStraightBlockedFields(legalMoves, sourceField, friendlyFields, enemyFields);
            var diagonalMoves = this.RemoveDiagonalBlockedFields(legalMoves, sourceField, friendlyFields, enemyFields);

            return horizontalMoves.Concat(diagonalMoves);
        }

        /// <summary>
        /// Removes all blocked fields in straight directions.
        /// </summary>
        /// <param name="legalMoves">The general legal moves of a chess piece.</param>
        /// <param name="sourceField">The source field of the chess piece.</param>
        /// <param name="friendlyFields">The fields where friendly chess pieces reside.</param>
        /// <param name="enemyFields">The fields where enemy chess pieces reside.</param>
        /// <returns>The trimmed collection, where the blocked fields got removed.</returns>
        private IEnumerable<Field> RemoveStraightBlockedFields(
                                        IEnumerable<Field> legalMoves,
                                        Field sourceField,
                                        IEnumerable<Field> friendlyFields,
                                        IEnumerable<Field> enemyFields)
        {
            var friendlyBlockingFields = friendlyFields.Intersect(legalMoves);
            var enemyBlockingFields = enemyFields.Intersect(legalMoves);
            List<Field> validMoves = new List<Field>();

            // North side.
            for (int i = sourceField.Top + 1; i < this.chessBoard.Height; i++)
            {
                Field nextField = new Field(sourceField.Left, i);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }

                if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            // East side.
            for (int i = sourceField.Left + 1; i < this.chessBoard.Width; i++)
            {
                Field nextField = new Field(i, sourceField.Top);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }

                if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            // South side.
            for (int i = sourceField.Top - 1; i >= 0; i--)
            {
                Field nextField = new Field(sourceField.Left, i);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }

                if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            // West side.
            for (int i = sourceField.Left - 1; i >= 0; i--)
            {
                Field nextField = new Field(i, sourceField.Top);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }

                if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            return validMoves;
        }

        /// <summary>
        /// Removes all blocked fields in diagonal directions.
        /// </summary>
        /// <param name="legalMoves">The general legal moves of a chess piece.</param>
        /// <param name="sourceField">The source field of the chess piece.</param>
        /// <param name="friendlyFields">The fields where friendly chess pieces reside.</param>
        /// <param name="enemyFields">The fields where enemy chess pieces reside.</param>
        /// <returns>The trimmed collection, where the blocked fields got removed.</returns>
        private IEnumerable<Field> RemoveDiagonalBlockedFields(
                                        IEnumerable<Field> legalMoves,
                                        Field sourceField,
                                        IEnumerable<Field> friendlyFields,
                                        IEnumerable<Field> enemyFields)
        {
            var friendlyBlockingFields = friendlyFields.Intersect(legalMoves);
            var enemyBlockingFields = enemyFields.Intersect(legalMoves);
            List<Field> validMoves = new List<Field>();

            // North-east side.
            for (int i = 1; true; i++)
            {
                if (sourceField.Left + i > this.chessBoard.Width || sourceField.Top + i > this.chessBoard.Height)
                {
                    break;
                }

                Field nextField = new Field(sourceField.Left + i, sourceField.Top + i);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            // South-east side.
            for (int i = 1; true; i++)
            {
                if (sourceField.Left + i > this.chessBoard.Width || sourceField.Top - i < 0)
                {
                    break;
                }

                Field nextField = new Field(sourceField.Left + i, sourceField.Top - i);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            // South-west side.
            for (int i = 1; true; i++)
            {
                if (sourceField.Left - i < 0 || sourceField.Top - i < 0)
                {
                    break;
                }

                Field nextField = new Field(sourceField.Left - i, sourceField.Top - i);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            // North-west side.
            for (int i = 1; true; i++)
            {
                if (sourceField.Left - i < 0 || sourceField.Top + i > this.chessBoard.Height)
                {
                    break;
                }

                Field nextField = new Field(sourceField.Left - i, sourceField.Top + i);

                if (!legalMoves.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && friendlyBlockingFields.Contains(nextField))
                {
                    break;
                }
                else if (legalMoves.Contains(nextField) && enemyBlockingFields.Contains(nextField))
                {
                    validMoves.Add(nextField);
                    break;
                }
                else
                {
                    validMoves.Add(nextField);
                }
            }

            return validMoves;
        }
    }
}