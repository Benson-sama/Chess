//----------------------------------------------------------------
// <copyright file="ChessPieceVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessPieceVM class.</summary>
//----------------------------------------------------------------
namespace Chess.ViewModel
{
    using System;
    using System.ComponentModel;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessPieceVM"/> class.
    /// </summary>
    public class ChessPieceVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="Model.ChessPieces.ChessPiece"/>.
        /// </summary>
        private ChessPiece chessPiece;

        /// <summary>
        /// The <see cref="ChessPieceVM"/>.
        /// </summary>
        private FieldVM fieldVM;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessPieceVM"/> class.
        /// </summary>
        /// <param name="chessPiece">The <see cref="Model.ChessPieces.ChessPiece"/>.</param>
        /// <param name="fieldVM">The <see cref="ChessPieceVM"/>.</param>
        public ChessPieceVM(ChessPiece chessPiece, FieldVM fieldVM)
        {
            this.ChessPiece = chessPiece;
            this.FieldVM = fieldVM;
        }

        /// <summary>
        /// The event that gets fired when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the <see cref="ViewModel.FieldVM"/>.
        /// </summary>
        /// <value>The <see cref="ViewModel.FieldVM"/>.</value>
        public FieldVM FieldVM
        {
            get => this.fieldVM;

            set
            {
                this.fieldVM = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null.");
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FieldVM)));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Model.ChessPieces.ChessPiece"/>.
        /// </summary>
        /// <value>The <see cref="Model.ChessPieces.ChessPiece"/>.</value>
        public ChessPiece ChessPiece
        {
            get => this.chessPiece;

            set
            {
                this.chessPiece = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null.");
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ChessPiece)));
            }
        }
    }
}
