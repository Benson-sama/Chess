//----------------------------------------------------------------------------
// <copyright file="ChessPieceMovedEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPieceMovedEventArgs class.</summary>
//----------------------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessPieceMovedEventArgs"/> class.
    /// </summary>
    public class ChessPieceMovedEventArgs : EventArgs
    {
        /// <summary>
        /// The moved <see cref="ChessPiece"/>.
        /// </summary>
        private ChessPiece movedChessPiece;

        /// <summary>
        /// The destination <see cref="Field"/>.
        /// </summary>
        private Field destination;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessPieceMovedEventArgs"/> class.
        /// </summary>
        /// <param name="movedChessPiece">The <see cref="ChessPiece"/> that got moved.</param>
        /// <param name="destination">The destination <see cref="Field"/>.</param>
        public ChessPieceMovedEventArgs(ChessPiece movedChessPiece, Field destination)
        {
            this.MovedChessPiece = movedChessPiece;
            this.Destination = destination;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessPieceMovedEventArgs"/> class.
        /// </summary>
        /// <param name="movedChessPiece">The <see cref="ChessPiece"/> that got moved.</param>
        /// <param name="destination">The destination <see cref="Field"/>.</param>
        /// <param name="wasMoveRewind">The value indicating whether this move was for rewinding.</param>
        public ChessPieceMovedEventArgs(ChessPiece movedChessPiece, Field destination, bool wasMoveRewind) : this(movedChessPiece, destination)
        {
            this.WasMoveRewind = wasMoveRewind;
        }

        /// <summary>
        /// Gets a value indicating whether the move was a rewind.
        /// </summary>
        /// <value>The value indicating whether the move was a rewind.</value>
        public bool WasMoveRewind { get; private set; }

        /// <summary>
        /// Gets the moved <see cref="ChessPiece"/>.
        /// </summary>
        /// <value>The moved <see cref="ChessPiece"/>.</value>
        public ChessPiece MovedChessPiece
        {
            get => this.movedChessPiece;

            private set
            {
                this.movedChessPiece = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null");
            }
        }

        /// <summary>
        /// Gets the destination <see cref="Field"/>.
        /// </summary>
        /// <value>The destination <see cref="Field"/>.</value>
        public Field Destination
        {
            get => this.destination;

            private set
            {
                this.destination = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null");
            }
        }
    }
}
