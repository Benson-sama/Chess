//-------------------------------------------------------------------
// <copyright file="ChessGameStatus.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameStatus class.</summary>
//-------------------------------------------------------------------

namespace Chess.Model;

/// <summary>
/// Represents the <see cref="ChessGameStatus"/> enumeration.
/// </summary>
public enum ChessGameStatus
{
    /// <summary>
    /// The player with black chess pieces is active.
    /// </summary>
    BlackActive,

    /// <summary>
    /// The player with white chess pieces is active.
    /// </summary>
    WhiteActive,

    /// <summary>
    /// The player with black chess pieces won.
    /// </summary>
    BlackWon,

    /// <summary>
    /// The player with white chess pieces won.
    /// </summary>
    WhiteWon,

    /// <summary>
    /// The game has ended in a draw.
    /// </summary>
    Draw
}
