//-----------------------------------------------------------
// <copyright file="Bishop.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Bishop class.</summary>
//-----------------------------------------------------------

using Chess.Model.Utility;

namespace Chess.Model.Pieces;

/// <summary>
/// Represents the <see cref="Bishop"/> record.
/// </summary>
public class Bishop : ChessPiece
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Bishop"/> record.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> for this <see cref="Bishop"/>.</param>
    public Bishop(Player player) : base(player)
    {
    }

    /// <summary>
    /// Accepts an <see cref="IChessPieceVisitor"/> in order to visit him.
    /// </summary>
    /// <param name="chessPieceVisitor">The <see cref="IChessPieceVisitor"/> to be visited.</param>
    public override void Accept(IChessPieceVisitor chessPieceVisitor)
        => chessPieceVisitor.Visit(this);
}
