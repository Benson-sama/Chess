//----------------------------------------------------------------
// <copyright file="ChessBoardVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessBoardVM class.</summary>
//----------------------------------------------------------------

namespace Chess.ViewModel;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Chess.Model;
using Chess.Model.Utility;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Represents the <see cref="ChessBoardVM"/> class.
/// </summary>
public class ChessBoardVM : ObservableObject
{
    /// <summary>
    /// The board of the <see cref="ChessBoardVM"/>.
    /// </summary>
    private readonly ChessBoard _chessBoard;

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessBoardVM"/> class.
    /// </summary>
    /// <param name="chessBoard">The <see cref="ChessBoard"/>.</param>
    public ChessBoardVM(ChessBoard chessBoard)
    {
        PlacedPieces = new ObservableCollection<ChessPieceVM>();
        _chessBoard = chessBoard;
        Fields = (from x in Enumerable.Range(0, _chessBoard.Width)
                  from y in Enumerable.Range(0, _chessBoard.Height)
                  select new FieldVM(new Field(x, y))).ToList();

        foreach (KeyValuePair<Field, Model.Pieces.ChessPiece> entry in _chessBoard.OccupiedFields)
        {
            FieldVM? fieldVM = Fields.FirstOrDefault(x => x.Field == entry.Key);

            if (fieldVM is null)
                throw new ArgumentNullException(nameof(fieldVM), "Cannot be null.");

            PlacedPieces.Add(new ChessPieceVM(entry.Value, fieldVM));
        }

        RowLabels = (from number in Enumerable.Range(1, Height)
                     select number.ToString("D2")).Reverse().ToList();

        ColumnLabels = (from number in Enumerable.Range(0, Width)
                        select char.ConvertFromUtf32(65 + number)).ToList();
    }

    /// <summary>
    /// Gets the height of the <see cref="ChessBoardVM"/>.
    /// </summary>
    /// <value>The height of the <see cref="ChessBoardVM"/>.</value>
    public int Height => _chessBoard.Height;

    /// <summary>
    /// Gets the width of the <see cref="ChessBoardVM"/>.
    /// </summary>
    /// <value>The width of the <see cref="ChessBoardVM"/>.</value>
    public int Width => _chessBoard.Width;

    /// <summary>
    /// Gets or sets the fields of the <see cref="ChessBoardVM"/>.
    /// </summary>
    /// <value>The fields of the <see cref="ChessBoardVM"/>.</value>
    public List<FieldVM> Fields { get; }

    /// <summary>
    /// Gets or sets the placed pieces of this <see cref="ChessBoardVM"/>.
    /// </summary>
    /// <value>The placed pieces of this <see cref="ChessBoardVM"/>.</value>
    public ObservableCollection<ChessPieceVM> PlacedPieces { get; }

    /// <summary>
    /// Gets the row labels of the <see cref="ChessBoardVM"/>.
    /// </summary>
    /// <value>The row labels of the <see cref="ChessBoardVM"/>.</value>
    public List<string> RowLabels { get; }

    /// <summary>
    /// Gets the column labels of the <see cref="ChessBoardVM"/>.
    /// </summary>
    /// <value>The column labels of the <see cref="ChessBoardVM"/>.</value>
    public List<string> ColumnLabels { get; }
}
