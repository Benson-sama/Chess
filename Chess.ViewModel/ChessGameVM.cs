//---------------------------------------------------------------
// <copyright file="ChessGameVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameVM class.</summary>
//---------------------------------------------------------------

namespace Chess.ViewModel;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Chess.Model;
using Chess.Model.Events;
using Chess.Model.Pieces;
using Chess.Model.Utility;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Represents the <see cref="ChessGameVM"/> class.
/// </summary>
public partial class ChessGameVM : ObservableObject
{
    /// <summary>
    /// The list of fields that are selected by the user.
    /// </summary>
    private readonly List<FieldVM> _selections = new();

    /// <summary>
    /// The <see cref="Model.ChessGame"/> of this <see cref="ChessGameVM"/>.
    /// </summary>
    private readonly ChessGame _chessGame;

    private readonly ChessGameStatusVM _chessGameStatusVM;

    /// <summary>
    /// The <see cref="ViewModel.ChessBoardVM"/> of this <see cref="ChessGameVM"/>.
    /// </summary>
    private readonly ChessBoardVM _chessBoardVM;

    /// <summary>
    /// The list of fields indicating the possible moves of a selected <see cref="ChessPiece"/>.
    /// </summary>
    private readonly List<FieldVM> _highlightedFields = new();

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessGameVM"/> class.
    /// </summary>
    /// <param name="chessGame">The <see cref="Model.ChessGame"/> for this <see cref="ChessGameVM"/>.</param>
    public ChessGameVM(ChessGame chessGame)
    {
        _chessGame = chessGame;
        _chessBoardVM = new ChessBoardVM(chessGame.Board);
        _chessGameStatusVM = new ChessGameStatusVM(chessGame.Status);
        chessGame.ChessPiecePlaced += ChessGame_ChessPiecePlaced;
        chessGame.ChessPieceBeaten += ChessGame_ChessPieceBeaten;
        chessGame.ChessPieceMoved += ChessGame_ChessPieceMoved;
        chessGame.StatusUpdated += ChessGame_StatusUpdated;
        chessGame.KingInDanger += ChessGame_KingInDanger;
    }

    private List<FieldVM> Selections => _selections;

    private List<FieldVM> HighlightedFields => _highlightedFields;

    /// <summary>
    /// Gets the <see cref="Model.ChessGame"/> of this <see cref="ChessGameVM"/>.
    /// </summary>
    /// <value>The <see cref="Model.ChessGame"/> of this <see cref="ChessGameVM"/>.</value>
    public ChessGame ChessGame => _chessGame;

    /// <summary>
    /// Gets the <see cref="ViewModel.ChessBoardVM"/> of this <see cref="ChessGameVM"/>.
    /// </summary>
    /// <value>The <see cref="ViewModel.ChessBoardVM"/> of this <see cref="ChessGameVM"/>.</value>
    public ChessBoardVM ChessBoardVM => _chessBoardVM;

    /// <summary>
    /// Gets the <see cref="ChessGameStatusVM"/> of this <see cref="ChessGameVM"/>.
    /// </summary>
    /// <value>The <see cref="ChessGameStatusVM"/> of this <see cref="ChessGameVM"/>.</value>
    public ChessGameStatusVM Status => _chessGameStatusVM;

    /// <summary>
    /// Gets the list containing all beaten black <see cref="ChessPiece"/> objects.
    /// </summary>
    /// <value>The list containing all beaten black <see cref="ChessPiece"/> objects.</value>
    public ObservableCollection<ChessPieceVM> BeatenBlackChessPieces { get; private set; } = new();

    /// <summary>
    /// Gets the list containing all beaten white <see cref="ChessPiece"/> objects.
    /// </summary>
    /// <value>The list containing all beaten white <see cref="ChessPiece"/> objects.</value>
    public ObservableCollection<ChessPieceVM> BeatenWhiteChessPieces { get; private set; } = new();

    /// <summary>
    /// Gets the move list of this <see cref="ChessGameVM"/>.
    /// </summary>
    /// <value>The move list of this <see cref="ChessGameVM"/>.</value>
    public ObservableCollection<ChessMove> MoveList { get; private set; } = new();

