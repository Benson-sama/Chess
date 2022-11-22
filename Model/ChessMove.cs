//-------------------------------------------------------------
// <copyright file="ChessMove.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessMove class.</summary>
//-------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using System.Xml.Serialization;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessMove"/> class.
    /// </summary>
    [Serializable]
    public class ChessMove
    {
        /// <summary>
        /// The source <see cref="Field"/>.
        /// </summary>
        private Field from;

        /// <summary>
        /// The destination <see cref="Field"/>.
        /// </summary>
        private Field to;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessMove"/> class.
        /// </summary>
        public ChessMove()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessMove"/> class.
        /// </summary>
        /// <param name="from">The source <see cref="Field"/>.</param>
        /// <param name="to">The destination <see cref="Field"/>.</param>
        public ChessMove(Field from, Field to)
        {
            this.From = from;
            this.To = to;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessMove"/> class.
        /// </summary>
        /// <param name="from">The source <see cref="Field"/>.</param>
        /// <param name="to">The destination <see cref="Field"/>.</param>
        /// <param name="beatenChessPiece">The beaten <see cref="ChessPiece"/>.</param>
        public ChessMove(Field from, Field to, ChessPiece beatenChessPiece) : this(from, to)
        {
            this.BeatenChessPiece = beatenChessPiece;
        }

        /// <summary>
        /// Gets or sets the source <see cref="Field"/>.
        /// </summary>
        /// <value>The source <see cref="Field"/>.</value>
        public Field From
        {
            get => this.from;

            set
            {
                this.from = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Gets or sets the destination <see cref="Field"/>.
        /// </summary>
        /// <value>The destination <see cref="Field"/>.</value>
        public Field To
        {
            get => this.to;

            set
            {
                this.to = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Gets or sets the beaten <see cref="ChessPiece"/>.
        /// </summary>
        /// <value>The beaten <see cref="ChessPiece"/>.</value>
        [XmlIgnore]
        public ChessPiece BeatenChessPiece { get; set; }

        /// <summary>
        /// Creates a string representation of this <see cref="ChessMove"/>.
        /// </summary>
        /// <returns>A string representing this <see cref="ChessMove"/>.</returns>
        public override string ToString()
        {
            return $"{this.From} -> {this.To}";
        }
    }
}
