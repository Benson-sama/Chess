//-------------------------------------------------------------
// <copyright file="ChessMove.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessMove class.</summary>
//-------------------------------------------------------------

namespace Chess.Model;

using System;
using System.Xml.Serialization;
using Chess.Model.Pieces;
using Chess.Model.Utility;

/// <summary>
/// Represents the <see cref="ChessMove"/> record.
/// </summary>
[Serializable]
public record ChessMove
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ChessMove"/> record.
    /// </summary>
    /// <param name="from">The source <see cref="Field"/>.</param>
    /// <param name="to">The destination <see cref="Field"/>.</param>
    /// <param name="beatenChessPiece">The beaten <see cref="ChessPiece"/>.</param>
    public ChessMove(Field from, Field to, ChessPiece? beatenChessPiece = null)
    {
        From = from;
        To = to;
        BeatenChessPiece = beatenChessPiece;
    }

    /// <summary>
    /// Gets or sets the source <see cref="Field"/>.
    /// </summary>
    /// <value>The source <see cref="Field"/>.</value>
    public Field From { get; init; }

    /// <summary>
    /// Gets or sets the destination <see cref="Field"/>.
    /// </summary>
    /// <value>The destination <see cref="Field"/>.</value>
    public Field To { get; init; }

    /// <summary>
    /// Gets or sets the beaten <see cref="ChessPiece"/>.
    /// </summary>
    /// <value>The beaten <see cref="ChessPiece"/>.</value>
    [XmlIgnore]
    public ChessPiece? BeatenChessPiece { get; init; }

    /// <summary>
    /// Creates a string representation of this <see cref="ChessMove"/>.
    /// </summary>
    /// <returns>A string representing this <see cref="ChessMove"/>.</returns>
    public override string ToString() => $"{From} -> {To}";
}
