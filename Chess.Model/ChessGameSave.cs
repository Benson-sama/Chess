//-----------------------------------------------------------------
// <copyright file="ChessGameSave.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameSave class.</summary>
//-----------------------------------------------------------------

namespace Chess.Model;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents the <see cref="ChessGameSave"/> class.
/// </summary>
[Serializable]
public class ChessGameSave
{
    /// <summary>
    /// The width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
    /// </summary>
    private int _width;

    /// <summary>
    /// The height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
    /// </summary>
    private int _height;

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessGameSave"/> class.
    /// </summary>
    public ChessGameSave()
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessGameSave"/> class.
    /// </summary>
    /// <param name="moveList">The move list of this <see cref="ChessGameSave"/>.</param>
    /// <param name="width">The width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</param>
    /// <param name="height">The height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</param>
    public ChessGameSave(List<ChessMove> moveList, int width, int height)
    {
        MoveList = moveList;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Gets or sets the move list of this <see cref="ChessGameSave"/>.
    /// </summary>
    /// <value>The move list of this <see cref="ChessGameSave"/>.</value>
    public List<ChessMove> MoveList { get; set; } = new();

    /// <summary>
    /// Gets or sets the height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
    /// </summary>
    /// <value>The height of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</value>
    public int Height
    {
        get => _height;

        set
        {
            if (value < 8)
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot be less than 8.");

            _height = value;
        }
    }

    /// <summary>
    /// Gets or sets the width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.
    /// </summary>
    /// <value>The width of the <see cref="ChessBoard"/> that this <see cref="ChessGameSave"/> belongs to.</value>
    public int Width
    {
        get => _width;

        set
        {
            if (value < 8)
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot be less than 8.");

            _width = value;
        }
    }
}
