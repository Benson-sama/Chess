﻿//-----------------------------------------------------------
// <copyright file="King.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the King class.</summary>
//-----------------------------------------------------------

using Chess.Model.Utility;

namespace Chess.Model.Pieces;

/// <summary>
/// Represents the <see cref="King"/> class.
/// </summary>
public class King : ChessPiece
{
    /// <summary>
    /// Initialises a new instance of the <see cref="King"/> class.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> for this <see cref="King"/>.</param>
    public King(Player player) : base(player)
    {
    }

    /// <summary>
    /// Accepts an <see cref="IChessPieceVisitor"/> in order to visit him.
    /// </summary>
    /// <param name="chessPieceVisitor">The <see cref="IChessPieceVisitor"/> to be visited.</param>
    public override void Accept(IChessPieceVisitor chessPieceVisitor)
        => chessPieceVisitor.Visit(this);
}
