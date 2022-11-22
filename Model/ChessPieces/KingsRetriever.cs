//------------------------------------------------------------------
// <copyright file="KingsRetriever.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the KingsRetriever class.</summary>
//------------------------------------------------------------------
namespace Chess.Model.ChessPieces
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the <see cref="KingsRetriever"/> class used for retrieving
    /// <see cref="King"/> instances out of <see cref="ChessPiece"/> instances.
    /// </summary>
    public class KingsRetriever : IChessPieceVisitor
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="KingsRetriever"/> class.
        /// </summary>
        public KingsRetriever()
        {
            this.Kings = new List<King>();
        }

        /// <summary>
        /// Gets the list of <see cref="King"/> objects.
        /// </summary>
        /// <value>The list of <see cref="King"/> objects.</value>
        public List<King> Kings { get; private set; }

        /// <summary>
        /// Adds the visited <see cref="King"/> to the internal list.
        /// </summary>
        /// <param name="king">The visited <see cref="King"/>.</param>
        public void Visit(King king)
        {
            this.Kings.Add(king);
        }

        /// <summary>
        /// Placeholder method for visiting a <see cref="Queen"/>.
        /// </summary>
        /// <param name="queen">The visited <see cref="Queen"/>.</param>
        public void Visit(Queen queen)
        {
        }

        /// <summary>
        /// Placeholder method for visiting a <see cref="Bishop"/>.
        /// </summary>
        /// <param name="bishop">The visited <see cref="Bishop"/>.</param>
        public void Visit(Bishop bishop)
        {
        }

        /// <summary>
        /// Placeholder method for visiting a <see cref="Rook"/>.
        /// </summary>
        /// <param name="rook">The visited <see cref="Rook"/>.</param>
        public void Visit(Rook rook)
        {
        }

        /// <summary>
        /// Placeholder method for visiting a <see cref="Knight"/>.
        /// </summary>
        /// <param name="knight">The visited <see cref="Knight"/>.</param>
        public void Visit(Knight knight)
        {
        }

        /// <summary>
        /// Placeholder method for visiting a <see cref="Pawn"/>.
        /// </summary>
        /// <param name="pawn">The visited <see cref="Pawn"/>.</param>
        public void Visit(Pawn pawn)
        {
        }
    }
}
