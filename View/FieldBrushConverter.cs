//-----------------------------------------------------------------------
// <copyright file="FieldBrushConverter.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the FieldBrushConverter class.</summary>
//-----------------------------------------------------------------------
namespace Chess.View
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using Chess.Model;

    /// <summary>
    /// Represents the <see cref="FieldBrushConverter"/> class.
    /// </summary>
    public class FieldBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a given <see cref="Field"/> to its corresponding <see cref="Brushes"/>.
        /// </summary>
        /// <param name="value">The expected <see cref="ChessPiece"/> for the conversion.</param>
        /// <param name="targetType">The target type, which is not used.</param>
        /// <param name="parameter">The parameter, which is not used.</param>
        /// <param name="culture">The culture, which is not used.</param>
        /// <returns>The <see cref="Brushes"/> based on the position of the field.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Field field = (Field)value;

            if ((field.Left + field.Top) % 2 == 0)
            {
                return Brushes.DimGray;
            }

            return Brushes.White;
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
