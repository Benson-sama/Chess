//-----------------------------------------------------------------
// <copyright file="ChessGameSave.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameSave class.</summary>
//-----------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the <see cref="ChessGameSave"/> class.
    /// </summary>
    [Serializable]
    public class ChessGameSave
    {
        /// <summary>
        /// The move list of this <see cref="ChessGameSave"/>.
        /// </summary>
        private List<ChessMove> moveList;

        /// <summary>
        /// The width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
        /// </summary>
        private int width;

        /// <summary>
        /// The height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
        /// </summary>
        private int height;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessGameSave"/> class.
        /// </summary>
        /// <param name="moveList">The move list of this <see cref="ChessGameSave"/>.</param>
        /// <param name="width">The width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</param>
        /// <param name="height">The height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</param>
        public ChessGameSave(List<ChessMove> moveList, int width, int height)
        {
            this.MoveList = moveList;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessGameSave"/> class.
        /// </summary>
        public ChessGameSave()
        {
        }

        /// <summary>
        /// Gets or sets the move list of this <see cref="ChessGameSave"/>.
        /// </summary>
        /// <value>The move list of this <see cref="ChessGameSave"/>.</value>
        public List<ChessMove> MoveList
        {
            get => this.moveList;

            set
            {
                this.moveList = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Gets or sets the height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
        /// </summary>
        /// <value>The height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</value>
        public int Height
        {
            get => this.height;

            set
            {
                if (value < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified height of the chess board cannot be less than 8");
                }

                this.height = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
        /// </summary>
        /// <value>The width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</value>
        public int Width
        {
            get => this.width;

            set
            {
                if (value < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified width of the chess board cannot be less than 8");
                }

                this.width = value;
            }
        }
    }
}
