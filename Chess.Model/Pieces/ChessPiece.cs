//--------------------------------------------------------------
// <copyright file="ChessPiece.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPiece class.</summary>
//--------------------------------------------------------------

using Chess.Model.Utility;

namespace Chess.Model.Pieces;

/// <summary>
/// Represents the <see cref="ChessPiece"/> record.
/// </summary>
public abstract class ChessPiece
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ChessPiece"/> class.
    /// </summary>
    /// <param name="player">The <see cref="Utility.Player"/> for this <see cref="ChessPiece"/>.</param>
    public ChessPiece(Player player)
        => Player = player;

    /// <summary>
    /// Gets the <see cref="Utility.Player"/> of this <see cref="ChessPiece"/>.
    /// </summary>
    public Player Player { get; init; }

    /// <summary>
    /// Accepts a visitor in order to call it back.
    /// </summary>
    /// <param name="chessPieceVisitor">The <see cref="IChessPieceVisitor"/> to be accepted.</param>
    public abstract void Accept(IChessPieceVisitor chessPieceVisitor);
}
