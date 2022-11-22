//-----------------------------------------------------------
// <copyright file="Rook.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Rook class.</summary>
//-----------------------------------------------------------
namespace Chess.Model.ChessPieces
{
    /// <summary>
    /// Represents the <see cref="Rook"/> class.
    /// </summary>
    public class Rook : ChessPiece
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Rook"/> class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> for this <see cref="Rook"/>.</param>
        public Rook(Player player) : base(player)
        {
        }

        /// <summary>
        /// Accepts an <see cref="IChessPieceVisitor"/> in order to visit him.
        /// </summary>
        /// <param name="chessPieceVisitor">The <see cref="IChessPieceVisitor"/> to be visited.</param>
        public override void Accept(IChessPieceVisitor chessPieceVisitor)
        {
            chessPieceVisitor.Visit(this);
        }
    }
}
