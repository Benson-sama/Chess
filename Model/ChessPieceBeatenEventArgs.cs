//-----------------------------------------------------------------------------
// <copyright file="ChessPieceBeatenEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPieceBeatenEventArgs class.</summary>
//-----------------------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessPieceBeatenEventArgs"/> class.
    /// </summary>
    public class ChessPieceBeatenEventArgs : EventArgs
    {
        /// <summary>
        /// The beaten <see cref="ChessPiece"/>.
        /// </summary>
        private ChessPiece beatenChessPiece;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessPieceBeatenEventArgs"/> class.
        /// </summary>
        /// <param name="beatenChessPiece">The beaten <see cref="ChessPiece"/>.</param>
        public ChessPieceBeatenEventArgs(ChessPiece beatenChessPiece)
        {
            this.BeatenChessPiece = beatenChessPiece;
        }

        /// <summary>
        /// Gets the beaten <see cref="ChessPiece"/>.
        /// </summary>
        /// <value>The beaten <see cref="ChessPiece"/>.</value>
        public ChessPiece BeatenChessPiece
        {
            get => this.beatenChessPiece;

            private set
            {
                this.beatenChessPiece = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null");
            }
        }
    }
}
