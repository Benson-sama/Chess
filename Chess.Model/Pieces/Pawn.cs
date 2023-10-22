//-----------------------------------------------------------
// <copyright file="Pawn.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Pawn class.</summary>
//-----------------------------------------------------------

using Chess.Model.Utility;

namespace Chess.Model.Pieces;

/// <summary>
/// Represents the <see cref="Pawn"/> class.
/// </summary>
public class Pawn : ChessPiece
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Pawn"/> class.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> for this <see cref="Pawn"/>.</param>
    public Pawn(Player player) : base(player)
    {
    }

    /// <summary>
    /// Accepts an <see cref="IChessPieceVisitor"/> in order to visit him.
    /// </summary>
    /// <param name="chessPieceVisitor">The <see cref="IChessPieceVisitor"/> to be visited.</param>
    public override void Accept(IChessPieceVisitor chessPieceVisitor)
        => chessPieceVisitor.Visit(this);
}
