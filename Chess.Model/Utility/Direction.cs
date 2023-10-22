//-------------------------------------------------------------
// <copyright file="Direction.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Direction enumeration.</summary>
//-------------------------------------------------------------

namespace Chess.Model.Utility;

/// <summary>
/// Represents the <see cref="Direction"/> enumeration.
/// </summary>
public enum Direction
{
    /// <summary>
    /// The north side.
    /// </summary>
    North = 0,

    /// <summary>
    /// The east side.
    /// </summary>
    East = 90,

    /// <summary>
    /// The south side.
    /// </summary>
    South = 180,

    /// <summary>
    /// The west side.
    /// </summary>
    West = 270
}
