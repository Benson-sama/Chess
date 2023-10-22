//-----------------------------------------------------------
// <copyright file="Field.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Field class.</summary>
//-----------------------------------------------------------

namespace Chess.Model.Utility;

using System;

/// <summary>
/// Represents the <see cref="Field"/> record.
/// </summary>
[Serializable]
public record Field
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Field"/> record.
    /// </summary>
    /// <param name="column">The column of the <see cref="Field"/>.</param>
    /// <param name="row">The row of the <see cref="Field"/>.</param>
    public Field(int column = 0, int row = 0)
    {
        if (column < 0)
            throw new ArgumentOutOfRangeException(nameof(column), "Cannot be less than zero.");

        if (row < 0)
            throw new ArgumentOutOfRangeException(nameof(row), "Cannot be less than zero.");

        (Left, Top) = (column, row);
    }

    /// <summary>
    /// Gets or sets the column position of the <see cref="Field"/>.
    /// </summary>
    /// <value>The column position of the <see cref="Field"/>.</value>
    public int Left { get; init; }

    /// <summary>
    /// Gets or sets the row position of the <see cref="Field"/>.
    /// </summary>
    /// <value>The row position of the <see cref="Field"/>.</value>
    public int Top { get; init; }

    /// <summary>
    /// Creates a string representing this <see cref="Field"/>.
    /// </summary>
    /// <returns>The string representing this <see cref="Field"/>.</returns>
    public override string ToString()
    {
        string column = char.ConvertFromUtf32(65 + Left);

        return $"{column}{Top + 1}";
    }
}
