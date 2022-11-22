//-----------------------------------------------------------
// <copyright file="Field.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the Field class.</summary>
//-----------------------------------------------------------
namespace Chess.Model
{
    using System;

    /// <summary>
    /// Represents the <see cref="Field"/> class.
    /// </summary>
    [Serializable]
    public class Field : IEquatable<Field>
    {
        /// <summary>
        /// The column position of the <see cref="Field"/>.
        /// </summary>
        private int left;

        /// <summary>
        /// The row position of the <see cref="Field"/>.
        /// </summary>
        private int top;

        /// <summary>
        /// Initialises a new instance of the <see cref="Field"/> class.
        /// </summary>
        public Field()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="column">The column of the <see cref="Field"/>.</param>
        /// <param name="row">The row of the <see cref="Field"/>.</param>
        public Field(int column, int row)
        {
            this.Left = column;
            this.Top = row;
        }

        /// <summary>
        /// Gets or sets the column position of the <see cref="Field"/>.
        /// </summary>
        /// <value>The column position of the <see cref="Field"/>.</value>
        public int Left
        {
            get => this.left;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified value cannot be null.");
                }

                this.left = value;
            }
        }

        /// <summary>
        /// Gets or sets the row position of the <see cref="Field"/>.
        /// </summary>
        /// <value>The row position of the <see cref="Field"/>.</value>
        public int Top
        {
            get => this.top;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The specified value cannot be null.");
                }

                this.top = value;
            }
        }

        /// <summary>
        /// Determines if two fields are equal.
        /// </summary>
        /// <param name="firstField">The first <see cref="Field"/>.</param>
        /// <param name="secondField">The second <see cref="Field"/>.</param>
        /// <returns>A value indicating whether the specified fields are equal.</returns>
        public static bool operator ==(Field firstField, Field secondField)
        {
            if (firstField is null)
            {
                if (secondField is null)
                {
                    return true;
                }

                return false;
            }

            return firstField.Equals(secondField);
        }

        /// <summary>
        /// Determines if two fields are not equal.
        /// </summary>
        /// <param name="firstField">The first <see cref="Field"/>.</param>
        /// <param name="secondField">The second <see cref="Field"/>.</param>
        /// <returns>A value indicating whether the specified fields are not equal.</returns>
        public static bool operator !=(Field firstField, Field secondField) => !(firstField == secondField);

        /// <summary>
        /// Compares this <see cref="Field"/> with another <see cref="Field"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Field"/>.</param>
        /// <returns>A value indicating whether the fields are equal.</returns>
        public bool Equals(Field other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Left == this.Left
                   && other.Top == this.Top;
        }

        /// <summary>
        /// The default equality comparison.
        /// </summary>
        /// <param name="obj">The second object to compare.</param>
        /// <returns>A value indicating whether the specified object is equal.</returns>
        public override bool Equals(object obj) => this.Equals(obj as Field);

        /// <summary>
        /// Calculates the hash code of this <see cref="Field"/>.
        /// </summary>
        /// <returns>The hash code of this <see cref="Field"/>.</returns>
        public override int GetHashCode() => (this.Left, this.Top).GetHashCode();

        /// <summary>
        /// Creates a string representing this <see cref="Field"/>.
        /// </summary>
        /// <returns>The string representing this <see cref="Field"/>.</returns>
        public override string ToString()
        {
            string column = char.ConvertFromUtf32(65 + this.Left);

            return $"{column}{this.Top + 1}";
        }
    }
}
