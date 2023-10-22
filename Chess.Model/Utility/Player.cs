//-----------------------------------------------------------
// <copyright file="Player.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Player class.</summary>
//-----------------------------------------------------------

namespace Chess.Model.Utility;

/// <summary>
/// Represents the <see cref="Player"/> class.
/// </summary>
public class Player
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <param name="facingDirection">The facing direction.</param>
    public Player(Direction facingDirection)
     => FacingDirection = facingDirection;

    /// <summary>
    /// Gets the <see cref="Direction"/> that this <see cref="Player"/> is facing in.
    /// </summary>
    /// <value>The <see cref="Direction"/> that this <see cref="Player"/> is facing in.</value>
    public Direction FacingDirection { get; init; }
}
