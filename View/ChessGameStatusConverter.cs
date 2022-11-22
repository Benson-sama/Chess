//----------------------------------------------------------------------------
// <copyright file="ChessGameStatusConverter.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameStatusConverter class.</summary>
//----------------------------------------------------------------------------
namespace Chess.View
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Chess.Model;

    /// <summary>
    /// Represents the <see cref="ChessGameStatusConverter"/> class.
    /// </summary>
    public class ChessGameStatusConverter : IValueConverter
    {
        /// <summary>
        /// Converts a given <see cref="ChessGameStatus"/> to its status text.
        /// </summary>
        /// <param name="value">The expected <see cref="ChessGameStatus"/> for the conversion.</param>
        /// <param name="targetType">The target type, which is not used.</param>
        /// <param name="parameter">The parameter, which is not used.</param>
        /// <param name="culture">The culture, which is not used.</param>
        /// <returns>The status text as a string.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ChessGameStatus status = (ChessGameStatus)value;

            switch (status)
            {
                case ChessGameStatus.BlackActive:
                    return "Black's turn";
                case ChessGameStatus.WhiteActive:
                    return "White's turn";
                case ChessGameStatus.BlackWon:
                    return "Black won!";
                case ChessGameStatus.WhiteWon:
                    return "White won!";
                case ChessGameStatus.Draw:
                    return "It's a draw!";
                default:
                    throw new ArgumentOutOfRangeException("The specified status is unknown.");
            }
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
        {
            throw new NotImplementedException();
        }
    }
}