    /// <summary>
    /// Selects the <see cref="FieldVM"/> based on the currently selected fields. Unable to select if the game is over.
    /// </summary>
    /// <param name="left">The column position of the selected <see cref="FieldVM"/>.</param>
    /// <param name="top">The row position of the selected <see cref="FieldVM"/>.</param>
    public void SelectField(double left, double top)
    {
        if (ChessGame.IsGameOver)
            return;

        if (!Selections.Any())
            SelectFirstField(left, top);
        else if (Selections.Count == 1)
            SelectSecondField(left, top);
    }

    /// <summary>
    /// Rewinds the <see cref="Model.ChessGame"/> up until including the given <see cref="ChessMove"/>.
    /// </summary>
    /// <param name="chessMove">The <see cref="ChessMove"/> as an indicator for the amount to rewind.</param>
    public void Rewind(ChessMove chessMove)
    {
        if (!MoveList.Contains(chessMove))
            return;

        var rewindMoves = MoveList.SkipWhile(x => x != chessMove).Reverse().ToList();

        rewindMoves.ForEach(x => ChessGame.Rewind(x));
        ChessGame.DetermineCurrentGameStatus();
    }

    /// <summary>
    /// Updates the current danger status of the corresponding <see cref="King"/>.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void ChessGame_KingInDanger(object? sender, KingInDangerEventArgs e)
    {
        var affectedKing = ChessBoardVM.PlacedPieces.FirstOrDefault(x => x.ChessPiece == e.King);
        affectedKing.FieldVM.IsInDanger = e.IsInDanger;
    }

    /// <summary>
    /// Updates the current <see cref="ChessGameStatus"/> of the <see cref="ChessGameStatusVM"/>.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void ChessGame_StatusUpdated(object? sender, StatusUpdatedEventArgs e)
        => Status.Status = e.Status;

    /// <summary>
    /// Moves the affected <see cref="ChessPieceVM"/> to its new position, updates the <see cref="FieldVM"/> danger values
    /// and adds the move to the move list.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void ChessGame_ChessPieceMoved(object? sender, ChessPieceMovedEventArgs e)
    {
        // Move chess piece and update danger values of the fields.
        var movedChessPieceVM = ChessBoardVM.PlacedPieces.FirstOrDefault(x => x.ChessPiece == e.MovedChessPiece);
        var wasInDanger = movedChessPieceVM.FieldVM.IsInDanger;
        movedChessPieceVM.FieldVM.IsInDanger = false;
        movedChessPieceVM.FieldVM = ChessBoardVM.Fields.FirstOrDefault(x => x.Field == e.Destination);
        movedChessPieceVM.FieldVM.IsInDanger = wasInDanger;

        // Move list update.
        if (!e.WasMoveRewind)
            MoveList.Add(ChessGame.MoveList.Last());
        else
            MoveList.Remove(ChessGame.MoveList.Last());
    }

    /// <summary>
    /// Removes the beaten <see cref="ChessPieceVM"/> from the board and stores it in the
    /// corresponding list for beaten chess pieces.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void ChessGame_ChessPieceBeaten(object? sender, ChessPieceBeatenEventArgs e)
    {
        var beatenChessPieceVM = ChessBoardVM.PlacedPieces.FirstOrDefault(x => x.ChessPiece == e.BeatenChessPiece);
        ChessBoardVM.PlacedPieces.Remove(beatenChessPieceVM);

        if (beatenChessPieceVM.ChessPiece.Player.FacingDirection == Direction.North)
            BeatenBlackChessPieces.Add(beatenChessPieceVM);
        else if (beatenChessPieceVM.ChessPiece.Player.FacingDirection == Direction.South)
            BeatenWhiteChessPieces.Add(beatenChessPieceVM);
        else
            throw new ArgumentOutOfRangeException("The beaten chess piece has an unknown colour. (Only black and white are supported)");
    }

