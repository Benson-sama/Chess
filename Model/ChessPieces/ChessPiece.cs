//--------------------------------------------------------------
// <copyright file="ChessPiece.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPiece class.</summary>
//--------------------------------------------------------------
namespace Chess.Model.ChessPieces
{
    /// <summary>
    /// Represents the <see cref="ChessPiece"/> class.
    /// </summary>
    public abstract class ChessPiece
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChessPiece"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Model.Player"/> for this <see cref="ChessPiece"/>.</param>
        public ChessPiece(Player player)
        {
            this.Player = player;
        }

        /// <summary>
        /// Gets or sets the <see cref="Model.Player"/> of the <see cref="ChessPiece"/>.
        /// </summary>
        /// <value>The <see cref="Model.Player"/> of the <see cref="ChessPiece"/>.</value>
        public Player Player
        {
            get;
            set;
        }

        /// <summary>
        /// Accepts a visitor in order to get something done.
        /// </summary>
        /// <param name="chessPieceVisitor">The <see cref="IChessPieceVisitor"/> to be accepted.</param>
        public abstract void Accept(IChessPieceVisitor chessPieceVisitor);
    }
}
