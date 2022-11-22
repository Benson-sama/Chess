//----------------------------------------------------------------------
// <copyright file="IChessPieceVisitor.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the IChessPieceVisitor interface.</summary>
//----------------------------------------------------------------------
namespace Chess.Model.ChessPieces
{
    /// <summary>
    /// Represents the <see cref="IChessPieceVisitor"/> interface.
    /// </summary>
    public interface IChessPieceVisitor
    {
        /// <summary>
        /// Visits the <see cref="King"/> class in order to do something.
        /// </summary>
        /// <param name="king">The <see cref="King"/> to be visited.</param>
        void Visit(King king);

        /// <summary>
        /// Visits the <see cref="Queen"/> class in order to do something.
        /// </summary>
        /// <param name="queen">The <see cref="Queen"/> to be visited.</param>
        void Visit(Queen queen);

        /// <summary>
        /// Visits the <see cref="Bishop"/> class in order to do something.
        /// </summary>
        /// <param name="bishop">The <see cref="Bishop"/> to be visited.</param>
        void Visit(Bishop bishop);

        /// <summary>
        /// Visits the <see cref="Rook"/> class in order to do something.
        /// </summary>
        /// <param name="rook">The <see cref="Rook"/> to be visited.</param>
        void Visit(Rook rook);

        /// <summary>
        /// Visits the <see cref="Knight"/> class in order to do something.
        /// </summary>
        /// <param name="knight">The <see cref="Knight"/> to be visited.</param>
        void Visit(Knight knight);

        /// <summary>
        /// Visits the <see cref="Pawn"/> class in order to do something.
        /// </summary>
        /// <param name="pawn">The <see cref="Pawn"/> to be visited.</param>
        void Visit(Pawn pawn);
    }
}
