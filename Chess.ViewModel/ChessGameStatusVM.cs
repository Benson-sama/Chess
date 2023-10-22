//---------------------------------------------------------------------
// <copyright file="ChessGameStatusVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameStatusVM class.</summary>
//---------------------------------------------------------------------

namespace Chess.ViewModel;

using Chess.Model;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Represents the <see cref="ChessGameStatusVM"/> class.
/// </summary>
public partial class ChessGameStatusVM : ObservableObject
{
    /// <summary>
    /// The <see cref="ChessGameStatus"/> of this <see cref="ChessGameStatusVM"/>.
    /// </summary>
    [ObservableProperty]
    private ChessGameStatus _status;

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessGameStatusVM"/> class.
    /// </summary>
    /// <param name="status">The <see cref="ChessGameStatus"/> for this <see cref="ChessGameStatusVM"/>.</param>
    public ChessGameStatusVM(ChessGameStatus status) => Status = status;
}
