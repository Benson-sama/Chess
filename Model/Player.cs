//-----------------------------------------------------------
// <copyright file="Player.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Player class.</summary>
//-----------------------------------------------------------
namespace Chess.Model
{
    using System;

    /// <summary>
    /// Represents the <see cref="Player"/> class.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The colour of the <see cref="Player"/>.
        /// </summary>
        private string colour;

        /// <summary>
        /// Initialises a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="colour">The colour for this <see cref="Player"/>.</param>
        /// <param name="facingDirection">The facing direction.</param>
        public Player(string colour, Direction facingDirection)
        {
            this.Colour = colour;
            this.FacingDirection = facingDirection;
        }

        /// <summary>
        /// Gets the colour of the <see cref="Player"/>.
        /// </summary>
        /// <value>The colour of the <see cref="Player"/>.</value>
        public string Colour
        {
            get => this.colour;

            private set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified value cannot be empty.");
                }

                this.colour = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="Direction"/> that this <see cref="Player"/> is facing in.
        /// </summary>
        /// <value>The <see cref="Direction"/> that this <see cref="Player"/> is facing in.</value>
        public Direction FacingDirection { get; private set; }
    }
}
