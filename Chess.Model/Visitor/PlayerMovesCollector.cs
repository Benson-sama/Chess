//------------------------------------------------------------------------
// <copyright file="PlayerMovesCollector.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the PlayerMovesCollector class.</summary>
//------------------------------------------------------------------------

namespace Chess.Model.Visitor;

using System.Collections.Generic;
using Chess.Model.Pieces;
using Chess.Model.Utility;

/// <summary>
/// Represents the <see cref="PlayerMovesCollector"/> class.
/// </summary>
public class PlayerMovesCollector : IChessPieceVisitor
{
    /// <summary>
    /// Initialises a new instance of the <see cref="PlayerMovesCollector"/> class.
    /// </summary>
    /// <param name="rulebook">The rulebook for this <see cref="PlayerMovesCollector"/>.</param>
    public PlayerMovesCollector(RuleBook rulebook) => RuleBook = rulebook;

    /// <summary>
    /// Gets or sets the <see cref="Utility.Player"/> whose moves will be collected.
    /// </summary>
    /// <value>The <see cref="Utility.Player"/> whose moves will be collected.</value>
    public Player? Player { get; set; }

    /// <summary>
    /// Gets all legal moves that the set player can perform.
    /// </summary>
    /// <value>All legal moves that the set player can perform.</value>
    public List<Field> TotalLegalMoves { get; } = new();

    /// <summary>
    /// Gets the <see cref="Visitor.RuleBook"/> of this <see cref="PlayerMovesCollector"/>.
    /// </summary>
    /// <value>The <see cref="Visitor.RuleBook"/> of this <see cref="PlayerMovesCollector"/>.</value>
    public RuleBook RuleBook { get; init; }

    /// <summary>
    /// This method does nothing.
    /// </summary>
    /// <param name="king">The specified <see cref="King"/>.</param>
    public void Visit(King king) { }

    /// <summary>
    /// Adds the legal moves of the specified <see cref="Queen"/> to the list of all legal moves.
    /// </summary>
    /// <param name="queen">The specified <see cref="Queen"/>.</param>
    public void Visit(Queen queen) => AddLegalMovesToList(queen);

    /// <summary>
    /// Adds the legal moves of the specified <see cref="Bishop"/> to the list of all legal moves.
    /// </summary>
    /// <param name="bishop">The specified <see cref="Bishop"/>.</param>
    public void Visit(Bishop bishop) => AddLegalMovesToList(bishop);

    /// <summary>
    /// Adds the legal moves of the specified <see cref="Rook"/> to the list of all legal moves.
    /// </summary>
    /// <param name="rook">The specified <see cref="Rook"/>.</param>
    public void Visit(Rook rook) => AddLegalMovesToList(rook);

    /// <summary>
    /// Adds the legal moves of the specified <see cref="Knight"/> to the list of all legal moves.
    /// </summary>
    /// <param name="knight">The specified <see cref="Knight"/>.</param>
    public void Visit(Knight knight) => AddLegalMovesToList(knight);

    /// <summary>
    /// Adds the legal moves of the specified <see cref="Pawn"/> to the list of all legal moves.
    /// </summary>
    /// <param name="pawn">The specified <see cref="Pawn"/>.</param>
    public void Visit(Pawn pawn) => AddLegalMovesToList(pawn);

    /// <summary>
    /// Adds the legal moves of a given <see cref="ChessPiece"/> to the list of all legal moves.
    /// </summary>
    /// <param name="chessPiece">The specified <see cref="ChessPiece"/>.</param>
    private void AddLegalMovesToList(ChessPiece chessPiece)
    {
        if (chessPiece.Player == Player)
            return;

        chessPiece.Accept(RuleBook);
        TotalLegalMoves.AddRange(RuleBook.LegalMoves);
    }
}
