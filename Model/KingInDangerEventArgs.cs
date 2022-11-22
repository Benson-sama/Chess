//-------------------------------------------------------------------------
// <copyright file="KingInDangerEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the KingInDangerEventArgs class.</summary>
//-------------------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="KingInDangerEventArgs"/> class.
    /// </summary>
    public class KingInDangerEventArgs : EventArgs
    {
        /// <summary>
        /// The <see cref="ChessPieces.King"/> of this <see cref="KingInDangerEventArgs"/>.
        /// </summary>
        private King king;

        /// <summary>
        /// Initialises a new instance of the <see cref="KingInDangerEventArgs"/> class.
        /// </summary>
        /// <param name="king">The <see cref="ChessPieces.King"/> of this <see cref="KingInDangerEventArgs"/>.</param>
        /// <param name="isInDanger">The value indicating whether the <see cref="ChessPieces.King"/> is in danger.</param>
        public KingInDangerEventArgs(King king, bool isInDanger)
        {
            this.King = king;
            this.IsInDanger = isInDanger;
        }

        /// <summary>
        /// Gets the <see cref="ChessPieces.King"/> of this <see cref="KingInDangerEventArgs"/>.
        /// </summary>
        /// <value>The <see cref="ChessPieces.King"/> of this <see cref="KingInDangerEventArgs"/>.</value>
        public King King
        {
            get => this.king;

            private set
            {
                this.king = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ChessPieces.King"/> is in danger.
        /// </summary>
        /// <value>The value indicating whether the <see cref="ChessPieces.King"/> is in danger.</value>
        public bool IsInDanger { get; private set; }
    }
}