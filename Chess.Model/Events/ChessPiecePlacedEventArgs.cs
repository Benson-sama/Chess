//-----------------------------------------------------------------------------
// <copyright file="ChessPiecePlacedEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPiecePlacedEventArgs class.</summary>
//-----------------------------------------------------------------------------

namespace Chess.Model.Events;

using System;
using Chess.Model.Pieces;
using Chess.Model.Utility;

/// <summary>
/// Represents the <see cref="ChessPiecePlacedEventArgs"/> class.
/// </summary>
public class ChessPiecePlacedEventArgs : EventArgs
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ChessPiecePlacedEventArgs"/> class.
    /// </summary>
    /// <param name="placedChessPiece">The placed <see cref="ChessPiece"/>.</param>
    /// <param name="destination">The destination <see cref="Field"/> of this placement.</param>
    public ChessPiecePlacedEventArgs(ChessPiece placedChessPiece, Field destination)
        => (PlacedChessPiece, Destination) = (placedChessPiece, destination);

    /// <summary>
    /// Gets the placed <see cref="ChessPiece"/>.
    /// </summary>
    /// <value>The placed <see cref="ChessPiece"/>.</value>
    public ChessPiece PlacedChessPiece { get; init; }

    /// <summary>
    /// Gets the destination <see cref="Field"/> of this placement.
    /// </summary>
    /// <value>The destination <see cref="Field"/> of this placement.</value>
    public Field Destination { get; init; }
}
