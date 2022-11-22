//---------------------------------------------------------------
// <copyright file="ChessGameVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameVM class.</summary>
//---------------------------------------------------------------
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Chess.Model;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessGameVM"/> class.
    /// </summary>
    public class ChessGameVM
    {
        /// <summary>
        /// The list of fields that are selected by the user.
        /// </summary>
        private readonly List<FieldVM> selections;

        /// <summary>
        /// The <see cref="Model.ChessGame"/> of this <see cref="ChessGameVM"/>.
        /// </summary>
        private ChessGame chessGame;

        /// <summary>
        /// The list of fields indicating the possible moves of a selected <see cref="ChessPiece"/>.
        /// </summary>
        private List<FieldVM> highlightedFields;

        /// <summary>
        /// The <see cref="ChessGameStatusVM"/> of this <see cref="ChessGameVM"/>.
        /// </summary>
        private ChessGameStatusVM status;

        /// <summary>
        /// The <see cref="ViewModel.ChessBoardVM"/> of this <see cref="ChessGameVM"/>.
        /// </summary>
        private ChessBoardVM chessBoardVM;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessGameVM"/> class.
        /// </summary>
        /// <param name="chessGame">The <see cref="Model.ChessGame"/> for this <see cref="ChessGameVM"/>.</param>
        public ChessGameVM(ChessGame chessGame)
        {
            this.ChessGame = chessGame;
            this.BeatenBlackChessPieces = new ObservableCollection<ChessPieceVM>();
            this.BeatenWhiteChessPieces = new ObservableCollection<ChessPieceVM>();
            this.Status = new ChessGameStatusVM(this.chessGame.Status);
            this.MoveList = new ObservableCollection<ChessMove>();
            this.selections = new List<FieldVM>();
            this.highlightedFields = new List<FieldVM>();
            this.ChessBoardVM = new ChessBoardVM(chessGame.Board);
            this.chessGame.ChessPiecePlaced += this.ChessGame_ChessPiecePlaced;
            this.chessGame.ChessPieceBeaten += this.ChessGame_ChessPieceBeaten;
            this.chessGame.ChessPieceMoved += this.ChessGame_ChessPieceMoved;
            this.chessGame.StatusUpdated += this.ChessGame_StatusUpdated;
            this.chessGame.KingInDanger += this.ChessGame_KingInDanger;
        }

        /// <summary>
        /// Gets the <see cref="Model.ChessGame"/> of this <see cref="ChessGameVM"/>.
        /// </summary>
        /// <value>The <see cref="Model.ChessGame"/> of this <see cref="ChessGameVM"/>.</value>
        public ChessGame ChessGame
        {
            get => this.chessGame;

            private set
            {
                this.chessGame = value ?? throw new ArgumentNullException("The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Gets the <see cref="ViewModel.ChessBoardVM"/> of this <see cref="ChessGameVM"/>.
        /// </summary>
        /// <value>The <see cref="ViewModel.ChessBoardVM"/> of this <see cref="ChessGameVM"/>.</value>
        public ChessBoardVM ChessBoardVM
        {
            get => this.chessBoardVM;

            private set
            {
                this.chessBoardVM = value ?? throw new ArgumentNullException("The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Gets the <see cref="ChessGameStatusVM"/> of this <see cref="ChessGameVM"/>.
        /// </summary>
        /// <value>The <see cref="ChessGameStatusVM"/> of this <see cref="ChessGameVM"/>.</value>
        public ChessGameStatusVM Status
        {
            get => this.status;

            private set
            {
                this.status = value ?? throw new ArgumentNullException("The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Gets the list containing all beaten black <see cref="ChessPiece"/> objects.
        /// </summary>
        /// <value>The list containing all beaten black <see cref="ChessPiece"/> objects.</value>
        public ObservableCollection<ChessPieceVM> BeatenBlackChessPieces
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list containing all beaten white <see cref="ChessPiece"/> objects.
        /// </summary>
        /// <value>The list containing all beaten white <see cref="ChessPiece"/> objects.</value>
        public ObservableCollection<ChessPieceVM> BeatenWhiteChessPieces
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the move list of this <see cref="ChessGameVM"/>.
        /// </summary>
        /// <value>The move list of this <see cref="ChessGameVM"/>.</value>
        public ObservableCollection<ChessMove> MoveList { get; private set; }

        /// <summary>
        /// Selects the <see cref="FieldVM"/> based on the currently selected fields. Unable to select if the game is over.
        /// </summary>
        /// <param name="left">The column position of the selected <see cref="FieldVM"/>.</param>
        /// <param name="top">The row position of the selected <see cref="FieldVM"/>.</param>
        public void SelectField(double left, double top)
        {
            if (this.chessGame.IsGameOver)
            {
                return;
            }
            else if (!this.selections.Any())
            {
                this.SelectFirstField(left, top);
            }
            else if (this.selections.Count == 1)
            {
                this.SelectSecondField(left, top);
            }
        }

        /// <summary>
        /// Rewinds the <see cref="Model.ChessGame"/> up until including the given <see cref="ChessMove"/>.
        /// </summary>
        /// <param name="chessMove">The <see cref="ChessMove"/> as an indicator for the amount to rewind.</param>
        public void Rewind(ChessMove chessMove)
        {
            if (!this.MoveList.Contains(chessMove))
            {
                return;
            }

            var rewindMoves = this.MoveList.SkipWhile(x => x != chessMove).Reverse().ToList();

            rewindMoves.ForEach(x => this.chessGame.Rewind(x));
            this.chessGame.DetermineCurrentGameStatus();
        }

        /// <summary>
        /// Updates the current danger status of the corresponding <see cref="King"/>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ChessGame_KingInDanger(object sender, KingInDangerEventArgs e)
        {
            var affectedKing = this.ChessBoardVM.PlacedPieces.FirstOrDefault(x => x.ChessPiece == e.King);
            affectedKing.FieldVM.Danger = e.IsInDanger;
        }

        /// <summary>
        /// Updates the current <see cref="ChessGameStatus"/> of the <see cref="ChessGameStatusVM"/>.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ChessGame_StatusUpdated(object sender, StatusUpdatedEventArgs e)
        {
            this.Status.Status = e.Status;
        }

        /// <summary>
        /// Moves the affected <see cref="ChessPieceVM"/> to its new position, updates the <see cref="FieldVM"/> danger values
        /// and adds the move to the move list.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ChessGame_ChessPieceMoved(object sender, ChessPieceMovedEventArgs e)
        {
            // Move chess piece and update danger values of the fields.
            var movedChessPieceVM = this.chessBoardVM.PlacedPieces.FirstOrDefault(x => x.ChessPiece == e.MovedChessPiece);
            var wasInDanger = movedChessPieceVM.FieldVM.Danger;
            movedChessPieceVM.FieldVM.Danger = false;
            movedChessPieceVM.FieldVM = this.ChessBoardVM.Fields.FirstOrDefault(x => x.Field == e.Destination);
            movedChessPieceVM.FieldVM.Danger = wasInDanger;

            // Move list update.
            if (!e.WasMoveRewind)
            {
                this.MoveList.Add(this.chessGame.MoveList.Last());
            }
            else
            {
                this.MoveList.Remove(this.chessGame.MoveList.Last());
            }
        }

        /// <summary>
        /// Removes the beaten <see cref="ChessPieceVM"/> from the board and stores it in the
        /// corresponding list for beaten chess pieces.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ChessGame_ChessPieceBeaten(object sender, ChessPieceBeatenEventArgs e)
        {
            var beatenChessPieceVM = this.chessBoardVM.PlacedPieces.FirstOrDefault(x => x.ChessPiece == e.BeatenChessPiece);
            this.ChessBoardVM.PlacedPieces.Remove(beatenChessPieceVM);

            if (beatenChessPieceVM.ChessPiece.Player.Colour.ToLower() == "black")
            {
                this.BeatenBlackChessPieces.Add(beatenChessPieceVM);
            }
            else if (beatenChessPieceVM.ChessPiece.Player.Colour.ToLower() == "white")
            {
                this.BeatenWhiteChessPieces.Add(beatenChessPieceVM);
            }
            else
            {
                throw new ArgumentOutOfRangeException("The beaten chess piece has an unknown colour. (Only black and white are supported)");
            }
        }

        /// <summary>
        /// Places the <see cref="ChessPiece"/> and removes it from the beaten list.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ChessGame_ChessPiecePlaced(object sender, ChessPiecePlacedEventArgs e)
        {
            if (e.PlacedChessPiece.Player.Colour.ToLower() == "black")
            {
                var chessPieceVM = this.BeatenBlackChessPieces.FirstOrDefault(x => x.ChessPiece == e.PlacedChessPiece);
                if (chessPieceVM != null)
                {
                    this.BeatenBlackChessPieces.Remove(chessPieceVM);
                    this.ChessBoardVM.PlacedPieces.Add(chessPieceVM);
                }
            }
            else if (e.PlacedChessPiece.Player.Colour.ToLower() == "white")
            {
                var chessPieceVM = this.BeatenWhiteChessPieces.FirstOrDefault(x => x.ChessPiece == e.PlacedChessPiece);
                if (chessPieceVM != null)
                {
                    this.BeatenWhiteChessPieces.Remove(chessPieceVM);
                    this.ChessBoardVM.PlacedPieces.Add(chessPieceVM);
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
            var selectedField = this.ChessBoardVM.Fields.FirstOrDefault(x => x.Field.Left == left && x.Field.Top == top);
            ChessPieceVM targetChessPiece = this.ChessBoardVM.PlacedPieces.Where(x => x.FieldVM.Field == selectedField.Field).FirstOrDefault();
            
            if (targetChessPiece == null)
            {
                return;
            }

            string chosenColour = targetChessPiece.ChessPiece.Player.Colour.ToLower();
            if ((this.chessGame.Status == ChessGameStatus.BlackActive && chosenColour == "white")
                || (this.chessGame.Status == ChessGameStatus.WhiteActive && chosenColour == "black"))
            {
                return;
            }

            if (!selectedField.Selected)
            {
                selectedField.Selected = true;
                this.selections.Add(selectedField);
                this.SetFieldHighlights(targetChessPiece.ChessPiece);
            }
            else
            {
                selectedField.Selected = false;
                this.selections.Remove(selectedField);
                this.ClearFieldHighlights();
            }
        }

        /// <summary>
        /// Selects the second <see cref="FieldVM"/> for moving a <see cref="ChessPieceVM"/>.
        /// </summary>
        /// <param name="left">The column position of the selected <see cref="FieldVM"/>.</param>
        /// <param name="top">The row position of the selected <see cref="FieldVM"/>.</param>
        private void SelectSecondField(double left, double top)
        {
            var selectedField = this.ChessBoardVM.Fields.FirstOrDefault(x => x.Field.Left == left && x.Field.Top == top);
            ChessPieceVM targetChessPiece = this.ChessBoardVM.PlacedPieces.Where(x => x.FieldVM.Field == selectedField.Field).FirstOrDefault();

            if (this.selections.Contains(selectedField))
            {
                selectedField.Selected = false;
                this.ClearFieldHighlights();
                this.selections.Clear();
                return;
            }

            if (this.highlightedFields.Contains(selectedField))
            {
                Field from = this.selections.FirstOrDefault().Field;
                this.chessGame.Move(from, selectedField.Field);
                this.selections.FirstOrDefault().Selected = false;
                this.ClearFieldHighlights();
                this.selections.Clear();
            }
        }

        /// <summary>
        /// Deactivates all currently highlighted fields using the list as a reference for which fields are highlighted.
        /// </summary>
        private void ClearFieldHighlights()
        {
            this.highlightedFields.ForEach(x => x.Highlighted = false);
            this.highlightedFields.Clear();
        }

        /// <summary>
        /// Highlights all fields that the given <see cref="ChessPiece"/> is permitted to move to.
        /// </summary>
        /// <param name="chessPiece">The <see cref="ChessPiece"/> whose moves get evaluated and highlighted.</param>
        private void SetFieldHighlights(ChessPiece chessPiece)
        {
            var field = this.selections.FirstOrDefault();
            var legalMoves = this.chessGame.GetLegalMoves(chessPiece);

            this.highlightedFields = (from move in legalMoves
                                      from fieldVM in this.ChessBoardVM.Fields
                                      where move.Left == fieldVM.Field.Left
                                      where move.Top == fieldVM.Field.Top
                                      select fieldVM).ToList();

            this.highlightedFields.ForEach(x => x.Highlighted = true);
        }
    }
}