    /// <summary>
    /// Places the <see cref="ChessPiece"/> and removes it from the beaten list.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void ChessGame_ChessPiecePlaced(object? sender, ChessPiecePlacedEventArgs e)
    {
        if (e.PlacedChessPiece.Player.FacingDirection == Direction.North)
        {
            var chessPieceVM = BeatenBlackChessPieces.FirstOrDefault(x => x.ChessPiece == e.PlacedChessPiece);
            if (chessPieceVM != null)
            {
                BeatenBlackChessPieces.Remove(chessPieceVM);
                ChessBoardVM.PlacedPieces.Add(chessPieceVM);
            }
        }
        else if (e.PlacedChessPiece.Player.FacingDirection == Direction.South)
        {
            var chessPieceVM = BeatenWhiteChessPieces.FirstOrDefault(x => x.ChessPiece == e.PlacedChessPiece);
            if (chessPieceVM != null)
            {
                BeatenWhiteChessPieces.Remove(chessPieceVM);
                ChessBoardVM.PlacedPieces.Add(chessPieceVM);
            }
        }
    }

    /// <summary>
    /// Selects the first <see cref="FieldVM"/> for moving a <see cref="ChessPieceVM"/>.
    /// </summary>
    /// <param name="left">The column position of the selected <see cref="FieldVM"/>.</param>
    /// <param name="top">The row position of the selected <see cref="FieldVM"/>.</param>
    private void SelectFirstField(double left, double top)
    {
        var selectedField = ChessBoardVM.Fields.FirstOrDefault(x => x.Field.Left == left && x.Field.Top == top);
        ChessPieceVM targetChessPiece = ChessBoardVM.PlacedPieces
            .Where(x => x.FieldVM.Field == selectedField.Field).FirstOrDefault();
        
        if (targetChessPiece is null)
            return;

        Player currentPlayer = targetChessPiece.ChessPiece.Player;
        if ((ChessGame.Status == ChessGameStatus.BlackActive && currentPlayer == ChessGame.FirstPlayer)
            || (ChessGame.Status == ChessGameStatus.WhiteActive && currentPlayer == ChessGame.SecondPlayer))
        {
            return;
        }

        if (!selectedField.IsSelected)
        {
            selectedField.IsSelected = true;
            Selections.Add(selectedField);
            SetFieldHighlights(targetChessPiece.ChessPiece);
        }
        else
        {
            selectedField.IsSelected = false;
            Selections.Remove(selectedField);
            ClearFieldHighlights();
        }
    }

    /// <summary>
    /// Selects the second <see cref="FieldVM"/> for moving a <see cref="ChessPieceVM"/>.
    /// </summary>
    /// <param name="left">The column position of the selected <see cref="FieldVM"/>.</param>
    /// <param name="top">The row position of the selected <see cref="FieldVM"/>.</param>
    private void SelectSecondField(double left, double top)
    {
        var selectedField = ChessBoardVM.Fields.FirstOrDefault(x => x.Field.Left == left && x.Field.Top == top);
        ChessPieceVM targetChessPiece = ChessBoardVM.PlacedPieces.Where(x => x.FieldVM.Field == selectedField.Field).FirstOrDefault();

        if (Selections.Contains(selectedField))
        {
            selectedField.IsSelected = false;
            ClearFieldHighlights();
            Selections.Clear();
            return;
        }

        if (_highlightedFields.Contains(selectedField))
        {
            Field from = Selections.FirstOrDefault().Field;
            ChessGame.Move(from, selectedField.Field);
            Selections.FirstOrDefault().IsSelected = false;
            ClearFieldHighlights();
            Selections.Clear();
        }
    }

    /// <summary>
    /// Deactivates all currently highlighted fields using the list as a reference for which fields are highlighted.
    /// </summary>
    private void ClearFieldHighlights()
    {
        HighlightedFields.ForEach(x => x.IsHighlighted = false);
        HighlightedFields.Clear();
    }

    /// <summary>
    /// Highlights all fields that the given <see cref="ChessPiece"/> is permitted to move to.
    /// </summary>
    /// <param name="chessPiece">The <see cref="ChessPiece"/> whose moves get evaluated and highlighted.</param>
    private void SetFieldHighlights(ChessPiece chessPiece)
    {
        var field = Selections.FirstOrDefault();
        var legalMoves = ChessGame.GetLegalMoves(chessPiece);

        HighlightedFields.Clear();
        HighlightedFields.AddRange((from move in legalMoves
                             from fieldVM in ChessBoardVM.Fields
                             where move.Left == fieldVM.Field.Left
                             where move.Top == fieldVM.Field.Top
                             select fieldVM));

        HighlightedFields.ForEach(x => x.IsHighlighted = true);
    }
}
