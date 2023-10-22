//--------------------------------------------------------------------------
// <copyright file="StatusUpdatedEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the StatusUpdatedEventArgs class.</summary>
//--------------------------------------------------------------------------

namespace Chess.Model.Events;

using System;
using Chess.Model;

/// <summary>
/// Represents the <see cref="StatusUpdatedEventArgs"/> class.
/// </summary>
public class StatusUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Initialises a new instance of the <see cref="StatusUpdatedEventArgs"/> class.
    /// </summary>
    /// <param name="status">The status of this <see cref="StatusUpdatedEventArgs"/>.</param>
    public StatusUpdatedEventArgs(ChessGameStatus status) => Status = status;

    /// <summary>
    /// Gets the <see cref="ChessGameStatus"/> of this <see cref="StatusUpdatedEventArgs"/>.
    /// </summary>
    /// <value>The <see cref="ChessGameStatus"/> of this <see cref="StatusUpdatedEventArgs"/>.</value>
    public ChessGameStatus Status { get; init; }
}
