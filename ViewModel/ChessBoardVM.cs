//----------------------------------------------------------------
// <copyright file="ChessBoardVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessBoardVM class.</summary>
//----------------------------------------------------------------
namespace Chess.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Chess.Model;

    /// <summary>
    /// Represents the <see cref="ChessBoardVM"/> class.
    /// </summary>
    public class ChessBoardVM
    {
        /// <summary>
        /// The board of the <see cref="ChessBoardVM"/>.
        /// </summary>
        private readonly ChessBoard board;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessBoardVM"/> class.
        /// </summary>
        /// <param name="chessBoard">The <see cref="ChessBoard"/>.</param>
        public ChessBoardVM(ChessBoard chessBoard)
        {
            this.PlacedPieces = new ObservableCollection<ChessPieceVM>();
            this.board = chessBoard;
            this.Height = this.board.Height;
            this.Width = this.board.Width;

            this.Fields = (from x in Enumerable.Range(0, this.board.Width)
                           from y in Enumerable.Range(0, this.board.Height)
                           select new FieldVM(new Field(x, y))).ToList();

            foreach (var entry in this.board.OccupiedFields)
            {
                var fieldVM = this.Fields.FirstOrDefault(x => x.Field == entry.Key);
                this.PlacedPieces.Add(new ChessPieceVM(entry.Value, fieldVM));
            }

            this.RowLabels = (from number in Enumerable.Range(1, this.Height)
                              select number.ToString("D2")).Reverse().ToList();

            this.ColumnLabels = (from number in Enumerable.Range(0, this.Width)
                                 select char.ConvertFromUtf32(65 + number)).ToList();
        }

        /// <summary>
        /// Gets the height of the <see cref="ChessBoardVM"/>.
        /// </summary>
        /// <value>The height of the <see cref="ChessBoardVM"/>.</value>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the width of the <see cref="ChessBoardVM"/>.
        /// </summary>
        /// <value>The width of the <see cref="ChessBoardVM"/>.</value>
        public int Width { get; private set; }

        /// <summary>
        /// Gets or sets the fields of the <see cref="ChessBoardVM"/>.
        /// </summary>
        /// <value>The fields of the <see cref="ChessBoardVM"/>.</value>
        public List<FieldVM> Fields
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the placed pieces of this <see cref="ChessBoardVM"/>.
        /// </summary>
        /// <value>The placed pieces of this <see cref="ChessBoardVM"/>.</value>
        public ObservableCollection<ChessPieceVM> PlacedPieces { get; set; }

        /// <summary>
        /// Gets the row labels of the <see cref="ChessBoardVM"/>.
        /// </summary>
        /// <value>The row labels of the <see cref="ChessBoardVM"/>.</value>
        public List<string> RowLabels { get; private set; }

        /// <summary>
        /// Gets the column labels of the <see cref="ChessBoardVM"/>.
        /// </summary>
        /// <value>The column labels of the <see cref="ChessBoardVM"/>.</value>
        public List<string> ColumnLabels { get; private set; }
    }
}
