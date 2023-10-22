//---------------------------------------------------------------------
// <copyright file="ChessPieceVisitor.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPieceVisitor class.</summary>
//---------------------------------------------------------------------

namespace Chess.WPF.Converter;

using System;
using Chess.Model.Pieces;

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
    public string? ImagePath { get; private set; }

    /// <summary>
    /// Visits a <see cref="King"/> to get the corresponding image path.
    /// </summary>
    /// <param name="king">The <see cref="King"/> to be visited.</param>
    public void Visit(King king)
    {
        ImagePath = king.Player.FacingDirection switch
        {
            Model.Utility.Direction.North => @".\Images\ChessPieces\PNG\King-black.png",
            Model.Utility.Direction.South => @".\Images\ChessPieces\PNG\King-white.png",
            _ => throw new ArgumentOutOfRangeException(nameof(king), "Unable to determine the colour."),
        };
    }

    /// <summary>
    /// Visits a <see cref="Queen"/> to get the corresponding image path.
    /// </summary>
    /// <param name="queen">The <see cref="Queen"/> to be visited.</param>
    public void Visit(Queen queen)
    {
        ImagePath = queen.Player.FacingDirection switch
        {
            Model.Utility.Direction.North => @".\Images\ChessPieces\PNG\Queen-black.png",
            Model.Utility.Direction.South => @".\Images\ChessPieces\PNG\Queen-white.png",
            _ => throw new ArgumentOutOfRangeException(nameof(queen), "Unable to determine the colour."),
        };
    }

    /// <summary>
    /// Visits a <see cref="Bishop"/> to get the corresponding image path.
    /// </summary>
    /// <param name="bishop">The <see cref="Bishop"/> to be visited.</param>
    public void Visit(Bishop bishop)
    {
        ImagePath = bishop.Player.FacingDirection switch
        {
            Model.Utility.Direction.North => @".\Images\ChessPieces\PNG\Bishop-black.png",
            Model.Utility.Direction.South => @".\Images\ChessPieces\PNG\Bishop-white.png",
            _ => throw new ArgumentOutOfRangeException(nameof(bishop), "Unable to determine the colour."),
        };
    }

    /// <summary>
    /// Visits a <see cref="Rook"/> to get the corresponding image path.
    /// </summary>
    /// <param name="rook">The <see cref="Rook"/> to be visited.</param>
    public void Visit(Rook rook)
    {
        ImagePath = rook.Player.FacingDirection switch
        {
            Model.Utility.Direction.North => @".\Images\ChessPieces\PNG\Rook-black.png",
            Model.Utility.Direction.South => @".\Images\ChessPieces\PNG\Rook-white.png",
            _ => throw new ArgumentOutOfRangeException(nameof(rook), "Unable to determine the colour."),
        };
    }

    /// <summary>
    /// Visits a <see cref="King"/> to get the corresponding image path.
    /// </summary>
    /// <param name="knight">The <see cref="King"/> to be visited.</param>
    public void Visit(Knight knight)
    {
        ImagePath = knight.Player.FacingDirection switch
        {
            Model.Utility.Direction.North => @".\Images\ChessPieces\PNG\Knight-black.png",
            Model.Utility.Direction.South => @".\Images\ChessPieces\PNG\Knight-white.png",
            _ => throw new ArgumentOutOfRangeException(nameof(knight), "Unable to determine the colour."),
        };
    }

    /// <summary>
    /// Visits a <see cref="Pawn"/> to get the corresponding image path.
    /// </summary>
    /// <param name="pawn">The <see cref="Pawn"/> to be visited.</param>
    public void Visit(Pawn pawn)
    {
        ImagePath = pawn.Player.FacingDirection switch
        {
            Model.Utility.Direction.North => @".\Images\ChessPieces\PNG\Pawn-black.png",
            Model.Utility.Direction.South => @".\Images\ChessPieces\PNG\Pawn-white.png",
            _ => throw new ArgumentOutOfRangeException(nameof(pawn), "Unable to determine the colour."),
        };
    }
}
