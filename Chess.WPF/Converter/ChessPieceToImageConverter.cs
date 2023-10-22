//------------------------------------------------------------------------------
// <copyright file="ChessPieceToImageConverter.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPieceToImageConverter class.</summary>
//------------------------------------------------------------------------------

namespace Chess.WPF.Converter;

using System;
using System.Globalization;
using System.Windows.Data;
using Chess.Model.Pieces;

/// <summary>
/// Represents the <see cref="ChessPieceToImageConverter"/> class.
/// </summary>
public class ChessPieceToImageConverter : IValueConverter
{
    /// <summary>
    /// Converts a given <see cref="ChessPiece"/> to its corresponding image path.
    /// </summary>
    /// <param name="value">The expected <see cref="ChessPiece"/> for the conversion.</param>
    /// <param name="targetType">The target type, which is not used.</param>
    /// <param name="parameter">The parameter, which is not used.</param>
    /// <param name="culture">The culture, which is not used.</param>
    /// <returns>The image path based on the given <see cref="ChessPiece"/>.</returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ChessPieceVisitor visitor = new();
        ChessPiece chessPiece = (ChessPiece)value;
        chessPiece.Accept(visitor);

        return visitor.ImagePath;
    }

    /// <summary>
    /// This method is not implemented.
    /// </summary>
    /// <param name="value">The value, which is not used.</param>
    /// <param name="targetType">The target type, which is not used.</param>
    /// <param name="parameter">The parameter, which is not used.</param>
    /// <param name="culture">The culture, which is not used.</param>
    /// <returns>Nothing because it is not implemented.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
