//----------------------------------------------------------------
// <copyright file="ChessPieceVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPieceVM class.</summary>
//----------------------------------------------------------------

namespace Chess.ViewModel;

using Chess.Model.Pieces;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Represents the <see cref="ChessPieceVM"/> class.
/// </summary>
public partial class ChessPieceVM : ObservableObject
{
    /// <summary>
    /// The <see cref="Model.ChessPieces.ChessPiece"/>.
    /// </summary>
    [ObservableProperty]
    private ChessPiece _chessPiece;

    /// <summary>
    /// The <see cref="ChessPieceVM"/>.
    /// </summary>
    [ObservableProperty]
    private FieldVM _fieldVM;

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessPieceVM"/> class.
    /// </summary>
    /// <param name="chessPiece">The <see cref="Model.ChessPieces.ChessPiece"/>.</param>
    /// <param name="fieldVM">The <see cref="ChessPieceVM"/>.</param>
    public ChessPieceVM(ChessPiece chessPiece, FieldVM fieldVM)
     => (ChessPiece, FieldVM) = (chessPiece, fieldVM);
}
