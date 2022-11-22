//--------------------------------------------------------------
// <copyright file="ChessBoard.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessBoard class.</summary>
//--------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessBoard"/> class.
    /// </summary>
    public class ChessBoard
    {
        /// <summary>
        /// The height of the <see cref="ChessBoard"/>.
        /// </summary>
        private int height;

        /// <summary>
        /// The width of the <see cref="ChessBoard"/>.
        /// </summary>
        private int width;

        /// <summary>
        /// The dictionary used to place the chess pieces onto the board.
        /// </summary>
        private Dictionary<Field, ChessPiece> occupiedFields;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessBoard"/> class.
        /// </summary>
        /// <param name="parameters">The <see cref="ChessBoardParameters"/>
        /// containing the height and width of the <see cref="ChessBoard"/>.</param>
        public ChessBoard(ChessBoardParameters parameters)
        {
            this.Width = parameters.Width;
            this.Height = parameters.Height;
            this.occupiedFields = new Dictionary<Field, ChessPiece>();
        }

        /// <summary>
        /// Gets the height of the <see cref="ChessBoard"/>.
        /// </summary>
        /// <value>The height of the <see cref="ChessBoard"/>.</value>
        public int Height
        {
            get => this.height;

            private set
            {
                if (value < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified height of the chess board cannot be less than 8");
                }

                this.height = value;
            }
        }

        /// <summary>
        /// Gets the width of the <see cref="ChessBoard"/>.
        /// </summary>
        /// <value>The width of the <see cref="ChessBoard"/>.</value>
        public int Width
        {
            get => this.width;

            private set
            {
                if (value < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified width of the chess board cannot be less than 8");
                }

                this.width = value;
            }
        }

        /// <summary>
        /// Gets the occupied fields of the <see cref="ChessBoard"/>.
        /// </summary>
        /// <value>The occupied fields of the <see cref="ChessBoard"/>.</value>
        public Dictionary<Field, ChessPiece> OccupiedFields
        {
            get => this.occupiedFields;

            private set
            {
                this.occupiedFields = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Removes a given <see cref="ChessPiece"/> from the board if possible.
        /// </summary>
        /// <param name="chessPiece">The given <see cref="ChessPiece"/> to remove.</param>
        public void Remove(ChessPiece chessPiece)
        {
            var targetOccupation = this.OccupiedFields.FirstOrDefault(x => x.Value == chessPiece);

            if (targetOccupation.Key == null)
            {
                return;
            }

            this.OccupiedFields.Remove(targetOccupation.Key);
        }

        /// <summary>
        /// Moves a given ChessPiece from one field to another if possible.
        /// </summary>
        /// <param name="chessPiece">The <see cref="Field"/> of which the <see cref="ChessPiece"/> is tried to be moved.</param>
        /// <param name="destination">The <see cref="Field"/> where the <see cref="ChessPiece"/> is trying to get moved to.</param>
        public void Move(ChessPiece chessPiece, Field destination)
        {
            if (this.OccupiedFields.ContainsKey(destination))
            {
                return;
            }

            this.OccupiedFields.Remove(this.OccupiedFields.FirstOrDefault(x => x.Value == chessPiece).Key);
            this.OccupiedFields.Add(destination, chessPiece);
        }

        /// <summary>
        /// Gets the <see cref="ChessPiece"/> occupying the given <see cref="Field"/>.
        /// </summary>
        /// <param name="field">The given <see cref="Field"/>.</param>
        /// <returns>The <see cref="ChessPiece"/> or null if the <see cref="Field"/> is empty.</returns>
        public ChessPiece GetChessPiece(Field field)
        {
            return (from entry in this.OccupiedFields
                    where entry.Key == field
                    select entry.Value).FirstOrDefault();
        }

        /// <summary>
        /// Places a given <see cref="ChessPiece"/> on this <see cref="ChessBoard"/>.
        /// </summary>
        /// <param name="chessPiece">The <see cref="ChessPiece"/> to be placed.</param>
        /// <param name="left">The desired column position.</param>
        /// <param name="top">The desired row position.</param>
        public void Place(ChessPiece chessPiece, int left, int top)
        {
            if (this.OccupiedFields.Values.Contains(chessPiece))
            {
                throw new InvalidOperationException("Cannot place a chess piece several times on one board.");
            }

            Field desiredField = new Field(left, top);
            this.Validate(desiredField);

            this.occupiedFields.Add(new Field(left, top), chessPiece);
        }

        /// <summary>
        /// Checks if a <see cref="Field"/> is inside the width and height of the <see cref="ChessBoard"/>.
        /// </summary>
        /// <param name="field">The field to be checked.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Is raised when the field is outside this chessboards limits.
        /// </exception>
        private void Validate(Field field)
        {
            if (field.Left > this.Width || field.Left < 0
                || field.Top > this.Height || field.Top < 0)
            {
                throw new ArgumentOutOfRangeException("The specified field must be within chess board limits.");
            }
        }
    }
}
