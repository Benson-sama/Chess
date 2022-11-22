//---------------------------------------------------------------------
// <copyright file="ChessPieceVisitor.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPieceVisitor class.</summary>
//---------------------------------------------------------------------
namespace Chess.ViewModel
{
    using System;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessPieceVisitor"/> class.
    /// </summary>
    public class ChessPieceVisitor : IChessPieceVisitor
    {
        /// <summary>
        /// Gets the image path for the chess piece that has been visited.
        /// </summary>
        /// <value>The image path for the chess piece that has been visited.
        /// Null if no chess piece has been visited.</value>
        public string ImagePath { get; private set; }

        /// <summary>
        /// Visits a <see cref="King"/> to get the corresponding image path.
        /// </summary>
        /// <param name="king">The <see cref="King"/> to be visited.</param>
        public void Visit(King king)
        {
            switch (king.Player.Colour.ToLower())
            {
                case "black":
                    this.ImagePath = @".\Images\ChessPieces\PNG\King-black.png";
                    break;
                case "white":
                    this.ImagePath = @".\Images\ChessPieces\PNG\King-white.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("The colour of the chess piece must be black or white.");
            }
        }

        /// <summary>
        /// Visits a <see cref="Queen"/> to get the corresponding image path.
        /// </summary>
        /// <param name="queen">The <see cref="Queen"/> to be visited.</param>
        public void Visit(Queen queen)
        {
            switch (queen.Player.Colour.ToLower())
            {
                case "black":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Queen-black.png";
                    break;
                case "white":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Queen-white.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("The colour of the chess piece must be black or white.");
            }
        }

        /// <summary>
        /// Visits a <see cref="Bishop"/> to get the corresponding image path.
        /// </summary>
        /// <param name="bishop">The <see cref="Bishop"/> to be visited.</param>
        public void Visit(Bishop bishop)
        {
            switch (bishop.Player.Colour.ToLower())
            {
                case "black":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Bishop-black.png";
                    break;
                case "white":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Bishop-white.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("The colour of the chess piece must be black or white.");
            }
        }

        /// <summary>
        /// Visits a <see cref="Rook"/> to get the corresponding image path.
        /// </summary>
        /// <param name="rook">The <see cref="Rook"/> to be visited.</param>
        public void Visit(Rook rook)
        {
            switch (rook.Player.Colour.ToLower())
            {
                case "black":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Rook-black.png";
                    break;
                case "white":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Rook-white.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("The colour of the chess piece must be black or white.");
            }
        }

        /// <summary>
        /// Visits a <see cref="King"/> to get the corresponding image path.
        /// </summary>
        /// <param name="knight">The <see cref="King"/> to be visited.</param>
        public void Visit(Knight knight)
        {
            switch (knight.Player.Colour.ToLower())
            {
                case "black":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Knight-black.png";
                    break;
                case "white":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Knight-white.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("The colour of the chess piece must be black or white.");
            }
        }

        /// <summary>
        /// Visits a <see cref="Pawn"/> to get the corresponding image path.
        /// </summary>
        /// <param name="pawn">The <see cref="Pawn"/> to be visited.</param>
        public void Visit(Pawn pawn)
        {
            switch (pawn.Player.Colour.ToLower())
            {
                case "black":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Pawn-black.png";
                    break;
                case "white":
                    this.ImagePath = @".\Images\ChessPieces\PNG\Pawn-white.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("The colour of the chess piece must be black or white.");
            }
        }
    }
}
