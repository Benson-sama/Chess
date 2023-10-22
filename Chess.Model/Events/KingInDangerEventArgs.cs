//-------------------------------------------------------------------------
// <copyright file="KingInDangerEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the KingInDangerEventArgs class.</summary>
//-------------------------------------------------------------------------

namespace Chess.Model.Events;

using System;
using Chess.Model.Pieces;

/// <summary>
/// Represents the <see cref="KingInDangerEventArgs"/> class.
/// </summary>
public class KingInDangerEventArgs : EventArgs
{
    /// <summary>
    /// Initialises a new instance of the <see cref="KingInDangerEventArgs"/> class.
    /// </summary>
    /// <param name="king">The <see cref="ChessPieces.King"/> of this <see cref="KingInDangerEventArgs"/>.</param>
    /// <param name="isInDanger">The value indicating whether the <see cref="ChessPieces.King"/> is in danger.</param>
    public KingInDangerEventArgs(King king, bool isInDanger)
        => (King, IsInDanger) = (king, isInDanger);

    /// <summary>
    /// Gets the <see cref="ChessPieces.King"/> of this <see cref="KingInDangerEventArgs"/>.
    /// </summary>
    /// <value>The <see cref="ChessPieces.King"/> of this <see cref="KingInDangerEventArgs"/>.</value>
    public King King { get; init; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="ChessPieces.King"/> is in danger.
    /// </summary>
    /// <value>The value indicating whether the <see cref="ChessPieces.King"/> is in danger.</value>
    public bool IsInDanger { get; init; }
}